using SyncthingTray.Properties;
using System;
using System.IO;
using System.Xml.Serialization;

namespace SyncthingTray.External
{
    public partial class Configuration
    {
        public delegate void SyncthingConfigEventHandler(object sender, SyncthingConfigEventArgs args);
        public static event SyncthingConfigEventHandler ConfigurationChanged;

        static Configuration()
        {
            var directoryPath = Path.GetDirectoryName(ConfigPath);
            Directory.CreateDirectory(directoryPath);
            Watcher = new FileSystemWatcher(directoryPath)
            {
                Filter = Path.GetFileName(ConfigPath)
            };
            Watcher.Changed += ConfigChanged;
            Watcher.EnableRaisingEvents = true;
        }

        [XmlIgnore]
        private static FileSystemWatcher Watcher { get; }

        [XmlIgnore]
        public static string ConfigPath
        {
            get
            {
                var syncthingXmlPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Syncthing", Settings.Default.SyncthingConfigFileName);
                return syncthingXmlPath;
            }
        }

        public static Configuration Load()
        {
            return File.Exists(ConfigPath) ? ReadConfig() : null;
        }

        private static void ConfigChanged(object sender, FileSystemEventArgs e)
        {
            ConfigurationChanged?.Invoke(sender, new SyncthingConfigEventArgs(ReadConfig()));
        }

        private static Configuration ReadConfig()
        {
            return LoadFromFile(ConfigPath);
        }
    }

    public class SyncthingConfigEventArgs
    {
        public Configuration Configuration { get; private set; }

        public SyncthingConfigEventArgs(Configuration configuration)
        {
            Configuration = configuration;
        }
    }
}