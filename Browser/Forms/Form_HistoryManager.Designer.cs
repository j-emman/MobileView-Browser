
namespace MobileView
{
    partial class Form_HistoryManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_HistoryManager));
            TopBarPanel = new Panel();
            MenuButton = new Button();
            BackButton = new Button();
            ReloadButton = new Button();
            URLTextBox = new TextBox();
            dataGridView1 = new DataGridView();
            TopBarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // TopBarPanel
            // 
            TopBarPanel.BackColor = Color.FromArgb(33, 33, 33);
            TopBarPanel.Controls.Add(MenuButton);
            TopBarPanel.Controls.Add(BackButton);
            TopBarPanel.Controls.Add(ReloadButton);
            TopBarPanel.Controls.Add(URLTextBox);
            TopBarPanel.Dock = DockStyle.Top;
            TopBarPanel.Location = new Point(0, 0);
            TopBarPanel.Name = "TopBarPanel";
            TopBarPanel.Size = new Size(312, 31);
            TopBarPanel.TabIndex = 2;
            // 
            // MenuButton
            // 
            MenuButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MenuButton.BackgroundImage = Properties.Resources.menu_25dp_FFFFFF;
            MenuButton.BackgroundImageLayout = ImageLayout.Stretch;
            MenuButton.FlatAppearance.BorderColor = Color.FromArgb(33, 33, 33);
            MenuButton.FlatStyle = FlatStyle.Flat;
            MenuButton.Location = new Point(282, 3);
            MenuButton.Name = "MenuButton";
            MenuButton.Size = new Size(25, 26);
            MenuButton.TabIndex = 3;
            MenuButton.UseVisualStyleBackColor = true;
            // 
            // BackButton
            // 
            BackButton.BackgroundImage = Properties.Resources.arrow_back_25dp_FFFFFF;
            BackButton.BackgroundImageLayout = ImageLayout.Stretch;
            BackButton.FlatAppearance.BorderColor = Color.FromArgb(33, 33, 33);
            BackButton.FlatStyle = FlatStyle.Flat;
            BackButton.Location = new Point(5, 2);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(27, 26);
            BackButton.TabIndex = 2;
            BackButton.UseVisualStyleBackColor = true;
            // 
            // ReloadButton
            // 
            ReloadButton.BackgroundImage = Properties.Resources.refresh_35dp;
            ReloadButton.BackgroundImageLayout = ImageLayout.Stretch;
            ReloadButton.FlatAppearance.BorderColor = Color.FromArgb(33, 33, 33);
            ReloadButton.FlatStyle = FlatStyle.Flat;
            ReloadButton.Location = new Point(36, 2);
            ReloadButton.Name = "ReloadButton";
            ReloadButton.Size = new Size(25, 26);
            ReloadButton.TabIndex = 1;
            ReloadButton.UseVisualStyleBackColor = true;
            // 
            // URLTextBox
            // 
            URLTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            URLTextBox.BackColor = Color.FromArgb(23, 23, 23);
            URLTextBox.BorderStyle = BorderStyle.FixedSingle;
            URLTextBox.ForeColor = Color.White;
            URLTextBox.Location = new Point(65, 4);
            URLTextBox.Name = "URLTextBox";
            URLTextBox.Size = new Size(211, 23);
            URLTextBox.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 31);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(312, 533);
            dataGridView1.TabIndex = 3;
            // 
            // Form_HistoryManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(33, 33, 33);
            ClientSize = new Size(312, 564);
            Controls.Add(dataGridView1);
            Controls.Add(TopBarPanel);
            ForeColor = SystemColors.HighlightText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form_HistoryManager";
            Shown += Form_HistoryManager_Shown;
            TopBarPanel.ResumeLayout(false);
            TopBarPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }


        #endregion

        private Panel TopBarPanel;
        private Button MenuButton;
        private Button BackButton;
        private Button ReloadButton;
        private TextBox URLTextBox;
        private DataGridView dataGridView1;
    }
}