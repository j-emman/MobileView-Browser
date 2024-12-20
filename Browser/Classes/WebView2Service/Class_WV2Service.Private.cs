using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace WV2Service
{
    public partial class WebViewService
    {
        private async Task<CoreWebView2Profile> GetProfile(WebView2 webView, CoreWebView2Environment environment)
        {
            await webView.EnsureCoreWebView2Async(environment);
            return  webView.CoreWebView2.Profile;
        }
        private async Task<CoreWebView2Environment> InitializeWebEnviromentAsync(WebView2 webView, string profileName)
        {
            //string userDataFolder = Path.Combine(
            //            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            //            "MyWebView2AppData",
            //            profileName);

            string appBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ProfileFolder = Path.Combine(appBaseDirectory, "WebControl", "profiles", profileName);


            var environmentOptions = new CoreWebView2EnvironmentOptions
            {
                AreBrowserExtensionsEnabled = true
            };

            var environment = await CoreWebView2Environment.CreateAsync(null, ProfileFolder, environmentOptions);
            await webView.EnsureCoreWebView2Async(environment);
            return environment;
        }
        private async Task<CoreWebView2Environment> InitializeSharedWebEnviromentAsync(WebView2 webView, string FolderPath)
        {
            var environmentOptions = new CoreWebView2EnvironmentOptions
            {
                AreBrowserExtensionsEnabled = true
            };

            var environment = await CoreWebView2Environment.CreateAsync(null, FolderPath, environmentOptions);
            await webView.EnsureCoreWebView2Async(environment);
            return environment;
        }
        private async Task<CoreWebView2Environment> Incognito_InitializeWebEnviromentAsync(WebView2 webView, string profileName)
        {
            _TempFolder = Path.Combine(Path.GetTempPath(), "Incognito_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(_TempFolder);

            var environmentOptions = new CoreWebView2EnvironmentOptions
            {
                AreBrowserExtensionsEnabled = true
            };

            var environment = await CoreWebView2Environment.CreateAsync(null, _TempFolder, environmentOptions);
            await webView.EnsureCoreWebView2Async(environment);
            return environment;
        }
        private async Task<CoreWebView2BrowserExtension> AddExtensionsAsync(WebView2 webView, CoreWebView2Environment environment, CoreWebView2Profile profile)
        {
            CoreWebView2BrowserExtension extension = null;
            try
            {
                profile = profile != null ? profile : await GetProfile(webView, environment);
                foreach (string extensionPath in ExtensionsPath)
                {
                    extension = await profile.AddBrowserExtensionAsync(extensionPath);
                }

                return extension;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load extensions: {ex.Message}");
            }
            return extension;
        }
        private async Task<IReadOnlyList<CoreWebView2BrowserExtension>> GetBrowserExtensionsAsync(WebView2 webView, CoreWebView2Environment environment, CoreWebView2Profile profile)
        {
            try
            {

                profile = profile != null ? profile : await GetProfile(webView, environment);
                IReadOnlyList<CoreWebView2BrowserExtension> extensions = await profile.GetBrowserExtensionsAsync();
                // Display extensions or handle them as needed
                Console.WriteLine("Installed Extensions:");
                foreach (var extension in extensions)
                {
                    Console.WriteLine($"- {extension.Name}, ID: {extension.Id}");
                }
                return extensions;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving extensions: {ex.Message}");
            }
            return null;
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
            extension.RemoveAsync();
        }
        private async void ClearAllBrowsingData(WebView2 webView, CoreWebView2Environment environment, CoreWebView2Profile profile)
        {
            profile = profile != null? profile : await GetProfile(webView, environment);
            //await webView.EnsureCoreWebView2Async(environment);
            await profile.ClearBrowsingDataAsync(
                CoreWebView2BrowsingDataKinds.Cookies |
                CoreWebView2BrowsingDataKinds.BrowsingHistory |
                CoreWebView2BrowsingDataKinds.GeneralAutofill |
                CoreWebView2BrowsingDataKinds.PasswordAutosave |
                CoreWebView2BrowsingDataKinds.ServiceWorkers |
                CoreWebView2BrowsingDataKinds.CacheStorage |
                CoreWebView2BrowsingDataKinds.DownloadHistory |
                CoreWebView2BrowsingDataKinds.DiskCache);
        }
        private async void ClearAllBrowserData(WebView2 webView, CoreWebView2Environment environment, CoreWebView2Profile profile)
        {
            profile = profile != null ? profile : await GetProfile(webView, environment);
            await webView.EnsureCoreWebView2Async(environment);
            await profile.ClearBrowsingDataAsync();
            webView.Reload();
        }
        private async void ClearBrowserData(WebView2 webView, CoreWebView2Environment environment, CoreWebView2Profile profile, CoreWebView2BrowsingDataKinds dataKinds)
        {
            profile = profile != null ? profile : await GetProfile(webView, environment);
            //await webView.EnsureCoreWebView2Async(environment);
            await profile.ClearBrowsingDataAsync(dataKinds);
            webView.Reload();
        }
        private async void ClearBrowsingDataBetweenDateRange(WebView2 webView, CoreWebView2Environment environment, CoreWebView2Profile profile, DateTime startDate, DateTime endDate)
        {
            profile = profile != null ? profile : await GetProfile(webView, environment);
            //await webView.EnsureCoreWebView2Async(environment);
            await profile.ClearBrowsingDataAsync(
                CoreWebView2BrowsingDataKinds.Cookies |
                CoreWebView2BrowsingDataKinds.BrowsingHistory |
                CoreWebView2BrowsingDataKinds.GeneralAutofill |
                CoreWebView2BrowsingDataKinds.PasswordAutosave |
                CoreWebView2BrowsingDataKinds.ServiceWorkers |
                CoreWebView2BrowsingDataKinds.CacheStorage |
                CoreWebView2BrowsingDataKinds.DownloadHistory |
                CoreWebView2BrowsingDataKinds.DiskCache, startDate, endDate);
            webView.Reload();
        }
        private string EnsureHttpsPrefix(string url)
        {
            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase) &&
                !url.StartsWith("edge://", StringComparison.OrdinalIgnoreCase))
            {
                url = "https://" + url;
            }
            return url;
        }
        private bool IsURLSuffixValid(string url)
        {
            string[] validTLDs = { ".com", ".org", ".net", ".edu", ".gov", ".io", ".co", ".us", ".uk", ".ph" };
            if (url.StartsWith("edge://", StringComparison.OrdinalIgnoreCase))  { return true; }
            foreach (string tld in validTLDs)
            {
                if (url.Contains(tld, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
        private async void NavigateTo(WebView2 webView, CoreWebView2Environment environment, string address)
        {
            await webView.EnsureCoreWebView2Async(environment);
            webView.CoreWebView2.Navigate(address);
        }
        private async void EnableNewWindowRequest(WebView2 webView, CoreWebView2Environment environment)
        {
            await webView.EnsureCoreWebView2Async(environment);
            webView.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
        }
        private async void EnableNavigationMonitoring(WebView2 webView, CoreWebView2Environment environment)
        {
            await webView.EnsureCoreWebView2Async(environment);
            webView.NavigationStarting += OnNavigationStarting;
            webView.NavigationCompleted += OnNavigationCompleted;
        }

        public event EventHandler<CoreWebView2NewWindowRequestedEventArgs> NewWindowRequested;
        private void CoreWebView2_NewWindowRequested(object? sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            NewWindowRequested?.Invoke(sender, e);

            if (NewWindowRequested == null)
            {
                DialogResult result = MessageBox.Show( $"A website wants to open a new window:\n{e.Uri}\n\nAllow this popup?", "Confirm Popup",  MessageBoxButtons.YesNo, MessageBoxIcon.Question );

                if (result == DialogResult.Yes)
                {
                    e.Handled = false;
                    //// Optional: Open in the current WebView
                    //NavigateTo(webviewControl, environment, e.Uri);
                    return;
                }
                e.Handled = true;
                Console.WriteLine($"Popup blocked: {e.Uri}");
            }
        }
        public event EventHandler<string> NavigationChanged;
        private void OnNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                NavigationChanged?.Invoke(this, WebViewControl.Source.ToString());
            }
            else
            {
                NavigationChanged?.Invoke(this, "Navigation failed.");
            }
        } 
        private void OnNavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
        {
            URL = e.Uri;
            NavigationChanged?.Invoke(this, $"Navigating to: {e.Uri}");
        }
    }
}
