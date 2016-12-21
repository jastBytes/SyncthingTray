using log4net;
using SyncthingTray.External;
using SyncthingTray.Properties;
using SyncthingTray.Utilities;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SyncthingTray.UI
{
    public partial class SyncthingTray : BaseForm
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SyncthingTray));

        private Process _activeProcess;
        private Configuration _syncthingConfig;

        public SyncthingTray()
        {
            InitializeComponent();

            Configuration.ConfigurationChanged += SyncthingConfigOnConfigurationChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            chkMinimizeOnStart.Checked = Settings.Default.MinimizeOnStart;
            chkShowTrayNotifications.Checked = Settings.Default.ShowTrayNotifications;
            base.OnLoad(e);
        }

        private void SyncthingConfigOnConfigurationChanged(object sender, SyncthingConfigEventArgs args)
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
            CheckSyncthingStateAndUpdateUi();
        }

        private void CheckSyncthingStateAndUpdateUi()
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
                    ProcessUtil.StopProcessByName(Path.GetFileNameWithoutExtension(Settings.Default.SyncthingPath));
                    _activeProcess = null;
                }
                CheckSyncthingStateAndUpdateUi();
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
                _activeProcess = ProcessUtil.StartProcess(Settings.Default.SyncthingPath);
                _activeProcess.OutputDataReceived += ActiveProcess_OutputDataReceived;
                _activeProcess.ErrorDataReceived += ActiveProcess_OutputDataReceived;
                _activeProcess.BeginOutputReadLine();
                _activeProcess.BeginErrorReadLine();
                _activeProcess.Exited += ActiveProcess_Exited;
                _activeProcess.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ActiveProcess_Exited(object sender, EventArgs e)
        {
            if (_activeProcess != null)
            {
                _activeProcess.OutputDataReceived -= ActiveProcess_OutputDataReceived;
                _activeProcess.ErrorDataReceived -= ActiveProcess_OutputDataReceived;
                _activeProcess.Exited -= ActiveProcess_Exited;
                _activeProcess = null;
            }
            Invoke(new MethodInvoker(CheckSyncthingStateAndUpdateUi));
        }

        private void ActiveProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Invoke(new MethodInvoker(() => textBoxLog.AppendText(e.Data + Environment.NewLine)));
            if (WindowState == FormWindowState.Minimized && !Settings.Default.ShowTrayNotifications)
            {
                Invoke(new MethodInvoker(() => notifyIcon.ShowBalloonTip(1000, Settings.Default.ApplicationName, e.Data, ToolTipIcon.Info)));
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            SwitchToTray(WindowState != FormWindowState.Minimized);
        }

        private void ShowSyncthingTraySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchToTray(WindowState != FormWindowState.Minimized);
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
                RegistryUtil.SetStartup(chkStartOnBoot.Checked);
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
                chkStartOnBoot.Checked = RegistryUtil.GetStartup();
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
                        GitHubUtil.GetLatestVersion();
                        Settings.Default.SyncthingPath = expectedPath;
                        Settings.Default.Save();
                        txtPath.Text = Settings.Default.SyncthingPath;
                        CheckPath(txtPath.Text.Trim());
                    }
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
            if (IsSyncthingRunning())
            {
                btnStop.PerformClick();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            Close();
        }

        private void openWebinterfaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_syncthingConfig.gui.enabled && IsSyncthingRunning() && !string.IsNullOrEmpty(_syncthingConfig.gui.address))
            {
                Process.Start($"{(_syncthingConfig.gui.tls ? "https://" : "http://")}{_syncthingConfig.gui.address}");
            }
        }

        private void chkWebGui_CheckedChanged(object sender, EventArgs e)
        {
            txtWebGui.Enabled = chkWebGui.Checked;
        }

        private void ReloadConfig()
        {
            groupBoxSyncthing.Enabled = false;
            if (_syncthingConfig == null) return;
            chkWebGui.Checked = _syncthingConfig.gui.enabled;
            chkUpnp.Checked = _syncthingConfig.options.upnpEnabled;
            chkStartBrowser.Checked = _syncthingConfig.options.startBrowser;
            txtWebGui.Text = $"{(_syncthingConfig.gui.tls ? "https://" : "http://")}{_syncthingConfig.gui.address}";
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

            _syncthingConfig = Configuration.Load();
            ReloadConfig();
        }

        public bool IsSyncthingRunning()
        {
            return _activeProcess != null || (!string.IsNullOrEmpty(Settings.Default.SyncthingPath) && ProcessUtil.IsProcessOpen(Settings.Default.SyncthingPath));
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
