using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SyncthingTray.Dialogs
{
    public partial class PendingActivityDialog : Form
    {
        public bool SupportsCancellation
        {
            get { return btnCancel.Enabled; }
            set
            {
                btnCancel.Enabled = value;
            }
        }

        public bool SupportsProgressVisualization
        {
            get { return progressBar.Style == ProgressBarStyle.Continuous; }
            set
            {
                progressBar.Style = value ? ProgressBarStyle.Continuous : ProgressBarStyle.Marquee;
            }
        }

        public int ProgressMaxValue
        {
            get { return progressBar.Maximum; }
            set { progressBar.Maximum = value; }
        }

        public int ProgressValue
        {
            get { return progressBar.Value; }
            set { progressBar.Value = value; }
        }

        public string CurrentAcivityDescription
        {
            get { return lblCurrentTask.Text; }
            set
            {
                if (_cancellationPending) return;
                lblCurrentTask.Text = value;
            }
        }

        public BackgroundWorker BackgroundWorker => backgroundWorker;

        private bool _cancellationPending;

        public PendingActivityDialog()
        {
            InitializeComponent();
            SupportsCancellation = true;
            SupportsProgressVisualization = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_cancellationPending) return;
            btnCancel.Enabled = false;
            _cancellationPending = true;
            lblCurrentTask.Text = "Cancellation pending...";
            backgroundWorker.CancelAsync();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblCurrentTask.Text = e.UserState as string;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnCancel.Enabled = false;
            Close();
        }

        private void PendingActivityDialog_Shown(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }
    }
}
