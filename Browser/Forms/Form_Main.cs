using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.ComponentModel;
using WV2Service;

namespace Browser
{
    public partial class Form_Main : Form
    {
        private readonly CoreWebView2Environment environment;
        private WebViewService Browser;
        private WebViewService Browser2;
        private bool incognito;

        public Form_Main(bool _incognito, Form currentForm = null)
        {
            incognito = _incognito;
            InitializeComponent();
            if (_incognito)
            {
                this.Text = "Private";
                PreserveCurrentFormLocation(currentForm);

                Browser = new WebViewService();
                Browser.PropertyChanged += WebView_PropertyChanged;
                Browser.WebViewControl = WebView21;
                Browser.ExtensionsPath = new List<string>
                {
                    @"C:\Users\admin\Documents\Training\Misc\Browser_Extensions\uBlock0"
                };
                Browser.ProfileName = "User1";
                Browser.Incognito_InitializeMobileWebView();
                return;
            }
            Browser = Globals.Instances.Browser;
            Browser.PropertyChanged += WebView_PropertyChanged;
            Browser.WebViewControl = WebView21;
            Browser.ExtensionsPath = new List<string>
            {
                @"C:\Users\admin\Documents\Training\Misc\Browser_Extensions\uBlock0",
                @"C:\Users\admin\Documents\Training\Misc\Browser_Extensions\privacy_badger"
            };
            Browser.ProfileName = "User1";
            Browser.InitializeMobileWebView();
        }

        private void TestNewControl()
        {
            //Browser2 = new WebViewService(Browser.Profile, Browser.ProfileFolder);
            //Browser2.PropertyChanged += WebView_PropertyChanged;
            //Browser.WebViewControl = webView22;
            //Browser2.InitializeSharedEnviroment();
            //Browser.NavigateTo("https://www.google.com");
            //return;
        }
        private void WebView_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Browser.URL))
            {
                URLTextBox.Text = Browser.URL;
            }
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
        private async void Form_Main_Shown(object sender, EventArgs e)
        {
            if (incognito) { await Task.Delay(1000); }
            Browser.NavigateTo("https://www.google.com");
            TestNewControl();
        }
        private void Form1_Resize(object sender, EventArgs e)
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
            Browser.Incognito_DisposeSession();
            this.Hide();
            this.Dispose();
            if (incognito) { return; }
            Application.Exit();
        }
        private void ReloadButton_Click(object sender, EventArgs e)
        {
            Browser.Reload();
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            Browser.GoBack();
        }
        private void URLTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                WebView21.Focus();
                Browser.NavigateTo(URLTextBox.Text);
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
        private void URLTextBox_DoubleClick(object sender, EventArgs e)
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
            Form incognito = new Form_Main(true, this);
            this.Hide();
            incognito.ShowDialog();
            this.Show();
        }

    }
}
