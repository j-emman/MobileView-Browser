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
            GetProfile();
            EnableMobileView();
            InitializeExtensions();
            EnableNewWindowRequest();
            EnableNavigationMonitoring();
        }
        public void InitializeSharedMobileWebView()
        {
            InitializeSharedEnviroment();
            GetProfile();
            EnableMobileView();
            InitializeExtensions();
            EnableNewWindowRequest();
        }
        public void Incognito_InitializeMobileWebView()
        {
            Incognito_InitializeEnviroment();
            GetProfile();
            EnableMobileView();
            InitializeExtensions();
            EnableNewWindowRequest();
        }
        public async void InitializeEnviroment()
        {
            environment = await InitializeWebEnviromentAsync(WebViewControl, ProfileName);
        }
        public async void InitializeSharedEnviroment()
        {
            environment = await InitializeSharedWebEnviromentAsync(WebViewControl, ProfileFolder);
        }
        public async void GetProfile()
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
        public void ClearAllBrowsingData()
        {
            ClearAllBrowsingData(WebViewControl, environment, Profile);
        }
        public void ClearAllBrowsingDataBetweenDates(DateTime startDate, DateTime endDate)
        {
            ClearBrowsingDataBetweenDateRange(WebViewControl, environment, Profile, startDate, endDate);
        }
        public void ClearBrowserData(BrowsingDataKinds dataKind)
        {
            CoreWebView2BrowsingDataKinds _dataKind = EnumMapper.BrowsingDataKindMap[dataKind];
            ClearBrowserData(WebViewControl, environment, Profile, _dataKind);
        }
        public void ClearAllBrowserData()
        {
            ClearAllBrowserData(WebViewControl, environment, Profile);
        }
        public void NavigateTo(string address)
        {
            if (IsURLSuffixValid(address))
            {
                URL = EnsureHttpsPrefix(address);
                NavigateTo(WebViewControl, environment, URL);
                return;
            }

            string searchQuery = Uri.EscapeDataString(address);
            string searchUrl = "https://www.google.com/search?q=" + searchQuery;

            URL = (new Uri(searchUrl)).ToString();
            NavigateTo(WebViewControl, environment, URL);
        }
        public async void Incognito_DisposeSession()
        {
            int maxRetries = 5;
            int delayMilliseconds = 2000;
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    //ensure all processes are closed first
                    await Task.Run(() =>
                    {
                        if (!Directory.Exists(TempFolder)) { return; }
                        Directory.Delete(TempFolder, true);
                    });
                }
                catch
                {
                    await Task.Delay(delayMilliseconds * attempt);
                }
            }
        }
        public void Reload()
        {
            WebViewControl.Reload();
        }
        public void GoBack()
        {
            if (WebViewControl.CanGoBack) WebViewControl.GoBack();
        }
        public void GoForward()
        {
            if (WebViewControl.CanGoForward) WebViewControl.GoForward();
        }
    }
}
