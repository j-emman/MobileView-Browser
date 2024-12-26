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
        private bool incognito;
        private string url;

        public Form_Main(bool _incognito = false, Form? currentForm = null, string? _url = null)
        {
            InitializeComponent();

            incognito = _incognito;
            url = _url;
            titleBar = new TitleBar
            (
                parentForm: this,
                panel: TitleBarPanel,
                closeButton: CloseButton,
                minimizeButton: MinimizeButton
            );

            if (_incognito)
            {
                InitializeIncognito(currentForm);
                return;
            }
            if (!string.IsNullOrWhiteSpace(url))
            {
                Browser = new WebViewService();
                Browser.WebViewControl = WebView21;
                Browser.InitializeWebViewNewTab();
                return;
            }
            InitializeBrowser(currentForm);

            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.ControlBox = false;
        }
        private void InitializeIncognito(Form currentForm)
        {
            this.Text = "Private";
            PreserveCurrentFormLocation(currentForm);

            Browser = new WebViewService
            {
                ProfileName = "User1",
                WebViewControl = WebView21,
                ExtensionsPath = new List<string> { @"C:\Users\admin\Documents\Training\Misc\Browser_Extensions\uBlock0" }
            };
            Browser.PropertyChanged += WebView_PropertyChanged;
            Browser.Incognito_InitializeWebView();

        }
        private void InitializeBrowser(Form? currentForm = null)
        {
            if (currentForm != null) { PreserveCurrentFormLocation(currentForm); }
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
            //this.DataBindings.Add("Text", Browser, nameof(Browser.SiteTitle));
        }
        private void PreserveCurrentFormLocation(Form currentForm)
        {
            var state = currentForm.WindowState;
            var location = currentForm.Location;

            this.WindowState = state;
            this.StartPosition = FormStartPosition.Manual;

            var centerX = location.X + (currentForm.Width - this.Width) / 2;
            var centerY = location.Y + (currentForm.Height - this.Height) / 2;
            this.Location = new Point(centerX, centerY);
        }
        private void NewWindow(string link)
        {
            Form newWindow = new Form_Main(_url: link);
            newWindow.Show();
        }
        private void OnNewWindowRequested(object? sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            var newWebView = e.NewWindow;
            DialogResult result = MessageBox.Show($"A website wants to open a new window:\n{e.Uri}\n\nAllow this popup?", "Confirm Popup", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                NewWindow(e.Uri);
                e.Handled = false;
                return;
            }
            e.Handled = true;
        }
        private void WebView_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Browser.URL)) { URLTextBox.Text = Browser.URL; } //add a oneway binding to URL
        }
        private async void Form_Main_Shown(object sender, EventArgs e)
        {
            if (incognito) { await Task.Delay(1000); }
            if (!string.IsNullOrWhiteSpace(url)) { Browser.Navigation.NewTabGoTo(url); return; }
            Browser.Navigation.GoTo("google.com");
        }
        private void Form_Main_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Normal)
            { return; }

            const double aspectRatio = 9.0 / 16.0;
            int newWidth = this.Width;
            int newHeight = (int)(newWidth / aspectRatio);

            if (this.Height != newHeight)
            {
                this.Height = newHeight;
            }
        }
        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (incognito)
            {
                Browser.Navigation.Incognito_DisposeSession();
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
        private void ClearAllBrowsingDataToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void URLTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.Focus();
                textBox.SelectAll();
            }
        }
        private void IncognitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form incognito = new Form_Main(_incognito: true, currentForm: this);
            this.Hide();
            incognito.ShowDialog();
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
                PreserveCurrentFormLocation(historyManager);
            }
            this.Show();
            MenuButton.PerformClick();
            Browser.WebViewControl.Focus();
        }
    }
}
