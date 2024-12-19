using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WV2Service
{
    public partial class WebViewService : INotifyPropertyChanged
    {
        private WV2ServiceModel WebViewModel;

        public WebViewService() 
        {
            WebViewModel = new WV2ServiceModel();
        }
        public WebViewService(WebView2 _WebViewControl, string _ProfileName, List<string> _ExtensionsPath)
        {
            WebViewModel = new WV2ServiceModel();
            WebViewControl = _WebViewControl;
            ProfileName = _ProfileName;
            ExtensionsPath = _ExtensionsPath;
        }
        public WebViewService(CoreWebView2Profile _profile, string ProfileFolderPath) //SharedProfile e.g. 2 webcontrol 1 user profile
        {
            WebViewModel = new WV2ServiceModel();
            Profile = _profile;
            ProfileFolder = ProfileFolderPath;
        }
        public WebViewService(CoreWebView2Profile _profile, CoreWebView2Environment _environment) //SharedProfile e.g. 2 webcontrol 1 user profile
        {
            WebViewModel = new WV2ServiceModel();
            Profile = _profile;
            environment = _environment;
        }

        private string TempFolder { get; set; }
        public WebView2 WebViewControl
        {
            get { return WebViewModel.WebviewControl; }
            set
            {
                if (WebViewModel.WebviewControl != value)
                {
                    WebViewModel.WebviewControl = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public CoreWebView2Environment environment
        {
            get { return WebViewModel.Environment; }
            set
            {
                if (WebViewModel.Environment != value)
                {
                    WebViewModel.Environment = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public CoreWebView2Profile Profile
        {
            get { return WebViewModel.Profile; }
            set
            {
                if (WebViewModel.Profile != value)
                {
                    WebViewModel.Profile = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public List<string> ExtensionsPath
        {
            get => WebViewModel.ExtensionsPath;
            set
            {
                if (WebViewModel.ExtensionsPath != value)
                {
                    WebViewModel.ExtensionsPath = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string ProfileName
        {
            get => WebViewModel.ProfileName;
            set
            {
                if (WebViewModel.ProfileName != value)
                {
                    WebViewModel.ProfileName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string URL
        {
            get => WebViewModel.URL;
            set
            {
                if (WebViewModel.URL != value)
                {
                    WebViewModel.URL = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string ProfileFolder
        {
            get { return WebViewModel.ProfileFolder; }
            set
            {
                if (WebViewModel.ProfileFolder != value)
                {
                    WebViewModel.ProfileFolder = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
   
}
