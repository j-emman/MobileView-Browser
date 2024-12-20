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
        public void InitializeMobileWebView()
        {
            InitializeEnviroment();
            InitializeProfile();
            EnableMobileView();
            InitializeExtensions();
            EnableNewWindowRequest();
            EnableNavigationMonitoring();
        }
        public void InitializeWebViewNewTab()
        {
            InitializeSharedEnviroment();
            InitializeProfile();
            EnableMobileView();
            InitializeExtensions();
            EnableNewWindowRequest();
        }
        public void InitializeSharedMobileWebView()
        {
            InitializeSharedEnviroment();
            InitializeProfile();
            EnableMobileView();
            InitializeExtensions();
            EnableNewWindowRequest();
        }
        public void Incognito_InitializeMobileWebView()
        {
            Incognito_InitializeEnviroment();
            InitializeExtensions();
            InitializeProfile();
            EnableMobileView();
            EnableNewWindowRequest();
        }
        public async void InitializeEnviroment()
        {
            environment = await InitializeWebEnviromentAsync(WebViewControl, ProfileName);
        }
        public async void InitializeSharedEnviroment()
        {
            environment = await CoreWebView2Environment.CreateAsync();
        }
        public async void InitializeProfile()
        {
            Profile = await GetProfile(WebViewControl, environment);
        }
        public async void Incognito_InitializeEnviroment()
        {
            environment = await Incognito_InitializeWebEnviromentAsync(WebViewControl, "Incognito");
        }
        public void EnableMobileView()
        {
            EnableMobileView(WebViewControl, environment);
        }
        public void InitializeExtensions()
        {
            InitializeExtensions(WebViewControl, environment, Profile);
        }
        public void EnableNewWindowRequest()
        {
            EnableNewWindowRequest(WebViewControl, environment);
        }
        public void EnableNavigationMonitoring()
        {
            EnableNavigationMonitoring(WebViewControl, environment);
        }
        public async Task<List<string>> GetExtensions()
        {
            List<string> extensions = new List<string>();

            IReadOnlyList<CoreWebView2BrowserExtension> extensionsList =  await GetBrowserExtensionsAsync(WebViewControl, environment, Profile);

            foreach (var extension in extensionsList)
            {
                extensions.Add(extension.Name);
                Console.WriteLine($"- {extension.Name}, ID: {extension.Id}");
            }

            return extensions;
        }
    }
}
