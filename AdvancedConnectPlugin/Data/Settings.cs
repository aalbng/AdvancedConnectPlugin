/*
Copyright 2016 TGW Software Services GmbH

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is 
distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
See the License for the specific language governing permissions and limitations under the License.
*/
using AdvancedConnectPlugin.Tools;
using KeePass.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
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

        [XmlElement(ElementName = "EnableBuiltinRDP")]
        public Boolean enableBuiltinRDP = true;
        [XmlElement(ElementName = "RDPConnectionAddressField")]
        public String rdpConnectionAddressField = String.Empty;
        [XmlElement(ElementName = "RDPConnectionMethod")]
        public String rdpConnectionMethod = String.Empty;
        [XmlElement(ElementName = "RDPCustomParameter")]
        public String rdpCustomParameter = String.Empty;

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
                FileStream readFileStream = new FileStream(this.plugin.pathToPluginConfigFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                loadedSettings = (Settings)serializerObj.Deserialize(readFileStream);
                readFileStream.Close();
            }
            else
            {
                loadedSettings = new Settings();  
            }



            loadedSettings.plugin = this.plugin;
            return loadedSettings;
        }

        public bool save()
        {
            try
            {
                XmlSerializer serializerObj = new XmlSerializer(typeof(Settings));
                TextWriter writeFileStream = new StreamWriter(this.plugin.pathToPluginConfigFile);
                serializerObj.Serialize(writeFileStream, this);
                writeFileStream.Close();
            }
            catch (Exception)
            {
                return false;
            }

            return true;            
        }

    }
}
