using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WV2Service
{
    public partial class WebViewService
    {
        public class NavigationManager(WebViewService webviewService)
        {
            private readonly WebViewService wv2service = webviewService;
            public void GoTo(string address)
            {
                if (wv2service.IsURLSuffixValid(address))
                {
                    wv2service.URL = wv2service.EnsureHttpsPrefix(address);
                    wv2service.NavigateTo(wv2service.WebViewControl, wv2service.environment, wv2service.URL);
                    return;
                }

                string searchQuery = Uri.EscapeDataString(address);
                string searchUrl = "https://www.google.com/search?q=" + searchQuery;

                wv2service.URL = (new Uri(searchUrl)).ToString();
                wv2service.NavigateTo(wv2service.WebViewControl, wv2service.environment, wv2service.URL);
            }
            public void NewTabGoTo(string address)
            {
                if (wv2service.IsURLSuffixValid(address))
                {
                    //address = EnsureHttpsPrefix(address);
                    //WebViewControl.EnsureCoreWebView2Async();
                    //WebViewControl.Source = new Uri(address);
                    wv2service.URL = wv2service.EnsureHttpsPrefix(address);
                    wv2service.NavigateTo(wv2service.WebViewControl, wv2service.environment, wv2service.URL);
                    return;
                }

                string searchQuery = Uri.EscapeDataString(address);
                string searchUrl = "https://www.google.com/search?q=" + searchQuery;

                wv2service.WebViewControl.EnsureCoreWebView2Async();
                wv2service.WebViewControl.Source = new Uri(searchUrl);
            }
            public void Reload()
            {
                wv2service.WebViewControl.Reload();
            }
            public void GoBack()
            {
                if (wv2service.WebViewControl.CanGoBack) wv2service.WebViewControl.GoBack();
            }
            public void GoForward()
            {
                if (wv2service.WebViewControl.CanGoForward) wv2service.WebViewControl.GoForward();
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
                            if (!Directory.Exists(wv2service._TempFolder)) { return; }
                            Directory.Delete(wv2service._TempFolder, true);
                        });
                    }
                    catch
                    {
                        await Task.Delay(delayMilliseconds * attempt);
                    }
                }
            }
        }
    }
}
