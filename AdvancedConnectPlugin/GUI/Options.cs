/*
Copyright 2016 TGW Software Services GmbH

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is 
distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
See the License for the specific language governing permissions and limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AdvancedConnectPlugin.GUI
{
    public partial class Options : Form
    {
        private AdvancedConnectPluginExt plugin = null;
        private List<String> dbFields = null;

        public Options(AdvancedConnectPluginExt plugin)
        {
            this.plugin = plugin;

            InitializeComponent();

            //Customize GUI from designer
            this.Icon = (System.Drawing.Icon)Properties.Resources.ICO_AdvancedConnect;

            //Add empty values to text-, ceck- and comboboxes
            this.comboBoxConnectionMethod.Items.Add(String.Empty);
            this.comboBoxConncectionOptions.Items.Add(String.Empty);
            this.checkBoxEnableBuiltinRDP.Checked = true;
            this.comboBoxRDPConncectionAddress.Items.Add(String.Empty);
            this.textBoxRDPConnectionMethod.Text = String.Empty;
            this.textBoxRDPCustomParameter.Text = String.Empty;

            //Load connection mapping from config
            this.comboBoxConnectionMethod.Text = plugin.settings.connectionMethodField;
            this.comboBoxConncectionOptions.Text = plugin.settings.connectionOptionsField;
            this.checkBoxEnableBuiltinRDP.Checked = plugin.settings.enableBuiltinRDP;
            this.comboBoxRDPConncectionAddress.Text = plugin.settings.rdpConnectionAddressField;
            this.textBoxRDPConnectionMethod.Text = plugin.settings.rdpConnectionMethod;
            this.textBoxRDPCustomParameter.Text = plugin.settings.rdpCustomParameter;
            
            //Check if database is open to load the custom values from db
            //(Lock configuration items if databse is closed)
            if (this.plugin.keepassHost.Database !=null && this.plugin.keepassHost.Database.IsOpen)
            {
                this.dbFields = getDBFields();
                foreach (var field in dbFields)
                {
                    this.comboBoxConnectionMethod.Items.Add(field);
                    this.comboBoxConncectionOptions.Items.Add(field);
                    this.comboBoxRDPConncectionAddress.Items.Add(field);
                }            
            } else
            {
                this.comboBoxConnectionMethod.Enabled = false;
                this.comboBoxConncectionOptions.Enabled = false;
                this.comboBoxRDPConncectionAddress.Enabled = false;
            }

            //Building GridView with BindingList as source
            this.dataGridViewApplications.DataSource = plugin.settings.applicationsBindingList;

            this.dataGridViewApplications.Columns["name"].HeaderText = "Application Name";
            this.dataGridViewApplications.Columns["name"].MinimumWidth = 100;
            this.dataGridViewApplications.Columns["name"].Width = 100;
            this.dataGridViewApplications.Columns["name"].DisplayIndex = 0;

            this.dataGridViewApplications.Columns["method"].HeaderText = "Method / Protocol";
            this.dataGridViewApplications.Columns["method"].Width = 70;
            this.dataGridViewApplications.Columns["method"].MinimumWidth = 70;
            this.dataGridViewApplications.Columns["method"].DisplayIndex = 1;

            this.dataGridViewApplications.Columns["path"].HeaderText = "Path";
            this.dataGridViewApplications.Columns["path"].Width = 120;
            this.dataGridViewApplications.Columns["path"].MinimumWidth = 120;
            this.dataGridViewApplications.Columns["path"].DisplayIndex = 2;

            this.dataGridViewApplications.Columns["options"].HeaderText = "Commandline Options";
            this.dataGridViewApplications.Columns["options"].Width = 100;
            this.dataGridViewApplications.Columns["options"].MinimumWidth = 100;
            this.dataGridViewApplications.Columns["options"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewApplications.Columns["options"].DisplayIndex = 3;

            //Sort columns initial by name
            this.dataGridViewApplications.Sort(this.dataGridViewApplications.Columns["name"], ListSortDirection.Ascending);            
        }


        //Loads all item fields (including custom fields)
        private List<String> getDBFields()
        {
            List<String> dbFields = new List<String>();
            foreach (var pwEntry in this.plugin.keepassHost.Database.RootGroup.GetEntries(true))
            {
                foreach (var str in pwEntry.Strings.GetKeys())
                {
                    if (!dbFields.Contains(str))
                    {
                        dbFields.Add(str);
                    }
                }
            }
            dbFields.Sort();

            return dbFields;
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            //Write fields into settings;
            this.plugin.settings.connectionMethodField = this.comboBoxConnectionMethod.Text;
            this.plugin.settings.connectionOptionsField = this.comboBoxConncectionOptions.Text;
            this.plugin.settings.enableBuiltinRDP = this.checkBoxEnableBuiltinRDP.Checked;
            this.plugin.settings.rdpConnectionAddressField = this.comboBoxRDPConncectionAddress.Text;
            this.plugin.settings.rdpConnectionMethod = this.textBoxRDPConnectionMethod.Text;
            this.plugin.settings.rdpCustomParameter = this.textBoxRDPCustomParameter.Text;


            //Write settings to settings file
            if (this.plugin.settings.save() == false)
            {
                MessageBox.Show(("Configuration " + this.plugin.pathToPluginConfig + " could not be written." ), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.buttonApply_Click(sender, e);
            this.Close();
        }

        private void buttonApplicationRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell oneCell in this.dataGridViewApplications.SelectedCells)
            {
                if (oneCell.Selected)
                {
                    this.dataGridViewApplications.Rows.RemoveAt(oneCell.RowIndex);
                }
            }
        }

        private void buttonApplicationAdd_Click(object sender, EventArgs e)
        {
            this.plugin.settings.applicationsBindingList.Add(new Data.ApplicationItem());
        }


    }
}
