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

        private ToolStripItemCollection toolStripMenu = null;
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
            this.toolStripMenu = this.plugin.keepassHost.MainWindow.ToolsMenu.DropDownItems;

            // Add a separator at the bottom
            this.pluginMenuSeperator = new ToolStripSeparator();
            this.toolStripMenu.Add(this.pluginMenuSeperator);

            // Add menu item for "Advanced Connect"
            this.pluginMenuItem = new ToolStripMenuItem();
            this.pluginMenuItem.Text = "Advanced Connect";
            this.pluginMenuItem.Image = (System.Drawing.Image)this.plugin.pluginIcon.ToBitmap();
            this.toolStripMenu.Add(this.pluginMenuItem);

            // Add menu item for "Advanced Conenct -> Options"
            this.pluginMenuItemOptions = new ToolStripMenuItem();
            this.pluginMenuItemOptions.Text = "Options";
            this.pluginMenuItemOptions.Click += new EventHandler(optionsOnClick);
            this.pluginMenuItem.DropDownItems.Add(this.pluginMenuItemOptions);

            // Add menu item for "Advanced Conenct -> About"
            this.pluginMenuItemAbout = new ToolStripMenuItem();
            this.pluginMenuItemAbout.Text = "About";
            this.pluginMenuItemAbout.Click += new EventHandler(aboutOnClick);
            this.pluginMenuItem.DropDownItems.Add(pluginMenuItemAbout);
            

        }

        //Remove extension
        public void removeToolsMenuExtensions()
        {
            this.toolStripMenu.Remove(this.pluginMenuSeperator);
            this.toolStripMenu.Remove(this.pluginMenuItem);
            this.pluginMenuItem.DropDownItems.Remove(this.pluginMenuItemOptions);
            this.pluginMenuItem.DropDownItems.Remove(this.pluginMenuItemAbout);
        }


        void optionsOnClick(object sender, EventArgs e)
        {
            Form options = new Options(this.plugin);
            options.ShowDialog(this.plugin.keepassHost.MainWindow);

        }

        void aboutOnClick(object sender, EventArgs e)
        {
            Form about = new About(this.plugin);
            about.ShowDialog(this.plugin.keepassHost.MainWindow);

        }





    }

}
