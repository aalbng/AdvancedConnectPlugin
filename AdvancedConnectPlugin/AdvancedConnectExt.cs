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
using System.Drawing;
using System.IO;
using System.Reflection;

namespace AdvancedConnectPlugin
{
    public sealed class AdvancedConnectPluginExt : Plugin
    {
        public IPluginHost keepassHost = null;
        public Data.Settings settings = null; 
        public String pathToPluginConfigFile = String.Empty;
        private GUI.ToolsMenuExtension toolsMenuExtension = null;
        private GUI.ContextMenuExtension contextMenuExtension = null;
        public Icon pluginIcon = null;


        //Keepass start; Load plugin
        public override bool Initialize(IPluginHost keepassHost)
        {
            //Reference KeePass main application object
            this.keepassHost = keepassHost;

            //Load embedded icon
            System.Reflection.Assembly pluginAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            this.pluginIcon = new Icon(pluginAssembly.GetManifestResourceStream(pluginAssembly.GetName().Name + ".Icon.ico"));
            
            //Load\Create Config 
            buildConfigPath();
            settings = new Data.Settings(this);
            settings = this.settings.load();
                        
            //Extend Tools Menu (Options window)
            this.toolsMenuExtension = new GUI.ToolsMenuExtension(this);
            this.toolsMenuExtension.extendToolsMenu();

            //Contextmenu extension (Add handlers)
            this.contextMenuExtension = new GUI.ContextMenuExtension(this);
            this.contextMenuExtension.extendEntryContextMenu();

            return true;
        }

        //Keepass shutdown; Unload plugin
        public override void Terminate()
        {
            this.toolsMenuExtension.removeToolsMenuExtensions();
        }
        
        //Build configuration
        public void buildConfigPath()
        {
            //Set configuration file name
            String configFileName = "AdvancedConnect.xml";
            String configDirectory = String.Empty;

            //Check if portable / admin configuration is available      
            if (File.Exists(Path.Combine(ExecutableDirectory.GetExecutableDirectory(), configFileName)))
            {
                //Set directory in installation path (portable configuration)
                configDirectory = ExecutableDirectory.GetExecutableDirectory();
                this.pathToPluginConfigFile = Path.Combine(configDirectory, configFileName);
            }
            else
            {
                //Set and create directory within appdata roaming (user configuration)
                configDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KeePass/");
                System.IO.Directory.CreateDirectory(configDirectory);
                this.pathToPluginConfigFile = Path.Combine(configDirectory, configFileName);
            }
        }

        //Keepass update check
        public override string UpdateUrl
        {
            get { return "https://raw.githubusercontent.com/gusowski1/AdvancedConnectPlugin/master/version.txt"; }
        }
    }
}
