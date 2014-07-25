using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SyncthingTray
{
    internal class SyncthingConfig
    {
        public FileSystemWatcher Watcher { get; private set; }

        public static string ConfigPath
        {
            get
            {
                var syncthingXmlPath =
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Syncthing",
                        "config.xml");
                return File.Exists(syncthingXmlPath) ? syncthingXmlPath : null;
            }
        }

        internal static SyncthingConfig Load()
        {
            if (string.IsNullOrEmpty(ConfigPath)) return null;

            var conf = new SyncthingConfig();
            using (var reader = XmlReader.Create(ConfigPath))
            {
                while (reader.Read())
                {
                    if (!reader.IsStartElement()) continue;
                    switch (reader.Name)
                    {
                        case "configuration":
                            conf.Version = reader.GetAttribute("version");
                            break;
                        case "gui":
                            conf.GuiEnabled = Convert.ToBoolean(reader.GetAttribute("enabled"));
                            if (reader.ReadToDescendant("address") && reader.Read())
                                conf.GuiAddress = reader.Value;
                            break;
                        case "options":
                            if (reader.ReadToDescendant("startBrowser") && reader.Read())
                                conf.StartBrowser = Convert.ToBoolean(reader.Value);
                            if (reader.ReadToDescendant("upnpEnabled") && reader.Read())
                                conf.UpnpEnabled = Convert.ToBoolean(reader.Value);
                            break;
                    }
                }
            }

            conf.Watcher = new FileSystemWatcher(Path.GetDirectoryName(ConfigPath))
            {
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite,
                Filter = "*.xml"
            };

            return conf;
        }

        static void changeWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        public string Version { get; set; }

        public bool GuiEnabled { get; set; }

        public string GuiAddress { get; set; }

        public bool StartBrowser { get; set; }

        public bool UpnpEnabled { get; set; }
    }
}
