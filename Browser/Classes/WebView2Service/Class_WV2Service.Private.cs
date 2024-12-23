using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.Diagnostics;

namespace WV2Service
{
    public partial class WebViewService
    {
        private void InitializeBrowser()
        {
            InitializeProfile();
            EnableMobileView();
            InitializeExtensions();
            EnableNewWindowRequest();
            EnableNavigationMonitoring();
        }
        private async Task<CoreWebView2Profile> GetProfile(WebView2 webView, CoreWebView2Environment environment)
        {
            try
            {
                await webView.EnsureCoreWebView2Async(environment);
                return  webView.CoreWebView2.Profile;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get profile:\n{ex.Message}");
            }
        }
        private async Task<CoreWebView2Environment> InitializeWebEnviromentAsync(WebView2 webView, string profileName)
        {
            try
            {
                // Path if the desired directory is in the locap appdata dir. I prefer it be in the base dir of the app itself
                //string userDataFolder = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyWebView2AppData", profileName);
                string appBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                ProfileFolder = Path.Combine(appBaseDirectory, "WebControl", "profiles", profileName);

                CoreWebView2EnvironmentOptions environmentOptions = new CoreWebView2EnvironmentOptions { AreBrowserExtensionsEnabled = true };
                CoreWebView2Environment environment = await CoreWebView2Environment.CreateAsync(null, ProfileFolder, environmentOptions);
                await webView.EnsureCoreWebView2Async(environment);
                return environment;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to initialize environment:\n{ex.Message}");
            }
        }
        private async Task<CoreWebView2Environment> Incognito_InitializeWebEnviromentAsync(WebView2 webView, string profileName)
        {
            try
            {
                _TempFolder = Path.Combine(Path.GetTempPath(), "EBWebView", "Incognito_" + Guid.NewGuid().ToString());
                Directory.CreateDirectory(_TempFolder);

                CoreWebView2EnvironmentOptions environmentOptions = new CoreWebView2EnvironmentOptions { AreBrowserExtensionsEnabled = true };

                CoreWebView2Environment environment = await CoreWebView2Environment.CreateAsync(null, _TempFolder, environmentOptions);

                CoreWebView2ControllerOptions options = environment.CreateCoreWebView2ControllerOptions();
                options.IsInPrivateModeEnabled = true;
                options.ProfileName = profileName;

                await webView.EnsureCoreWebView2Async(environment, options);
                //CoreWebView2Controller controller = await environment.CreateCoreWebView2ControllerAsync(webView.Handle, options);

                return environment;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to initialize environment:\n{ex.Message}");
            }
        }
        private async Task<CoreWebView2Environment> InitializeSharedWebEnviromentAsync(WebView2 webView, string FolderPath)
        {
            try
            {
                CoreWebView2EnvironmentOptions environmentOptions = new CoreWebView2EnvironmentOptions { AreBrowserExtensionsEnabled = true };
                CoreWebView2Environment environment = await CoreWebView2Environment.CreateAsync(null, FolderPath, environmentOptions);
                await webView.EnsureCoreWebView2Async(environment);
                return environment;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to initialize environment:\n{ex.Message}");
            }
        }
        private async void EnableMobileView(WebView2 webView, CoreWebView2Environment environment)
        {
            await webView.EnsureCoreWebView2Async(environment);
            webView.CoreWebView2.Settings.UserAgent = @"Mozilla/5.0 (Linux; Android 10; Mobile) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Mobile Safari/537.36";
        }
        private async void InitializeExtensions(WebView2 webView, CoreWebView2Environment environment, CoreWebView2Profile profile)
        {
            await webView.EnsureCoreWebView2Async(environment);
            CoreWebView2BrowserExtension extension = await AddExtensionsAsync(webView, environment, profile);
            await extension.EnableAsync(true);
        }
        private async Task<CoreWebView2BrowserExtension> AddExtensionsAsync(WebView2 webView, CoreWebView2Environment environment, CoreWebView2Profile profile)
        {
            try
            {
                CoreWebView2BrowserExtension extension = null;

                string localExtensionsPath = (!string.IsNullOrWhiteSpace(_TempFolder)) ?
                    Path.Combine(  _TempFolder, "EBWebView", "Default", "Extensions_Local") :
                    Path.Combine(ProfileFolder, "EBWebView", "Default", "Extensions_Local");

                if (!Directory.Exists(localExtensionsPath))
                {
                    Directory.CreateDirectory(localExtensionsPath);
                }

                foreach (string originalExtensionPath in ExtensionsPath)
                {
                    string extensionName = Path.GetFileName(originalExtensionPath);

                    string localExtensionPath = Path.Combine(localExtensionsPath, extensionName);

                    if (!Directory.Exists(localExtensionPath))
                    {
                        CopyDirectory(originalExtensionPath, localExtensionPath);
                    }

                    profile = profile ?? await GetProfile(webView, environment);
                    extension = await profile.AddBrowserExtensionAsync(localExtensionPath);
                }
                return extension;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load extensions:\n{ex.Message}");
            }
        }
        private async Task<IReadOnlyList<CoreWebView2BrowserExtension>> GetBrowserExtensionsAsync(WebView2 webView, CoreWebView2Environment environment, CoreWebView2Profile profile)
        {
            try
            {
                profile = profile != null ? profile : await GetProfile(webView, environment);
                IReadOnlyList<CoreWebView2BrowserExtension> extensions = await profile.GetBrowserExtensionsAsync();

                Debug.WriteLine("Installed Extensions:");
                foreach (var extension in extensions)
                {
                    Debug.WriteLine($"- {extension.Name}, ID: {extension.Id}");
                }
                return extensions;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving extensions:\n{ex.Message}");
            }
        }
        private void CopyDirectory(string sourceDir, string destinationDir)
        {
            Directory.CreateDirectory(destinationDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, destFile, overwrite: true);
            }

            foreach (var directory in Directory.GetDirectories(sourceDir))
            {
                string destDir = Path.Combine(destinationDir, Path.GetFileName(directory));
                CopyDirectory(directory, destDir);
            }
        }
    }
}
