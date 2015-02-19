using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Octokit;
using SyncthingTray.Dialogs;
using SyncthingTray.Properties;

namespace SyncthingTray
{
    public class GitHubHelper
    {
        private readonly GitHubClient _githubClient;

        public delegate void LatestVersionRetrievedEventHandler(object sender, LatestVersionRetrievedEventArgs args);
        public event LatestVersionRetrievedEventHandler LatestVersionRetrievedEvent;

        public GitHubHelper()
        {
            _githubClient = new GitHubClient(new ProductHeaderValue("SyncthingTray"));
        }

        async public void GetLatestVersionAsync()
        {
            var repos = await _githubClient.Release.GetAll("syncthing", "syncthing");
            var latest = await _githubClient.Release.GetAssets("syncthing", "syncthing", repos[0].Id);

            LatestVersionRetrievedEvent(this, GrabVersions(latest, repos[0]));
        }

        public void GetLatestVersion()
        {
            var paDialog = new PendingActivityDialog
            {
                Text = "Autoupdate syncthing",
                SupportsCancellation = false,
                SupportsProgressVisualization = false
            };
            paDialog.BackgroundWorker.DoWork += delegate
            {
                paDialog.BackgroundWorker.ReportProgress(0, "Checking for latest version...");

                var repos = _githubClient.Release.GetAll("syncthing", "syncthing").Result;
                var latest = _githubClient.Release.GetAssets("syncthing", "syncthing", repos[0].Id).Result;
                var parse = GrabVersions(latest, repos[0]);
                var asset = Environment.Is64BitOperatingSystem ? parse.LatestAmd64 : parse.LatestIntel386;

                paDialog.BackgroundWorker.ReportProgress(0, string.Format("Downloading {0}...", asset.Name));
                var response = _githubClient.Connection.Get<byte[]>(new Uri(asset.Url), new Dictionary<string, string>(), "application/octet-stream").Result;

                paDialog.BackgroundWorker.ReportProgress(50, string.Format("Unzipping {0}...", asset.Name));
                var filename = Path.GetTempFileName();
                using (var fileStream = File.Create(filename))
                {
                    fileStream.Write(response.BodyAsObject, 0, response.BodyAsObject.Length);
                }
                System.IO.Compression.ZipFile.ExtractToDirectory(filename, Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath));
                Directory.Move(Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), Path.GetFileNameWithoutExtension(asset.Name)), Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "syncthing"));
                if (string.IsNullOrEmpty(Settings.Default.SyncthingPath))
                {
                    Settings.Default.SyncthingPath = Path.Combine(Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "syncthing"), "syncthing.exe");
                    Settings.Default.Save();
                }

                File.Delete(filename);
                paDialog.BackgroundWorker.ReportProgress(100, "Finished!");
                Thread.Sleep(500);
            };
            paDialog.BackgroundWorker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs args)
            {
                if (args.Error != null)
                {
                    MessageBox.Show(args.Error.Message);
                }
            };
            paDialog.ShowDialog();
        }

        private static LatestVersionRetrievedEventArgs GrabVersions(IEnumerable<ReleaseAsset> latest, Release release)
        {
            var windowsLatest = latest.Where(asset => asset.Name.Contains("windows"));
            var windowsIntel386 = windowsLatest.FirstOrDefault(asset => asset.Name.Contains("386"));
            var windowsAmd64 = windowsLatest.FirstOrDefault(asset => asset.Name.Contains("amd64"));

            return new LatestVersionRetrievedEventArgs()
            {
                LatestIntel386 = windowsIntel386,
                LatestAmd64 = windowsAmd64,
                LatestVersion = release
            };
        }

    }

    public class LatestVersionRetrievedEventArgs
    {
        public ReleaseAsset LatestIntel386 { get; set; }

        public ReleaseAsset LatestAmd64 { get; set; }

        public Release LatestVersion { get; set; }
    }
}
