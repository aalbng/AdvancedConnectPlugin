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
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace AdvancedConnectPlugin.GUI
{
    class ToolsMenuExtension {

        private AdvancedConnectPluginExt plugin = null;

        private ToolStripSeparator pluginMenuSeperator = null;
        private ToolStripMenuItem pluginMenuItem = null;
        private ToolStripMenuItem pluginMenuItemAbout = null;
        private ToolStripMenuItem pluginMenuItemOptions = null;

        public ToolsMenuExtension(AdvancedConnectPluginExt plugin)
        {
            this.plugin = plugin;
        }


        public void extendToolsMenu()
        {
            // Get a reference to the 'Tools' menu item container
            ToolStripItemCollection toolStripMenu = this.plugin.keepassHost.MainWindow.ToolsMenu.DropDownItems;

            // Add a separator at the bottom
            pluginMenuSeperator = new ToolStripSeparator();
            toolStripMenu.Add(pluginMenuSeperator);

            // Add menu item for "Advanced Connect"
            pluginMenuItem = new ToolStripMenuItem();
            pluginMenuItem.Text = "Advanced Connect";
            pluginMenuItem.Image = (System.Drawing.Image)Properties.Resources.IMG_AdvancedConnect;
            toolStripMenu.Add(pluginMenuItem);

            // Add menu item for "Advanced Conenct -> Options"
            pluginMenuItemOptions = new ToolStripMenuItem();
            pluginMenuItemOptions.Text = "Options";
            pluginMenuItemOptions.Click += new EventHandler(optionsOnClick);
            pluginMenuItem.DropDownItems.Add(pluginMenuItemOptions);

            // Add menu item for "Advanced Conenct -> About"
            pluginMenuItemAbout = new ToolStripMenuItem();
            pluginMenuItemAbout.Text = "About";
            pluginMenuItemAbout.Click += new EventHandler(aboutOnClick);
            pluginMenuItem.DropDownItems.Add(pluginMenuItemAbout);
            

        }

        void optionsOnClick(object sender, EventArgs e)
        {
            Form options = new Options(this.plugin);
            options.ShowDialog(this.plugin.keepassHost.MainWindow);

        }

        void aboutOnClick(object sender, EventArgs e)
        {
            Form about = new About();
            about.ShowDialog(this.plugin.keepassHost.MainWindow);

        }





    }

}
