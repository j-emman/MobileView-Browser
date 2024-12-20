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
        public void InitializeWebViewNewTab()
        {
            InitializeSharedEnviroment();
            GetProfile();
            EnableMobileView();
            InitializeExtensions();
            EnableNewWindowRequest();
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
            environment = await CoreWebView2Environment.CreateAsync();
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
    }
}
