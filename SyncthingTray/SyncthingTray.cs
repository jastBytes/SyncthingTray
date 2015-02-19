using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using log4net;
using SyncthingTray.Properties;

namespace SyncthingTray
{
    public partial class SyncthingTray : Form
    {
        #region Member
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Process _activeProcess;
        private SyncthingConfig _syncthingConfig;
        private readonly GitHubHelper _gitHubHelper;
        #endregion

        public SyncthingTray()
        {
            InitializeComponent();

            _gitHubHelper = new GitHubHelper();

            SyncthingConfig.ConfigurationChanged += SyncthingConfigOnConfigurationChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            chkMinimizeOnStart.Checked = Settings.Default.MinimizeOnStart;
            chkShowTrayNotifications.Checked = Settings.Default.ShowTrayNotifications;
            if (Settings.Default.MinimizeOnStart)
            {
                Hide();
                Visible = false; // Hide form window
                ShowInTaskbar = false; // Remove from taskbar
                WindowState = FormWindowState.Minimized;
            }
            base.OnLoad(e);
        }

        #region Events
        private void SyncthingConfigOnConfigurationChanged(object sender, SynchtingConfigEventArgs args)
        {
            _syncthingConfig = args.Configuration;
            Invoke(new MethodInvoker(ReloadConfig));
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
            CheckSyncthingStateAndUpdateUI();
        }

        private void CheckSyncthingStateAndUpdateUI()
        {
            var isRunning = IsSyncthingRunning();
            if (isRunning)
            {
                lblState.Text = strings.Running;
                lblState.ForeColor = Color.Green;
                notifyIcon.Icon = Resources.logo_64;
            }
            else
            {
                lblState.Text = strings.NotRunning;
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
                if (!string.IsNullOrEmpty(Settings.Default.SyncthingPath))
                {
                    ProcessHelper.StopProcessByName(Path.GetFileNameWithoutExtension(Settings.Default.SyncthingPath));
                    _activeProcess = null;
                }
                CheckSyncthingStateAndUpdateUI();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
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
                _activeProcess.Exited += _activeProcess_Exited;
                _activeProcess.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void _activeProcess_Exited(object sender, EventArgs e)
        {
            if (_activeProcess != null)
            {
                _activeProcess.OutputDataReceived -= ActiveProcess_OutputDataReceived;
                _activeProcess.ErrorDataReceived -= ActiveProcess_OutputDataReceived;
                _activeProcess.Exited -= _activeProcess_Exited;
                _activeProcess = null;
            }
            Invoke(new MethodInvoker(CheckSyncthingStateAndUpdateUI));
        }

        void ActiveProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Invoke(new MethodInvoker(() => textBoxLog.AppendText(e.Data + Environment.NewLine)));
            if (WindowState == FormWindowState.Minimized && !Settings.Default.ShowTrayNotifications)
            {
                Invoke(new MethodInvoker(() => notifyIcon.ShowBalloonTip(1000, Settings.Default.ApplicationName, e.Data, ToolTipIcon.Info)));
            }
        }

        private void SyncthingTray_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                switchToTray(true);
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (WindowState == FormWindowState.Minimized)
                    switchToTray(false);
                else
                    switchToTray(true);
            }
        }

        private void switchToTray(bool bHide)
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

        private void showSyncthingTraySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                switchToTray(false);
            else
                switchToTray(true);
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
            else if (!btnStart.Enabled)
            {
                var expectedPath =
                    Path.Combine(
                        Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "syncthing"),
                        "syncthing.exe");
                try
                {
                    if (!File.Exists(expectedPath))
                    {
                        _gitHubHelper.GetLatestVersion();
                    }
                    else
                    {
                        Settings.Default.SyncthingPath = expectedPath;
                        Settings.Default.Save();
                    }
                    txtPath.Text = Settings.Default.SyncthingPath;
                    CheckPath(txtPath.Text.Trim());
                }
                catch (Exception ex)
                {
                    textBoxLog.AppendText(ex.ToString());
                    Log.Error(ex.ToString());
                }
            }
        }

        private void SyncthingTray_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                this.Hide();
                return;
            }

            if (IsSyncthingRunning())
            {
                btnStop.PerformClick();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Close();
        }

        private void openWebinterfaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_syncthingConfig.gui.enabled && IsSyncthingRunning() && !string.IsNullOrEmpty(_syncthingConfig.gui.address)) Process.Start(string.Format("{0}{1}", _syncthingConfig.gui.tls ? "https://" : "http://", _syncthingConfig.gui.address));
        }

        private void chkWebGui_CheckedChanged(object sender, EventArgs e)
        {
            txtWebGui.Enabled = chkWebGui.Checked;
        }
        #endregion

        private void ReloadConfig()
        {
            groupBoxSyncthing.Enabled = false;
            if (_syncthingConfig == null) return;
            chkWebGui.Checked = _syncthingConfig.gui.enabled;
            chkUpnp.Checked = _syncthingConfig.options.upnpEnabled;
            chkStartBrowser.Checked = _syncthingConfig.options.startBrowser;
            txtWebGui.Text = string.Format("{0}{1}", _syncthingConfig.gui.tls ? "https://" : "http://", _syncthingConfig.gui.address);
            groupBoxSyncthing.Enabled = true;
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
            ReloadConfig();
        }

        public bool IsSyncthingRunning()
        {
            return _activeProcess != null || (!string.IsNullOrEmpty(Settings.Default.SyncthingPath) && ProcessHelper.IsProcessOpen(Settings.Default.SyncthingPath));
        }

        private void chkHideTrayNotifications_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ShowTrayNotifications = chkShowTrayNotifications.Checked;
            Settings.Default.Save();
        }

        private void checkBoxUseSpecificVersion_CheckedChanged(object sender, EventArgs e)
        {
            btnSetPath.Enabled = checkBoxUseSpecificVersion.Checked;
        }
    }
}
