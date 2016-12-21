using SyncthingTray.Properties;
using System;
using System.Windows.Forms;

namespace SyncthingTray.UI
{
    public partial class BaseForm : Form
    {
        protected BaseForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Settings.Default.MinimizeOnStart)
            {
                Hide();
                Visible = false; // Hide form window
                ShowInTaskbar = false; // Remove from taskbar
                WindowState = FormWindowState.Minimized;
            }
            base.OnLoad(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                SwitchToTray(true);
            }
            base.OnResize(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                Hide();
                return;
            }
            base.OnFormClosing(e);
        }

        protected void SwitchToTray(bool bHide)
        {
            if (!bHide)
            {
                Show();
                WindowState = FormWindowState.Normal;
                BringToFront();
            }
            else
            {
                Hide();
                WindowState = FormWindowState.Minimized;
            }
        }
    }
}
