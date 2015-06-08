using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using SyncthingTray.Properties;

namespace SyncthingTray.External
{
    public partial class configuration
    {
        public delegate void SyncthingConfigEventHandler(object sender, SyncthingConfigEventArgs args);
        public static event SyncthingConfigEventHandler ConfigurationChanged;

        static configuration()
        {
            var DirectoryPath = Path.GetDirectoryName(ConfigPath);
            Directory.CreateDirectory(DirectoryPath);
            Watcher = new FileSystemWatcher(DirectoryPath)
            {
                Filter = Path.GetFileName(ConfigPath)
            };
            Watcher.Changed += ConfigChanged;
            Watcher.EnableRaisingEvents = true;
        }

        [XmlIgnore]
        private static FileSystemWatcher Watcher { get; set; }

        [XmlIgnore]
        public static string ConfigPath
        {
            get
            {
                var syncthingXmlPath =
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Syncthing",
                        Settings.Default.SyncthingConfigFileName);
                return syncthingXmlPath;
            }
        }

        public static configuration Load()
        {
            return File.Exists(ConfigPath) ? ReadConfig() : null;
        }

        private static void ConfigChanged(object sender, FileSystemEventArgs e)
        {
            if (ConfigurationChanged != null)
                ConfigurationChanged(sender, new SyncthingConfigEventArgs(ReadConfig()));
        }

        private static configuration ReadConfig()
        {
            return LoadFromFile(ConfigPath);
        }

        private static FileStream OpenRead(string filename, int numTrys = 5, int timeout = 100)
        {
            IOException ex = null;
            while (numTrys-- > 0)
            {
                try
                {
                    return File.OpenRead(filename);
                }
                catch (IOException e)
                {
                    ex = e;
                    Thread.Sleep(timeout);
                }
            }
            if (ex != null) throw ex;
            throw new IOException("Couldn't lock file.");
        }
    }

    public class SyncthingConfigEventArgs
    {
        public configuration Configuration { get; private set; }

        public SyncthingConfigEventArgs(configuration configuration)
        {
            Configuration = configuration;
        }
    }
}