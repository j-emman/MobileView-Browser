namespace MobileView
{
    partial class Form_Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            WebView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            TopBarPanel = new Panel();
            MenuButton = new Button();
            BackButton = new Button();
            ReloadButton = new Button();
            URLTextBox = new TextBox();
            MenuPanel = new Panel();
            MenuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            incognitoToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            extensionsToolStripMenuItem1 = new ToolStripMenuItem();
            profileToolStripMenuItem = new ToolStripMenuItem();
            bookmarksToolStripMenuItem = new ToolStripMenuItem();
            IncognitoToolStripMenuItem1 = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)WebView21).BeginInit();
            TopBarPanel.SuspendLayout();
            MenuPanel.SuspendLayout();
            MenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // WebView21
            // 
            WebView21.AllowExternalDrop = true;
            WebView21.CreationProperties = null;
            WebView21.DefaultBackgroundColor = Color.White;
            WebView21.Dock = DockStyle.Fill;
            WebView21.Location = new Point(0, 55);
            WebView21.Name = "WebView21";
            WebView21.Size = new Size(312, 509);
            WebView21.TabIndex = 0;
            WebView21.ZoomFactor = 1D;
            // 
            // TopBarPanel
            // 
            TopBarPanel.BackColor = Color.FromArgb(33, 33, 33);
            TopBarPanel.Controls.Add(MenuButton);
            TopBarPanel.Controls.Add(BackButton);
            TopBarPanel.Controls.Add(ReloadButton);
            TopBarPanel.Controls.Add(URLTextBox);
            TopBarPanel.Dock = DockStyle.Top;
            TopBarPanel.Location = new Point(0, 24);
            TopBarPanel.Name = "TopBarPanel";
            TopBarPanel.Size = new Size(312, 31);
            TopBarPanel.TabIndex = 1;
            // 
            // MenuButton
            // 
            MenuButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MenuButton.BackgroundImage = Properties.Resources.menu_25dp_FFFFFF;
            MenuButton.BackgroundImageLayout = ImageLayout.Stretch;
            MenuButton.FlatAppearance.BorderColor = Color.FromArgb(33, 33, 33);
            MenuButton.FlatStyle = FlatStyle.Flat;
            MenuButton.Location = new Point(283, 3);
            MenuButton.Name = "MenuButton";
            MenuButton.Size = new Size(25, 26);
            MenuButton.TabIndex = 3;
            MenuButton.UseVisualStyleBackColor = true;
            MenuButton.Click += MenuButton_Click;
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
            BackButton.Click += BackButton_Click;
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
            ReloadButton.Click += ReloadButton_Click;
            // 
            // URLTextBox
            // 
            URLTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            URLTextBox.BackColor = Color.FromArgb(23, 23, 23);
            URLTextBox.BorderStyle = BorderStyle.FixedSingle;
            URLTextBox.ForeColor = Color.White;
            URLTextBox.Location = new Point(65, 4);
            URLTextBox.Name = "URLTextBox";
            URLTextBox.Size = new Size(214, 23);
            URLTextBox.TabIndex = 0;
            URLTextBox.DoubleClick += URLTextBox_DoubleClick;
            URLTextBox.KeyDown += URLTextBox_KeyDown;
            URLTextBox.MouseDown += URLTextBox_MouseDown;
            // 
            // MenuPanel
            // 
            MenuPanel.AutoSize = true;
            MenuPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            MenuPanel.Controls.Add(MenuStrip);
            MenuPanel.Dock = DockStyle.Top;
            MenuPanel.Location = new Point(0, 0);
            MenuPanel.Name = "MenuPanel";
            MenuPanel.Size = new Size(312, 24);
            MenuPanel.TabIndex = 2;
            MenuPanel.Visible = false;
            // 
            // MenuStrip
            // 
            MenuStrip.BackColor = Color.FromArgb(33, 33, 33);
            MenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, settingsToolStripMenuItem, profileToolStripMenuItem, IncognitoToolStripMenuItem1 });
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Size = new Size(312, 24);
            MenuStrip.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { incognitoToolStripMenuItem });
            fileToolStripMenuItem.ForeColor = SystemColors.HighlightText;
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // incognitoToolStripMenuItem
            // 
            incognitoToolStripMenuItem.Name = "incognitoToolStripMenuItem";
            incognitoToolStripMenuItem.Size = new Size(125, 22);
            incognitoToolStripMenuItem.Text = "&Incognito";
            incognitoToolStripMenuItem.Click += IncognitoToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { extensionsToolStripMenuItem1 });
            settingsToolStripMenuItem.ForeColor = SystemColors.HighlightText;
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "&Settings";
            // 
            // extensionsToolStripMenuItem1
            // 
            extensionsToolStripMenuItem1.Name = "extensionsToolStripMenuItem1";
            extensionsToolStripMenuItem1.Size = new Size(130, 22);
            extensionsToolStripMenuItem1.Text = "&Extensions";
            // 
            // profileToolStripMenuItem
            // 
            profileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { bookmarksToolStripMenuItem });
            profileToolStripMenuItem.ForeColor = SystemColors.HighlightText;
            profileToolStripMenuItem.Name = "profileToolStripMenuItem";
            profileToolStripMenuItem.Size = new Size(53, 20);
            profileToolStripMenuItem.Text = "&Profile";
            // 
            // bookmarksToolStripMenuItem
            // 
            bookmarksToolStripMenuItem.Name = "bookmarksToolStripMenuItem";
            bookmarksToolStripMenuItem.Size = new Size(133, 22);
            bookmarksToolStripMenuItem.Text = "Bookmarks";
            // 
            // IncognitoToolStripMenuItem1
            // 
            IncognitoToolStripMenuItem1.ForeColor = SystemColors.HighlightText;
            IncognitoToolStripMenuItem1.Name = "IncognitoToolStripMenuItem1";
            IncognitoToolStripMenuItem1.Size = new Size(70, 20);
            IncognitoToolStripMenuItem1.Text = "&Incognito";
            IncognitoToolStripMenuItem1.Click += IncognitoToolStripMenuItem_Click;
            // 
            // Form_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(33, 33, 33);
            ClientSize = new Size(312, 564);
            Controls.Add(WebView21);
            Controls.Add(TopBarPanel);
            Controls.Add(MenuPanel);
            ForeColor = SystemColors.HighlightText;
            MainMenuStrip = MenuStrip;
            Name = "Form_Main";
            StartPosition = FormStartPosition.CenterScreen;
            FormClosing += Form_Main_FormClosing;
            Shown += Form_Main_Shown;
            Resize += Form_Main_Resize;
            ((System.ComponentModel.ISupportInitialize)WebView21).EndInit();
            TopBarPanel.ResumeLayout(false);
            TopBarPanel.PerformLayout();
            MenuPanel.ResumeLayout(false);
            MenuPanel.PerformLayout();
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 WebView21;
        private Panel TopBarPanel;
        private TextBox URLTextBox;
        private Button ReloadButton;
        private Button BackButton;
        private Panel MenuPanel;
        private MenuStrip MenuStrip;
        private Button MenuButton;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem profileToolStripMenuItem;
        private ToolStripMenuItem incognitoToolStripMenuItem;
        private ToolStripMenuItem extensionsToolStripMenuItem1;
        private ToolStripMenuItem IncognitoToolStripMenuItem1;
        private ToolStripMenuItem bookmarksToolStripMenuItem;
    }
}
