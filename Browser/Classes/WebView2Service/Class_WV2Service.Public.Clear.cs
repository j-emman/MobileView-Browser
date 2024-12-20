using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WV2Service
{
    public partial class WebViewService
    {
        public class ClearManager
        {
            private readonly WebViewService webViewService;
            public ClearManager(WebViewService webservice) 
            {
                webViewService = webservice;
            }
            public void AllBrowsingData()
            {
                webViewService.ClearAllBrowsingData(webViewService.WebViewControl, webViewService.environment, webViewService.Profile);
            }
            public void AllBrowsingDataBetweenDates(DateTime startDate, DateTime endDate)
            {
                webViewService.ClearBrowsingDataBetweenDateRange(webViewService.WebViewControl, webViewService.environment, webViewService.Profile, startDate, endDate);
            }
            public void BrowserData(BrowsingDataKinds dataKind)
            {
                CoreWebView2BrowsingDataKinds _dataKind = EnumMapper.BrowsingDataKindMap[dataKind];
                webViewService.ClearBrowserData(webViewService.WebViewControl, webViewService.environment, webViewService.Profile, _dataKind);
            }
            public void AllBrowserData()
            {
                webViewService.ClearAllBrowserData(webViewService.WebViewControl, webViewService.environment, webViewService.Profile);
            }
        }
    }
}
