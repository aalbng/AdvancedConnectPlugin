/*
Copyright 2026 Andreas Albang

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is 
distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
See the License for the specific language governing permissions and limitations under the License.
*/
using AdvancedConnectPlugin.Tools;
using System;
using System.IO;
using System.Xml.Serialization;

namespace AdvancedConnectPlugin.Data
{
    [Serializable()]
    public class Settings
    {
        [XmlIgnore]
        private AdvancedConnectPluginExt plugin = null;

        [XmlElement(ElementName = "ConnectionMethodField")]
        public String connectionMethodField = String.Empty;
        [XmlElement(ElementName = "ConnectionOptionsField")]
        public String connectionOptionsField = String.Empty;

        [XmlElement(ElementName = "SuppressUnresolvedFieldWarning")]
        public Boolean suppressUnresolvedFieldWarning = false;

        [XmlElement(ElementName = "EnableBuiltinRDP")]
        public Boolean enableBuiltinRDP = true;
        [XmlElement(ElementName = "RDPConnectionAddressField")]
        public String rdpConnectionAddressField = String.Empty;
        [XmlElement(ElementName = "RDPConnectionMethod")]
        public String rdpConnectionMethod = String.Empty;
        [XmlElement(ElementName = "RDPCustomParameter")]
        public String rdpCustomParameter = String.Empty;

        //Milliseconds to keep the temporary RDP credential available after mstsc signals it is
        //ready (used as a safety buffer before the credential is removed again).
        [XmlElement(ElementName = "RDPCredentialCleanupDelay")]
        public Int32 rdpCredentialCleanupDelay = 5000;

        [XmlArray("ApplicationList"),XmlArrayItem(Type = typeof(ApplicationItem))]
        public SortableBindingList<ApplicationItem> applicationsBindingList = new SortableBindingList<ApplicationItem>();

        public Settings()
        {
        }
        public Settings(AdvancedConnectPluginExt plugin) : this()
        {
            this.plugin = plugin;            
        }


        public Settings load()
        {
            Settings loadedSettings = null;

            //Load settings from file if possible or create a new one
            if (File.Exists(this.plugin.pathToPluginConfigFile))
            {
                XmlSerializer serializerObj = new XmlSerializer(typeof(Settings));

                //Try to parse settings from configuration file. Create new configuration on parsing error
                try
                {
                    using (FileStream readFileStream = new FileStream(this.plugin.pathToPluginConfigFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        //Load configuration
                        loadedSettings = (Settings)serializerObj.Deserialize(readFileStream);
                    }
                }
                catch (InvalidOperationException)
                {
                    //Create new configuration on error
                    loadedSettings = new Settings();
                }
            }
            else
            {
                //Create new configuration file if there is no configuration file present
                loadedSettings = new Settings();  
            }
            
            loadedSettings.plugin = this.plugin;
            return loadedSettings;
        }

        public bool save(out String errorMessage)
        {
            errorMessage = String.Empty;

            try
            {
                XmlSerializer serializerObj = new XmlSerializer(typeof(Settings));
                using (TextWriter writeFileStream = new StreamWriter(this.plugin.pathToPluginConfigFile))
                {
                    serializerObj.Serialize(writeFileStream, this);
                }
            }
            catch (Exception saveException)
            {
                //Surface the concrete reason (access denied, path not found, ...) to the caller.
                errorMessage = saveException.Message;
                return false;
            }

            return true;            
        }

    }
}
