using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SyncthingTray.Properties;

namespace SyncthingTray
{
    public partial class SyncthingTray : Form
    {
        #region Member
        private Process _activeProcess;
        private SyncthingConfig _syncthingConfig;
        #endregion

        public SyncthingTray()
        {
            InitializeComponent();
        }

        #region Events
        void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            ReloadConfig();
        }

        private void btnSetPath_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            var fileName = openFileDialog.FileName.Trim();
            CheckPath(fileName);
        }
        private void timerCheckSync_Tick(object sender, EventArgs e)
        {
            var isRunning = IsSyncthingRunning();
            if (isRunning)
            {
                lblState.Text = "Running";
                lblState.ForeColor = Color.Green;
                notifyIcon.Icon = Resources.logo_64;
            }
            else
            {
                lblState.Text = "Not running";
                lblState.ForeColor = Color.OrangeRed;
                notifyIcon.Icon = Resources.logo_64_grayscale;
            }
            btnStart.Enabled = !isRunning;
            btnStop.Enabled = isRunning;
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            CheckPath(txtPath.Text.Trim());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (_activeProcess != null)
                {
                    _activeProcess.OutputDataReceived -= ActiveProcess_OutputDataReceived;
                    _activeProcess.ErrorDataReceived -= ActiveProcess_OutputDataReceived;
                    _activeProcess.Kill();
                    _activeProcess = null;
                }
                else
                {
                    ProcessHelper.StopProcess("syncthing");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            textBoxLog.Clear();

            try
            {
                _activeProcess = ProcessHelper.StartProcess(Settings.Default.SyncthingPath);
                _activeProcess.OutputDataReceived += ActiveProcess_OutputDataReceived;
                _activeProcess.ErrorDataReceived += ActiveProcess_OutputDataReceived;
                _activeProcess.BeginOutputReadLine();
                _activeProcess.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ActiveProcess_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() => textBoxLog.AppendText(e.Data + System.Environment.NewLine)));
            if (WindowState == FormWindowState.Minimized && !Settings.Default.HideTrayNotification)
            {
                this.Invoke(
                    new MethodInvoker(() => notifyIcon.ShowBalloonTip(1000, "SyncthingTray", e.Data, ToolTipIcon.Info)));
            }
        }

        private void SyncthingTray_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
            }
            else
                this.ShowInTaskbar = true;
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                WindowState = WindowState == FormWindowState.Normal ? FormWindowState.Minimized : FormWindowState.Normal;
        }

        private void showSyncthingTraySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void chkMinimizeOnStart_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.MinimizeOnStart = chkMinimizeOnStart.Checked;
            Settings.Default.Save();
        }

        private void chkStartOnBoot_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RegistryHelper.SetStartup(chkStartOnBoot.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SyncthingTray_Shown(object sender, EventArgs e)
        {
            try
            {
                chkStartOnBoot.Checked = RegistryHelper.GetStartup();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            var syncthingPath = Settings.Default.SyncthingPath;
            CheckPath(syncthingPath);

            if (chkStartOnBoot.Checked && btnStart.Enabled && !IsSyncthingRunning())
            {
                btnStart.PerformClick();
            }
        }

        private void SyncthingTray_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                return;
            }

            if (IsSyncthingRunning())
            {
                btnStop.PerformClick();
            }
        }

        private void SyncthingTray_Load(object sender, EventArgs e)
        {
            chkMinimizeOnStart.Checked = Settings.Default.MinimizeOnStart;
            chkHideTrayNotifications.Checked = Settings.Default.HideTrayNotification;
            if (Settings.Default.MinimizeOnStart)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Close();
        }

        private void openWebinterfaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_syncthingConfig.GuiEnabled) Process.Start("http://" + _syncthingConfig.GuiAddress);
        }

        private void chkWebGui_CheckedChanged(object sender, EventArgs e)
        {
            txtWebGui.Enabled = chkWebGui.Checked;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Process.Start(Settings.Default.SyncthingReleaseUrl);
        }
        #endregion

        private void ReloadConfig()
        {
            chkWebGui.Checked = _syncthingConfig.GuiEnabled;
            chkUpnp.Checked = _syncthingConfig.UpnpEnabled;
            chkStartBrowser.Checked = _syncthingConfig.StartBrowser;
            txtWebGui.Text = _syncthingConfig.GuiAddress;
        }

        private void CheckPath(string syncthingPath)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = false;
            timerCheckSync.Enabled = false;
            groupBoxSyncthing.Enabled = false;

            if (string.IsNullOrEmpty(syncthingPath) || !File.Exists(syncthingPath)) return;
            txtPath.Text = syncthingPath;
            Settings.Default.SyncthingPath = syncthingPath;
            Settings.Default.Save();

            var isRunning = IsSyncthingRunning();
            btnStart.Enabled = !isRunning;
            btnStop.Enabled = isRunning;
            timerCheckSync.Enabled = true;

            _syncthingConfig = SyncthingConfig.Load();
            if (_syncthingConfig != null)
            {
                _syncthingConfig.Watcher.Changed += Watcher_Changed;
                ReloadConfig();
                groupBoxSyncthing.Enabled = true;
            }
        }

        public static bool IsSyncthingRunning()
        {
            return ProcessHelper.IsProcessOpen("syncthing");
        }

        private void chkHideTrayNotifications_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.HideTrayNotification = chkHideTrayNotifications.Checked;
            Settings.Default.Save();
        }

    }
}
