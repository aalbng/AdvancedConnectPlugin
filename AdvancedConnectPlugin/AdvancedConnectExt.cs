/*
Copyright 2016 TGW Software Services GmbH

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is 
distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
See the License for the specific language governing permissions and limitations under the License.
*/
using KeePass.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;




namespace AdvancedConnectPlugin
{
    public sealed class AdvancedConnectPluginExt : KeePass.Plugins.Plugin
    {
        public IPluginHost keepassHost = null;
        public Data.Settings settings = null;
        public String pathToPluginConfig = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KeePass\\AdvancedConnect.xml"); 
        private GUI.ToolsMenuExtension toolsMenuExtension = null;
        private GUI.ContextMenuExtension contextMenuExtension = null;
        
        
        //Keepass start; Load plugin
        public override bool Initialize(IPluginHost keepassHost)
        {
            //Reference KeePass main application object
            this.keepassHost = keepassHost;

            //Load\Create Config 
            settings = new Data.Settings(this);
            settings = this.settings.load();
                        
            //Extend Tools Menu (Options window)
            toolsMenuExtension = new GUI.ToolsMenuExtension(this);
            toolsMenuExtension.extendToolsMenu();

            //Contextmenu extension (Add handlers)
            contextMenuExtension = new GUI.ContextMenuExtension(this);
            contextMenuExtension.extendEntryContextMenu();

            return true;
        }

        //Keepass shutdown; Unload plugin
        public override void Terminate()
        {

        }
        


    }
}
