/*
Copyright 2016 TGW Software Services GmbH

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is 
distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
See the License for the specific language governing permissions and limitations under the License.
*/
using KeePassLib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace AdvancedConnectPlugin.GUI
{
    class ContextMenuExtension
    {
        private AdvancedConnectPluginExt plugin = null;
        private ContextMenuStrip entryContextMenu = null;

        private List<ToolStripItem> menuItemList = new List<ToolStripItem>(); 

        public ContextMenuExtension(AdvancedConnectPluginExt plugin)
        {
            this.plugin = plugin;
            this.entryContextMenu = this.plugin.keepassHost.MainWindow.EntryContextMenu;
        }

        public void extendEntryContextMenu()
        {
            this.plugin.keepassHost.MainWindow.EntryContextMenu.Opened += new EventHandler(entryContextMenu_Opened);
            this.plugin.keepassHost.MainWindow.EntryContextMenu.Closed += new ToolStripDropDownClosedEventHandler(entryContextMenu_Closed);
        }

        private void entryContextMenu_Opened(object sender, EventArgs e)
        {
            //Check if only one entry is selected
            PwEntry[] selectedEntries = this.plugin.keepassHost.MainWindow.GetSelectedEntries();
            if (selectedEntries != null && selectedEntries.Length == 1)
            {
                //Define menuItem var
                ToolStripMenuItem menuItem = null;

                //Add seperator to list 
                menuItemList.Add(new ToolStripSeparator());

                //Load connection methods into array (splitted by new line)
                String[] methodArr = Tools.StringCustom.splitByNewLine(selectedEntries[0].Strings.ReadSafe(this.plugin.settings.connectionMethodField));

                //Add a menuItem for each connection method and each programm with this connection method                
                foreach (var method in methodArr)
                {
                    //Add Builtin RDP support entries
                    if (this.plugin.settings.enableBuiltinRDP && this.plugin.settings.rdpConnectionMethod == method)
                    {
                        menuItem = new ToolStripMenuItem();
                        menuItem.Text = "Remote Desktop";
                        try { menuItem.Image = System.Drawing.Icon.ExtractAssociatedIcon(Data.RDPConnectionItem.pathToRemoteDesktop).ToBitmap(); } catch (Exception) { } //Extracts the icon from the executable and sets it as context menut item bitmap
                        menuItem.Tag = new Data.RDPConnectionItem(this.plugin, selectedEntries[0], false); //Contains the spezific connectionitem (kpentry) object reference
                        menuItem.Click += entryContextMenuItem_RDPApplication_Click;
                        menuItemList.Add(menuItem);

                        menuItem = new ToolStripMenuItem();
                        menuItem.Text = "Remote Desktop (Console)";
                        try { menuItem.Image = System.Drawing.Icon.ExtractAssociatedIcon(Data.RDPConnectionItem.pathToRemoteDesktop).ToBitmap(); } catch (Exception) { } //Extracts the icon from the executable and sets it as context menut item bitmap
                        menuItem.Tag = new Data.RDPConnectionItem(this.plugin, selectedEntries[0], true); //Contains the spezific connectionitem (kpentry) object reference
                        menuItem.Click += entryContextMenuItem_RDPApplication_Click;
                        menuItemList.Add(menuItem);
                    }

                    //Add Custom applications (loop)
                    foreach (var application in this.plugin.settings.applicationsBindingList)
                    {
                        if(method == application.method)
                        {
                            menuItem = new ToolStripMenuItem();
                            menuItem.Text = application.name;
                            try { menuItem.Image = System.Drawing.Icon.ExtractAssociatedIcon(application.path).ToBitmap(); } catch (Exception) { } //Extracts the icon from the executable and sets it as context menut item bitmap
                            menuItem.Tag = new Data.CustomConnectionItem(this.plugin, application, selectedEntries[0]); //Contains the spezific connectionitem (kpentry + applicationitem) object reference
                            menuItem.Click += entryContextMenuItem_CustomApplication_Click;
                            menuItemList.Add(menuItem);
                        }
                    }
                }
                                
                
                //Insert all items to the top of the context menu (Item order will be reversed)
                foreach (var item in menuItemList)
                {
                    this.entryContextMenu.Items.Insert(0, item);                    
                }
                
            }
        }

        private void entryContextMenuItem_CustomApplication_Click(object sender, EventArgs e)
        {
            String errorMessage = String.Empty;

            Data.CustomConnectionItem connectionItem = (Data.CustomConnectionItem)((ToolStripMenuItem)sender).Tag;
            if(connectionItem.startConnection(out errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void entryContextMenuItem_RDPApplication_Click(object sender, EventArgs e)
        {
            String errorMessage = String.Empty;

            Data.RDPConnectionItem connectionItem = (Data.RDPConnectionItem)((ToolStripMenuItem)sender).Tag;
            if (connectionItem.startConnection(out errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void entryContextMenu_Closed(object sender, EventArgs e)
        {
            //Remove items from context menu
            foreach (var item in menuItemList)
            {
                this.entryContextMenu.Items.Remove(item);
            }

            //Clear menuItemList
            this.menuItemList.Clear();

        }



    }
}
