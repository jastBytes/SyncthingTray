using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using SyncthingTray.Properties;

namespace SyncthingTray
{
    [Serializable, XmlRoot("configuration")]
    public partial class SyncthingConfig
    {
        public delegate void SyncthingConfigEventHandler(object sender, SynchtingConfigEventArgs args);
        public static event SyncthingConfigEventHandler ConfigurationChanged;

        static SyncthingConfig()
        {
            Watcher = new FileSystemWatcher(Path.GetDirectoryName(ConfigPath))
            {
                Filter = Path.GetFileName(ConfigPath)
            };
            Watcher.Changed += ConfigChanged;
            Watcher.EnableRaisingEvents = true;
        }

        [System.Xml.Serialization.XmlIgnore]
        private static FileSystemWatcher Watcher { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public static string ConfigPath
        {
            get
            {
                var syncthingXmlPath =
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Syncthing",
                        Settings.Default.SyncthingConfigFileName);
                return File.Exists(syncthingXmlPath) ? syncthingXmlPath : null;
            }
        }

        public static SyncthingConfig Load()
        {
            return string.IsNullOrEmpty(ConfigPath) ? null : ReadConfig();
        }

        private static void ConfigChanged(object sender, FileSystemEventArgs e)
        {
            if (ConfigurationChanged != null)
                ConfigurationChanged(sender, new SynchtingConfigEventArgs(ReadConfig()));
        }

        private static SyncthingConfig ReadConfig()
        {
            var serializer = new XmlSerializer(typeof(SyncthingConfig));
            using (var fileStream = OpenRead(ConfigPath))
            {
                var conf = serializer.Deserialize(fileStream) as SyncthingConfig;
                return conf;
            }
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


        /// <remarks/>
        private configurationRepository repositoryField;

        private configurationNode[] nodeField;

        private configurationGui guiField;

        private configurationOptions optionsField;

        private byte versionField;

        /// <remarks/>
        public configurationRepository repository
        {
            get
            {
                return this.repositoryField;
            }
            set
            {
                this.repositoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("node")]
        public configurationNode[] node
        {
            get
            {
                return this.nodeField;
            }
            set
            {
                this.nodeField = value;
            }
        }

        /// <remarks/>
        public configurationGui gui
        {
            get
            {
                return this.guiField;
            }
            set
            {
                this.guiField = value;
            }
        }

        /// <remarks/>
        public configurationOptions options
        {
            get
            {
                return this.optionsField;
            }
            set
            {
                this.optionsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class configurationRepository
    {

        private configurationRepositoryNode[] nodeField;

        private configurationRepositoryVersioning versioningField;

        private object syncorderField;

        private string idField;

        private string directoryField;

        private bool roField;

        private bool ignorePermsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("node")]
        public configurationRepositoryNode[] node
        {
            get
            {
                return this.nodeField;
            }
            set
            {
                this.nodeField = value;
            }
        }

        /// <remarks/>
        public configurationRepositoryVersioning versioning
        {
            get
            {
                return this.versioningField;
            }
            set
            {
                this.versioningField = value;
            }
        }

        /// <remarks/>
        public object syncorder
        {
            get
            {
                return this.syncorderField;
            }
            set
            {
                this.syncorderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string directory
        {
            get
            {
                return this.directoryField;
            }
            set
            {
                this.directoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool ro
        {
            get
            {
                return this.roField;
            }
            set
            {
                this.roField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool ignorePerms
        {
            get
            {
                return this.ignorePermsField;
            }
            set
            {
                this.ignorePermsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class configurationRepositoryNode
    {

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class configurationRepositoryVersioning
    {

        private configurationRepositoryVersioningParam paramField;

        private string typeField;

        /// <remarks/>
        public configurationRepositoryVersioningParam param
        {
            get
            {
                return this.paramField;
            }
            set
            {
                this.paramField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class configurationRepositoryVersioningParam
    {

        private string keyField;

        private byte valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class configurationNode
    {

        private string addressField;

        private string idField;

        private string nameField;

        /// <remarks/>
        public string address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class configurationGui
    {

        private string addressField;

        private bool enabledField;

        private bool tlsField;

        /// <remarks/>
        public string address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool enabled
        {
            get
            {
                return this.enabledField;
            }
            set
            {
                this.enabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool tls
        {
            get
            {
                return this.tlsField;
            }
            set
            {
                this.tlsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class configurationOptions
    {

        private string listenAddressField;

        private string globalAnnounceServerField;

        private bool globalAnnounceEnabledField;

        private bool localAnnounceEnabledField;

        private ushort localAnnouncePortField;

        private byte parallelRequestsField;

        private byte maxSendKbpsField;

        private byte rescanIntervalSField;

        private byte reconnectionIntervalSField;

        private ushort maxChangeKbpsField;

        private bool startBrowserField;

        private bool upnpEnabledField;

        private sbyte urAcceptedField;

        /// <remarks/>
        public string listenAddress
        {
            get
            {
                return this.listenAddressField;
            }
            set
            {
                this.listenAddressField = value;
            }
        }

        /// <remarks/>
        public string globalAnnounceServer
        {
            get
            {
                return this.globalAnnounceServerField;
            }
            set
            {
                this.globalAnnounceServerField = value;
            }
        }

        /// <remarks/>
        public bool globalAnnounceEnabled
        {
            get
            {
                return this.globalAnnounceEnabledField;
            }
            set
            {
                this.globalAnnounceEnabledField = value;
            }
        }

        /// <remarks/>
        public bool localAnnounceEnabled
        {
            get
            {
                return this.localAnnounceEnabledField;
            }
            set
            {
                this.localAnnounceEnabledField = value;
            }
        }

        /// <remarks/>
        public ushort localAnnouncePort
        {
            get
            {
                return this.localAnnouncePortField;
            }
            set
            {
                this.localAnnouncePortField = value;
            }
        }

        /// <remarks/>
        public byte parallelRequests
        {
            get
            {
                return this.parallelRequestsField;
            }
            set
            {
                this.parallelRequestsField = value;
            }
        }

        /// <remarks/>
        public byte maxSendKbps
        {
            get
            {
                return this.maxSendKbpsField;
            }
            set
            {
                this.maxSendKbpsField = value;
            }
        }

        /// <remarks/>
        public byte rescanIntervalS
        {
            get
            {
                return this.rescanIntervalSField;
            }
            set
            {
                this.rescanIntervalSField = value;
            }
        }

        /// <remarks/>
        public byte reconnectionIntervalS
        {
            get
            {
                return this.reconnectionIntervalSField;
            }
            set
            {
                this.reconnectionIntervalSField = value;
            }
        }

        /// <remarks/>
        public ushort maxChangeKbps
        {
            get
            {
                return this.maxChangeKbpsField;
            }
            set
            {
                this.maxChangeKbpsField = value;
            }
        }

        /// <remarks/>
        public bool startBrowser
        {
            get
            {
                return this.startBrowserField;
            }
            set
            {
                this.startBrowserField = value;
            }
        }

        /// <remarks/>
        public bool upnpEnabled
        {
            get
            {
                return this.upnpEnabledField;
            }
            set
            {
                this.upnpEnabledField = value;
            }
        }

        /// <remarks/>
        public sbyte urAccepted
        {
            get
            {
                return this.urAcceptedField;
            }
            set
            {
                this.urAcceptedField = value;
            }
        }
    }
    public class SynchtingConfigEventArgs
    {
        public SyncthingConfig Configuration { get; set; }
        public SynchtingConfigEventArgs(SyncthingConfig configuration)
        {
            this.Configuration = configuration;
        }

    }
}
