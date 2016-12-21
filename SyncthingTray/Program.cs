using log4net;
using SyncthingTray.Properties;
using System;
using System.Windows.Forms;

namespace SyncthingTray
{
    public static class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new UI.SyncthingTray());
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                MessageBox.Show(string.Format(strings.MainFatalError, ex.Message, Settings.Default.GitHubIssueUrl), strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
