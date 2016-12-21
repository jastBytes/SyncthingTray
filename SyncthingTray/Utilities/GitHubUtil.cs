using Octokit;
using SyncthingTray.Dialogs;
using SyncthingTray.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SyncthingTray.Utilities
{
    public class GitHubUtil
    {
        private readonly GitHubClient _githubClient;

        public delegate void LatestVersionRetrievedEventHandler(object sender, LatestVersionRetrievedEventArgs args);
        public event LatestVersionRetrievedEventHandler LatestVersionRetrievedEvent;

        public GitHubUtil()
        {
            _githubClient = new GitHubClient(new ProductHeaderValue("SyncthingTray"));
        }

        public async void GetLatestVersionAsync()
        {
            var repos = await _githubClient.Release.GetAll("syncthing", "syncthing");
            var latest = await _githubClient.Release.GetAssets("syncthing", "syncthing", repos[0].Id);

            LatestVersionRetrievedEvent?.Invoke(this, GrabVersions(latest, repos[0]));
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
                var repoIndex = 0;
                ReleaseAsset asset = null;

                while (asset == null && repoIndex < repos.Count)
                {
                    var latest = _githubClient.Release.GetAssets("syncthing", "syncthing", repos[0].Id).Result;
                    var versions = GrabVersions(latest, repos[0]);
                    asset = Environment.Is64BitOperatingSystem ? versions.LatestAmd64 : versions.LatestIntel386;
                }

                if (asset == null) return;
                paDialog.BackgroundWorker.ReportProgress(0, $"Downloading {asset.Name}...");
                var response = _githubClient.Connection.Get<byte[]>(new Uri(asset.Url), new Dictionary<string, string>(), "application/octet-stream").Result;

                paDialog.BackgroundWorker.ReportProgress(50, $"Unzipping {asset.Name}...");
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
            paDialog.BackgroundWorker.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
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
            var windowsLatest = latest.Where(asset => asset.Name.Contains("windows")).ToList();
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
