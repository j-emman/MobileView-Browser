using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MobileView.Classes
{
    public class TitleBar
    {
        private Form _ParentForm;
        private Panel _Panel;
        private Button _CloseButton;
        private Button _MinimizeButton;
        private Button _MaximizeButton;
        private bool isMaximized = false;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")] public extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")] public extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void Panel_MouseDown(object? sender, MouseEventArgs e)
        {
            if (this._ParentForm != null)
            {
                ReleaseCapture();
                SendMessage(this._ParentForm.Handle, 0x112, 0xf012, 0);
            }
        }
        private void CloseButton_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }
        private void MinimizeButton_Click(object? sender, EventArgs e)
        {
            MinimizeWindow();
        }
        private void MaximizeButton_Click(object? sender, EventArgs e)
        {
            MaximizeWindow();
        }
        public void AttachPanelMouseDownEvent(Panel externalPanel)
        {
            if (externalPanel != null)
            {
                externalPanel.MouseDown += Panel_MouseDown;
            }
        }
        public void DetachPanelMouseDownEvent(Panel externalPanel)
        {
            if (externalPanel != null)
            {
                externalPanel.MouseDown -= Panel_MouseDown;
            }
        }
        public void MaximizeWindow()
        {
            if (this._ParentForm != null)
            {
                if (isMaximized)
                {
                    this._ParentForm.WindowState = FormWindowState.Normal;
                    isMaximized = false;
                }
                else
                {
                    this._ParentForm.WindowState = FormWindowState.Maximized;
                    isMaximized = true;

                }
            }
        }
        public void MinimizeWindow()
        {
            if (this._ParentForm != null)
            {
                this._ParentForm.WindowState = FormWindowState.Minimized;
            }
        }
        public TitleBar(Form parentForm, Panel panel) 
        {
            if (_Panel != null)
            {
                _Panel.MouseDown += Panel_MouseDown;
            }
        }
        public TitleBar(Form parentForm, Panel panel, Button closeButton, Button minimizeButton)
        {
            _Panel = panel;
            _ParentForm = parentForm;
            _CloseButton = closeButton;
            _MinimizeButton = minimizeButton;

            if (_Panel != null)
            {
                _Panel.MouseDown += Panel_MouseDown;
                _CloseButton.Click += CloseButton_Click;
                _MinimizeButton.Click += MinimizeButton_Click;
            }
        }
        public TitleBar( Form parentForm, Panel panel, Button closeButton, Button maximizeButton, Button minimizeButton) 
        { 
            _Panel = panel;
            _ParentForm = parentForm;  
            _CloseButton = closeButton;
            _MinimizeButton = minimizeButton;
            _MaximizeButton = maximizeButton;

            if (_Panel != null)
            {
                _Panel.MouseDown += Panel_MouseDown;
                _CloseButton.Click += CloseButton_Click;
                _MinimizeButton.Click += MinimizeButton_Click;
                _MaximizeButton.Click += MaximizeButton_Click;
            }
        }
    }
}
