using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using SyncthingTray.Properties;

namespace SyncthingTray
{
    static class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new SyncthingTray());
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                MessageBox.Show(string.Format("An fatal error occurred: '{0}'\n\nIf this error persists, please report it at '{1}'.", ex.Message, Settings.Default.GitHubIssueUrl), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
