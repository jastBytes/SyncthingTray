namespace SyncthingTray
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
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSetPath
            // 
            this.btnSetPath.Location = new System.Drawing.Point(13, 13);
            this.btnSetPath.Name = "btnSetPath";
            this.btnSetPath.Size = new System.Drawing.Size(108, 23);
            this.btnSetPath.TabIndex = 0;
            this.btnSetPath.Text = "&Set Syncthing Path";
            this.btnSetPath.UseVisualStyleBackColor = true;
            this.btnSetPath.Click += new System.EventHandler(this.btnSetPath_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(127, 15);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(341, 20);
            this.txtPath.TabIndex = 1;
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipTitle = "SyncthingTray";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "SyncthingTray";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
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
            this.openWebinterfaceToolStripMenuItem.Text = "Open Webinterface";
            this.openWebinterfaceToolStripMenuItem.Click += new System.EventHandler(this.openWebinterfaceToolStripMenuItem_Click);
            // 
            // showSyncthingTraySettingsToolStripMenuItem
            // 
            this.showSyncthingTraySettingsToolStripMenuItem.Name = "showSyncthingTraySettingsToolStripMenuItem";
            this.showSyncthingTraySettingsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.showSyncthingTraySettingsToolStripMenuItem.Text = "Show SyncthingTray";
            this.showSyncthingTraySettingsToolStripMenuItem.Click += new System.EventHandler(this.showSyncthingTraySettingsToolStripMenuItem_Click);
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
            this.chkStartOnBoot.Location = new System.Drawing.Point(15, 71);
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
            this.label1.Location = new System.Drawing.Point(284, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Syncthing state:";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.ForeColor = System.Drawing.Color.Red;
            this.lblState.Location = new System.Drawing.Point(373, 47);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(95, 13);
            this.lblState.TabIndex = 5;
            this.lblState.Text = "NOT RUNNING";
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(13, 42);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(108, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start Syncthing";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(127, 42);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(87, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop Syncthing";
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
            this.textBoxLog.Location = new System.Drawing.Point(12, 103);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(784, 204);
            this.textBoxLog.TabIndex = 8;
            // 
            // chkMinimizeOnStart
            // 
            this.chkMinimizeOnStart.AutoSize = true;
            this.chkMinimizeOnStart.Location = new System.Drawing.Point(109, 71);
            this.chkMinimizeOnStart.Name = "chkMinimizeOnStart";
            this.chkMinimizeOnStart.Size = new System.Drawing.Size(104, 17);
            this.chkMinimizeOnStart.TabIndex = 9;
            this.chkMinimizeOnStart.Text = "&Minimize on start";
            this.chkMinimizeOnStart.UseVisualStyleBackColor = true;
            this.chkMinimizeOnStart.CheckedChanged += new System.EventHandler(this.chkMinimizeOnStart_CheckedChanged);
            // 
            // SyncthingTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 319);
            this.Controls.Add(this.chkMinimizeOnStart);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkStartOnBoot);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnSetPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SyncthingTray";
            this.Text = "SyncthingTray";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SyncthingTray_FormClosing);
            this.Load += new System.EventHandler(this.SyncthingTray_Load);
            this.Shown += new System.EventHandler(this.SyncthingTray_Shown);
            this.Resize += new System.EventHandler(this.SyncthingTray_Resize);
            this.contextMenuStrip.ResumeLayout(false);
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
    }
}

