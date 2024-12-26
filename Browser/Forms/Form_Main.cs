using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.ComponentModel;
using System.Xml.Serialization;
using WV2Service;
using MobileView.Classes;

namespace MobileView
{
    public partial class Form_Main : Form
    {
        private WebViewService Browser;
        private TitleBar titleBar;
        private FormManager formManager;
        private bool incognito;
        private bool newWindow;
        private string url;
        private string borderUsed;

        public Form_Main(bool _incognito = false, Form? currentForm = null, string? _url = null, string? profileFolder = null)
        {
            InitializeComponent();

            incognito = _incognito;
            url = _url;
            formManager = new FormManager(this);
            titleBar = new TitleBar
            (
                parentForm: this,
                panel: TitleBarPanel,
                formLabel: FormTextLabel,
                closeButton: CloseButton,
                minimizeButton: MinimizeButton
            );

            formManager.PreserveCurrentFormLocationAndSize(currentForm);
            EnableBorderlessWindows();

            if (_incognito)
            {
                InitializeIncognito();
                return;
            }
            if (!string.IsNullOrWhiteSpace(url))
            {
                InitializeNewWindow(profileFolder);
                return;
            }
            InitializeBrowser();
        }
        private void EnableBorderlessWindows()
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
            this.ControlBox = false;
        }
        private void InitializeIncognito()
        {
            FormTextLabel.Text = "Private";
            Browser = new WebViewService
            {
                ProfileName = "User1",
                WebViewControl = WebView21,
                ExtensionsPath = new List<string> { @"C:\Users\admin\Documents\Training\Misc\Browser_Extensions\uBlock0" }
            };
            Browser.PropertyChanged += WebView_PropertyChanged;
            Browser.Incognito_InitializeWebView();
        }
        private void InitializeBrowser()
        {
            Browser = new WebViewService
            {
                ProfileName = "User1",
                WebViewControl = WebView21,
                ExtensionsPath = new List<string>
                {
                    @"C:\Users\admin\Documents\Training\Misc\Browser_Extensions\uBlock0",
                    @"C:\Users\admin\Documents\Training\Misc\Browser_Extensions\privacy_badger"
                }
            };
            Browser.PropertyChanged += WebView_PropertyChanged;
            Browser.NewWindowRequested += OnNewWindowRequested;
            Browser.InitializeWebView();
            FormTextLabel.DataBindings.Add("Text", Browser, nameof(Browser.SiteTitle));
        }
        private void InitializeNewWindow(string profileFolder)
        {
            if (profileFolder == null) { return; }
            newWindow = true;
            MenuButton.Enabled = false;
            Browser = new WebViewService();
            Browser.WebViewControl = WebView21;
            Browser.InitializeWebViewNewTab(profileFolder);
            FormTextLabel.DataBindings.Add("Text", Browser, nameof(Browser.SiteTitle));
        }
        private void OpenNewWindow(string link)
        {
            Form newWindow = new Form_Main(currentForm: this, _url: link, profileFolder: Browser.ProfileFolder);
            newWindow.Show();
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;

            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST)
            {
                formManager.HandleWndProc(ref m);
            }
        }
        private void OnNewWindowRequested(object? sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            var newWebView = e.NewWindow;
            DialogResult result = MessageBox.Show($"A website wants to open a new window:\n{e.Uri}\n\nAllow this popup?", "Confirm Popup", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                e.Handled = true;
                OpenNewWindow(e.Uri);
                return;
            }
            e.Handled = true;
        }
        private void WebView_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Browser.URL)) { URLTextBox.Text = Browser.URL; } //add a oneway binding to URL
        }
        private async void Form_Main_Load(object sender, EventArgs e)
        {
            if (incognito) { await Task.Delay(1000); }
            if (!string.IsNullOrWhiteSpace(url)) { Browser.Navigation.NewTabGoTo(url); return; }
            Browser.Navigation.GoTo("google.com");
        }
        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (incognito)
            {
                Browser.Navigation.Incognito_DisposeSession();
            }
            if (incognito || newWindow)
            {
                this.Hide();
                this.Dispose();
                return;
            }
            Application.Exit();
        }
        private void ReloadButton_Click(object sender, EventArgs e)
        {
            Browser.Navigation.Reload();
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            Browser.Navigation.GoBack();
        }
        private void ClearAllBrowserDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Browser.Clear.AllBrowserData();
        }
        private void ClearAllBrowsingDataToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Browser.Clear.AllBrowsingData();
        }
        private void URLTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                WebView21.Focus();
                Browser.Navigation.GoTo(URLTextBox.Text);
            }
        }
        private void MenuButton_Click(object sender, EventArgs e)
        {
            MenuPanel.Visible = !MenuPanel.Visible;
        }
        private void URLTextBox_DoubleClick(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Focus();
            textBox.SelectAll();
        }
        private void IncognitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form incognito = new Form_Main(_incognito: true, currentForm: this);
            this.Hide();
            incognito.ShowDialog();
            formManager.PreserveCurrentFormLocationAndSize(incognito);
            this.Show();
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private async void ViewExtensionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> extensions = await Browser.GetExtensions();
            string extensionstring = string.Join(",\n", extensions);
            MessageBox.Show(extensionstring);
        }
        private void RemoveExtensionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void AddExtensionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void ViewHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form_HistoryManager historyManager = new Form_HistoryManager(Browser, this))
            {
                this.Hide();
                historyManager.ShowDialog();
                formManager.PreserveCurrentFormLocationAndSize(historyManager);
            }
            this.Show();
            MenuButton.PerformClick();
            Browser.WebViewControl.Focus();
        }
    }
}
