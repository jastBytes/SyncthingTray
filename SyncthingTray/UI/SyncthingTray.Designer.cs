namespace SyncthingTray.UI
{
    partial class SyncthingTray
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncthingTray));
            this.btnSetPath = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openWebinterfaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSyncthingTraySettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkStartOnBoot = new System.Windows.Forms.CheckBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.timerCheckSync = new System.Windows.Forms.Timer(this.components);
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.chkMinimizeOnStart = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxUseSpecificVersion = new System.Windows.Forms.CheckBox();
            this.chkShowTrayNotifications = new System.Windows.Forms.CheckBox();
            this.groupBoxSyncthing = new System.Windows.Forms.GroupBox();
            this.chkUpnp = new System.Windows.Forms.CheckBox();
            this.chkStartBrowser = new System.Windows.Forms.CheckBox();
            this.chkWebGui = new System.Windows.Forms.CheckBox();
            this.txtWebGui = new System.Windows.Forms.TextBox();
            this.chkHttps = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxSyncthing.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSetPath
            // 
            this.btnSetPath.Enabled = false;
            this.btnSetPath.Location = new System.Drawing.Point(134, 80);
            this.btnSetPath.Name = "btnSetPath";
            this.btnSetPath.Size = new System.Drawing.Size(50, 23);
            this.btnSetPath.TabIndex = 0;
            this.btnSetPath.Text = "&Path";
            this.btnSetPath.UseVisualStyleBackColor = true;
            this.btnSetPath.Click += new System.EventHandler(this.btnSetPath_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(190, 82);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(212, 20);
            this.txtPath.TabIndex = 1;
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipTitle = "SyncthingTray";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = global::SyncthingTray.Properties.Settings.Default.ApplicationName;
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openWebinterfaceToolStripMenuItem,
            this.showSyncthingTraySettingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(183, 70);
            // 
            // openWebinterfaceToolStripMenuItem
            // 
            this.openWebinterfaceToolStripMenuItem.Name = "openWebinterfaceToolStripMenuItem";
            this.openWebinterfaceToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.openWebinterfaceToolStripMenuItem.Text = "Open Web Interface";
            this.openWebinterfaceToolStripMenuItem.Click += new System.EventHandler(this.openWebinterfaceToolStripMenuItem_Click);
            // 
            // showSyncthingTraySettingsToolStripMenuItem
            // 
            this.showSyncthingTraySettingsToolStripMenuItem.Name = "showSyncthingTraySettingsToolStripMenuItem";
            this.showSyncthingTraySettingsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.showSyncthingTraySettingsToolStripMenuItem.Text = "Show SyncthingTray";
            this.showSyncthingTraySettingsToolStripMenuItem.Click += new System.EventHandler(this.ShowSyncthingTraySettingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // chkStartOnBoot
            // 
            this.chkStartOnBoot.AutoSize = true;
            this.chkStartOnBoot.Location = new System.Drawing.Point(189, 22);
            this.chkStartOnBoot.Name = "chkStartOnBoot";
            this.chkStartOnBoot.Size = new System.Drawing.Size(87, 17);
            this.chkStartOnBoot.TabIndex = 3;
            this.chkStartOnBoot.Text = "Start on &boot";
            this.chkStartOnBoot.UseVisualStyleBackColor = true;
            this.chkStartOnBoot.CheckedChanged += new System.EventHandler(this.chkStartOnBoot_CheckedChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "syncthing.exe";
            this.openFileDialog.Filter = "Syncthing Executable|*.exe";
            this.openFileDialog.Title = "Set path to syncthing";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Syncthing state:";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.ForeColor = System.Drawing.Color.Red;
            this.lblState.Location = new System.Drawing.Point(104, 129);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(62, 13);
            this.lblState.TabIndex = 5;
            this.lblState.Text = "Not running";
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(6, 18);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(55, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(67, 18);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(55, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // timerCheckSync
            // 
            this.timerCheckSync.Enabled = true;
            this.timerCheckSync.Interval = 1000;
            this.timerCheckSync.Tick += new System.EventHandler(this.timerCheckSync_Tick);
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.Location = new System.Drawing.Point(12, 145);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(784, 271);
            this.textBoxLog.TabIndex = 8;
            // 
            // chkMinimizeOnStart
            // 
            this.chkMinimizeOnStart.AutoSize = true;
            this.chkMinimizeOnStart.Location = new System.Drawing.Point(282, 22);
            this.chkMinimizeOnStart.Name = "chkMinimizeOnStart";
            this.chkMinimizeOnStart.Size = new System.Drawing.Size(104, 17);
            this.chkMinimizeOnStart.TabIndex = 9;
            this.chkMinimizeOnStart.Text = "&Minimize on start";
            this.chkMinimizeOnStart.UseVisualStyleBackColor = true;
            this.chkMinimizeOnStart.CheckedChanged += new System.EventHandler(this.chkMinimizeOnStart_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxUseSpecificVersion);
            this.groupBox1.Controls.Add(this.chkShowTrayNotifications);
            this.groupBox1.Controls.Add(this.btnSetPath);
            this.groupBox1.Controls.Add(this.txtPath);
            this.groupBox1.Controls.Add(this.chkMinimizeOnStart);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.chkStartOnBoot);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 114);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SyncthingTray Configuration";
            // 
            // checkBoxUseSpecificVersion
            // 
            this.checkBoxUseSpecificVersion.AutoSize = true;
            this.checkBoxUseSpecificVersion.Location = new System.Drawing.Point(7, 84);
            this.checkBoxUseSpecificVersion.Name = "checkBoxUseSpecificVersion";
            this.checkBoxUseSpecificVersion.Size = new System.Drawing.Size(121, 17);
            this.checkBoxUseSpecificVersion.TabIndex = 11;
            this.checkBoxUseSpecificVersion.Text = "Use specific version";
            this.checkBoxUseSpecificVersion.UseVisualStyleBackColor = true;
            this.checkBoxUseSpecificVersion.CheckedChanged += new System.EventHandler(this.checkBoxUseSpecificVersion_CheckedChanged);
            // 
            // chkShowTrayNotifications
            // 
            this.chkShowTrayNotifications.AutoSize = true;
            this.chkShowTrayNotifications.Location = new System.Drawing.Point(189, 45);
            this.chkShowTrayNotifications.Name = "chkShowTrayNotifications";
            this.chkShowTrayNotifications.Size = new System.Drawing.Size(132, 17);
            this.chkShowTrayNotifications.TabIndex = 10;
            this.chkShowTrayNotifications.Text = "&Show tray notifications";
            this.chkShowTrayNotifications.UseVisualStyleBackColor = true;
            this.chkShowTrayNotifications.CheckedChanged += new System.EventHandler(this.chkHideTrayNotifications_CheckedChanged);
            // 
            // groupBoxSyncthing
            // 
            this.groupBoxSyncthing.Controls.Add(this.chkHttps);
            this.groupBoxSyncthing.Controls.Add(this.chkUpnp);
            this.groupBoxSyncthing.Controls.Add(this.chkStartBrowser);
            this.groupBoxSyncthing.Controls.Add(this.chkWebGui);
            this.groupBoxSyncthing.Controls.Add(this.txtWebGui);
            this.groupBoxSyncthing.Enabled = false;
            this.groupBoxSyncthing.Location = new System.Drawing.Point(428, 12);
            this.groupBoxSyncthing.Name = "groupBoxSyncthing";
            this.groupBoxSyncthing.Size = new System.Drawing.Size(368, 114);
            this.groupBoxSyncthing.TabIndex = 11;
            this.groupBoxSyncthing.TabStop = false;
            this.groupBoxSyncthing.Text = "Syncthing Configuration";
            // 
            // chkUpnp
            // 
            this.chkUpnp.AutoSize = true;
            this.chkUpnp.Location = new System.Drawing.Point(120, 43);
            this.chkUpnp.Name = "chkUpnp";
            this.chkUpnp.Size = new System.Drawing.Size(56, 17);
            this.chkUpnp.TabIndex = 2;
            this.chkUpnp.Text = "UPNP";
            this.chkUpnp.UseVisualStyleBackColor = true;
            // 
            // chkStartBrowser
            // 
            this.chkStartBrowser.AutoSize = true;
            this.chkStartBrowser.Location = new System.Drawing.Point(6, 43);
            this.chkStartBrowser.Name = "chkStartBrowser";
            this.chkStartBrowser.Size = new System.Drawing.Size(108, 17);
            this.chkStartBrowser.TabIndex = 2;
            this.chkStartBrowser.Text = "Autostart browser";
            this.chkStartBrowser.UseVisualStyleBackColor = true;
            // 
            // chkWebGui
            // 
            this.chkWebGui.AutoSize = true;
            this.chkWebGui.Location = new System.Drawing.Point(6, 20);
            this.chkWebGui.Name = "chkWebGui";
            this.chkWebGui.Size = new System.Drawing.Size(68, 17);
            this.chkWebGui.TabIndex = 0;
            this.chkWebGui.Text = "WebGUI";
            this.chkWebGui.UseVisualStyleBackColor = true;
            this.chkWebGui.CheckedChanged += new System.EventHandler(this.chkWebGui_CheckedChanged);
            // 
            // txtWebGui
            // 
            this.txtWebGui.Location = new System.Drawing.Point(143, 18);
            this.txtWebGui.Name = "txtWebGui";
            this.txtWebGui.Size = new System.Drawing.Size(219, 20);
            this.txtWebGui.TabIndex = 1;
            this.txtWebGui.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // chkHttps
            // 
            this.chkHttps.AutoSize = true;
            this.chkHttps.Location = new System.Drawing.Point(80, 20);
            this.chkHttps.Name = "chkHttps";
            this.chkHttps.Size = new System.Drawing.Size(57, 17);
            this.chkHttps.TabIndex = 3;
            this.chkHttps.Text = "Https?";
            this.chkHttps.UseVisualStyleBackColor = true;
            // 
            // SyncthingTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 428);
            this.Controls.Add(this.groupBoxSyncthing);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.textBoxLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SyncthingTray";
            this.Text = "SyncthingTray";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SyncthingTray_FormClosing);
            this.Shown += new System.EventHandler(this.SyncthingTray_Shown);
            this.contextMenuStrip.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxSyncthing.ResumeLayout(false);
            this.groupBoxSyncthing.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSetPath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openWebinterfaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSyncthingTraySettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkStartOnBoot;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer timerCheckSync;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.CheckBox chkMinimizeOnStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxSyncthing;
        private System.Windows.Forms.CheckBox chkWebGui;
        private System.Windows.Forms.TextBox txtWebGui;
        private System.Windows.Forms.CheckBox chkStartBrowser;
        private System.Windows.Forms.CheckBox chkUpnp;
        private System.Windows.Forms.CheckBox chkShowTrayNotifications;
        private System.Windows.Forms.CheckBox chkHttps;
        private System.Windows.Forms.CheckBox checkBoxUseSpecificVersion;
    }
}

