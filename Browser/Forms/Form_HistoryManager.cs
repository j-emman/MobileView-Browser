using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WV2Service;

namespace MobileView
{
    public partial class Form_HistoryManager : Form
    {
        private readonly WebViewService Browser;
        private DataTable HistoryTable;
        public Form_HistoryManager(WebViewService _webViewService, Form? currentform = null)
        {
            InitializeComponent();
            Browser = _webViewService;
            if (currentform != null)
            {
                PreserveCurrentFormLocation(currentform);
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

        private async void Form_HistoryManager_Shown(object sender, EventArgs e)
        {
            try
            {
                HistoryTable = await Browser.ReadHistory(Browser.ProfileFolder);
                dataGridView1.DataSource = HistoryTable;
                dataGridView1.Columns["Visit Count"].Visible = false;
                dataGridView1.Columns["URL"].Visible = false;
                dataGridView1.Columns["ID"].Visible = false;
            }
            catch
            {

            }
        }
    }
}
