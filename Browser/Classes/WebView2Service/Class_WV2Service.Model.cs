using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.ComponentModel;

namespace WV2Service
{
    public partial class WebViewService
    {
        private class WV2ServiceModel()
        {
            public string ProfileName { get; set; }
            public string URL { get; set; }
            public string ProfileFolder { get; set; }
            public List<string> ExtensionsPath { get; set; }
            public CoreWebView2Environment Environment { get; set; }
            public CoreWebView2Profile Profile { get; set; }
            public WebView2 WebviewControl { get; set; }
        }
    }
}
