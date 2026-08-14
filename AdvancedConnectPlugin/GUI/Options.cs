/*
Copyright 2026 Andreas Albang

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
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedConnectPlugin.GUI
{
    public partial class Options : Form
    {
        private AdvancedConnectPluginExt plugin = null;
        private List<String> dbFields = null;

        //Transparent 1x1 placeholder used for the icon cell when no KeePass icon is selected.
        private static readonly Image emptyIconPlaceholder = new Bitmap(1, 1);

        public Options(AdvancedConnectPluginExt plugin)
        {
            this.plugin = plugin;

            InitializeComponent();

            //Customize GUI from designer
            this.Icon = (System.Drawing.Icon)this.plugin.pluginIcon;

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
            this.checkBoxSuppressUnresolvedFieldWarning.Checked = plugin.settings.suppressUnresolvedFieldWarning;
            this.checkBoxSuppressWorkingDirectoryWarning.Checked = plugin.settings.suppressWorkingDirectoryWarning;
            this.textBoxRDPCredentialCleanupDelay.Text = plugin.settings.rdpCredentialCleanupDelay.ToString();
            
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

            //Icon column (KeePass standard or custom database icon) as the first column.
            //Not data-bound: filled per row from the ApplicationItem and edited via the IconPickerForm.
            DataGridViewImageColumn iconColumn = this.dataGridViewApplications.Columns["icon"] as DataGridViewImageColumn;
            if (iconColumn == null)
            {
                iconColumn = new DataGridViewImageColumn();
                iconColumn.Name = "icon";
                this.dataGridViewApplications.Columns.Add(iconColumn);
            }
            iconColumn.HeaderText = "Icon";
            iconColumn.Width = 40;
            iconColumn.MinimumWidth = 40;
            iconColumn.DisplayIndex = 0;
            iconColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            iconColumn.ReadOnly = true;

            //The icon selection properties are auto-generated as columns by the data binding; hide them
            //(they are edited through the icon column / icon picker, not shown as raw text columns).
            if (this.dataGridViewApplications.Columns.Contains("iconId"))
            {
                this.dataGridViewApplications.Columns["iconId"].Visible = false;
            }
            if (this.dataGridViewApplications.Columns.Contains("customIconPngBase64"))
            {
                this.dataGridViewApplications.Columns["customIconPngBase64"].Visible = false;
            }

            this.dataGridViewApplications.Columns["name"].HeaderText = "Application Name";
            this.dataGridViewApplications.Columns["name"].MinimumWidth = 100;
            this.dataGridViewApplications.Columns["name"].Width = 100;
            this.dataGridViewApplications.Columns["name"].DisplayIndex = 1;

            this.dataGridViewApplications.Columns["method"].HeaderText = "Method / Protocol";
            this.dataGridViewApplications.Columns["method"].Width = 70;
            this.dataGridViewApplications.Columns["method"].MinimumWidth = 70;
            this.dataGridViewApplications.Columns["method"].DisplayIndex = 2;

            this.dataGridViewApplications.Columns["path"].HeaderText = "Path";
            this.dataGridViewApplications.Columns["path"].Width = 120;
            this.dataGridViewApplications.Columns["path"].MinimumWidth = 120;
            this.dataGridViewApplications.Columns["path"].DisplayIndex = 3;

            this.dataGridViewApplications.Columns["options"].HeaderText = "Commandline Options";
            this.dataGridViewApplications.Columns["options"].Width = 100;
            this.dataGridViewApplications.Columns["options"].MinimumWidth = 100;
            this.dataGridViewApplications.Columns["options"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewApplications.Columns["options"].DisplayIndex = 4;

            //Hidden helper columns for the (non-visible) icon selection are not needed:
            //the selection lives on the ApplicationItem; the image is refreshed from there.
            this.dataGridViewApplications.CellDoubleClick += new DataGridViewCellEventHandler(dataGridViewApplications_CellDoubleClick);
            //Right clicking the icon cell offers removing a set icon (restore the exe icon default).
            this.dataGridViewApplications.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridViewApplications_CellMouseDown);
            this.dataGridViewApplications.RowsAdded += new DataGridViewRowsAddedEventHandler(dataGridViewApplications_RowsAdded);
            //Unbound icon cell values are cleared when a bound grid is sorted, so refresh them afterwards.
            this.dataGridViewApplications.Sorted += new EventHandler(dataGridViewApplications_Sorted);
            refreshAllIconCells();

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
            //Event handler kept with the designer-expected signature; the result is only needed by OK.
            this.applySettings();
        }

        //Writes the dialog values into the settings and persists them.
        //Returns false (and shows the concrete reason) if saving the configuration failed.
        private bool applySettings()
        {
            //Write fields into settings;
            this.plugin.settings.connectionMethodField = this.comboBoxConnectionMethod.Text;
            this.plugin.settings.connectionOptionsField = this.comboBoxConncectionOptions.Text;
            this.plugin.settings.enableBuiltinRDP = this.checkBoxEnableBuiltinRDP.Checked;
            this.plugin.settings.rdpConnectionAddressField = this.comboBoxRDPConncectionAddress.Text;
            this.plugin.settings.rdpConnectionMethod = this.textBoxRDPConnectionMethod.Text;
            this.plugin.settings.rdpCustomParameter = this.textBoxRDPCustomParameter.Text;
            this.plugin.settings.suppressUnresolvedFieldWarning = this.checkBoxSuppressUnresolvedFieldWarning.Checked;
            this.plugin.settings.suppressWorkingDirectoryWarning = this.checkBoxSuppressWorkingDirectoryWarning.Checked;

            //Parse the RDP credential cleanup delay (fall back to previous value on invalid input)
            Int32 parsedCleanupDelay;
            if (Int32.TryParse(this.textBoxRDPCredentialCleanupDelay.Text, out parsedCleanupDelay) && parsedCleanupDelay >= 0)
            {
                this.plugin.settings.rdpCredentialCleanupDelay = parsedCleanupDelay;
            }
            else
            {
                MessageBox.Show("'RDP credential cleanup (ms)' must be a non-negative whole number. The previous value was kept.",
                    "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.textBoxRDPCredentialCleanupDelay.Text = this.plugin.settings.rdpCredentialCleanupDelay.ToString();
            }


            //Write settings to settings file
            String saveErrorMessage;
            if (this.plugin.settings.save(out saveErrorMessage) == false)
            {
                MessageBox.Show(
                    "Configuration '" + this.plugin.pathToPluginConfigFile + "' could not be written."
                    + Environment.NewLine + Environment.NewLine + saveErrorMessage,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //Only close the dialog if the configuration was saved successfully, so a save error stays visible.
            if (this.applySettings())
            {
                this.Close();
            }
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

        //Returns the ApplicationItem bound to a given grid row (null if not resolvable).
        private Data.ApplicationItem getApplicationItem(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= this.dataGridViewApplications.Rows.Count)
            {
                return null;
            }
            return this.dataGridViewApplications.Rows[rowIndex].DataBoundItem as Data.ApplicationItem;
        }

        //Refreshes the image shown in the icon cell of a single row from its ApplicationItem.
        private void refreshIconCell(int rowIndex)
        {
            Data.ApplicationItem application = getApplicationItem(rowIndex);
            if (application == null)
            {
                return;
            }

            DataGridViewImageCell iconCell = this.dataGridViewApplications.Rows[rowIndex].Cells["icon"] as DataGridViewImageCell;
            if (iconCell == null)
            {
                return;
            }

            System.Drawing.Image iconImage = Tools.ApplicationIcon.Resolve(this.plugin.keepassHost, application);
            //Use a 1x1 transparent placeholder when no KeePass icon is selected, so the cell stays empty
            //instead of showing the default "broken image" glyph.
            if (iconImage != null)
            {
                iconCell.Value = iconImage;
            }
            else
            {
                iconCell.Value = emptyIconPlaceholder;
            }
        }

        //Refreshes the icon cells of all rows.
        private void refreshAllIconCells()
        {
            for (int i = 0; i < this.dataGridViewApplications.Rows.Count; i++)
            {
                refreshIconCell(i);
            }
        }

        private void dataGridViewApplications_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
            {
                refreshIconCell(e.RowIndex + i);
            }
        }

        private void dataGridViewApplications_Sorted(object sender, EventArgs e)
        {
            //Sorting a data-bound grid re-creates the rows and clears the unbound icon cells.
            refreshAllIconCells();
        }

        //Double clicking the icon cell opens the KeePass icon picker for that application.
        private void dataGridViewApplications_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (this.dataGridViewApplications.Columns[e.ColumnIndex].Name != "icon")
            {
                return;
            }

            Data.ApplicationItem application = getApplicationItem(e.RowIndex);
            if (application == null)
            {
                return;
            }

            pickIconForApplication(application);
            refreshIconCell(e.RowIndex);
        }

        //Right clicking the icon cell shows a context menu to remove a set icon and restore the
        //default behaviour (fall back to the executable's own icon).
        private void dataGridViewApplications_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (this.dataGridViewApplications.Columns[e.ColumnIndex].Name != "icon")
            {
                return;
            }

            Data.ApplicationItem application = getApplicationItem(e.RowIndex);
            if (application == null)
            {
                return;
            }

            int rowIndex = e.RowIndex;

            ContextMenuStrip iconContextMenu = new ContextMenuStrip();
            ToolStripMenuItem removeIconItem = new ToolStripMenuItem("Remove icon (use application icon)");
            removeIconItem.Enabled = application.hasIcon();
            removeIconItem.Click += delegate(object menuSender, EventArgs menuArgs)
            {
                application.clearIcon();
                refreshIconCell(rowIndex);
            };
            iconContextMenu.Items.Add(removeIconItem);

            System.Drawing.Rectangle cellRectangle = this.dataGridViewApplications.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            iconContextMenu.Show(this.dataGridViewApplications, cellRectangle.Left + e.X, cellRectangle.Top + e.Y);
        }

        //Shows the KeePass IconPickerForm and stores the chosen standard/custom icon on the application.
        private void pickIconForApplication(Data.ApplicationItem application)
        {
            KeePassLib.PwDatabase database = this.plugin.keepassHost.Database;

            //Already selected custom icons are shown database-independently (stored as Base64 PNG), but
            //picking a NEW custom icon needs an open database, because the icon picker lists the custom
            //icons of the currently open database. Inform the user when no database is open.
            if (database == null || !database.IsOpen)
            {
                MessageBox.Show(
                    "No database is open. You can still choose a standard KeePass icon, but custom database icons are only available while a database is open.",
                    "Open a database to choose a custom icon",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //Custom (database) icons require an open database; standard icons are always available.
            uint numberOfStandardIcons = (uint)KeePassLib.PwIcon.Count;

            uint defaultIcon = 0;
            if (application.iconId != Data.ApplicationItem.NoIcon
                && application.iconId >= 0
                && application.iconId < (int)numberOfStandardIcons)
            {
                defaultIcon = (uint)application.iconId;
            }

            //Custom icons are stored self-contained as Base64 PNG data, so there is no persisted UUID to
            //preselect. Pass Zero (no custom icon) as the picker default.
            KeePassLib.PwUuid defaultCustomIcon = KeePassLib.PwUuid.Zero;

            KeePass.Forms.IconPickerForm iconPicker = new KeePass.Forms.IconPickerForm();
            iconPicker.InitEx(
                this.plugin.keepassHost.MainWindow.ClientIcons,
                numberOfStandardIcons,
                database,
                defaultIcon,
                defaultCustomIcon);

            try
            {
                if (iconPicker.ShowDialog(this) == DialogResult.OK)
                {
                    KeePassLib.PwUuid chosenCustomIcon = iconPicker.ChosenCustomIconUuid;
                    if (chosenCustomIcon != null && !chosenCustomIcon.Equals(KeePassLib.PwUuid.Zero))
                    {
                        //Custom database icon chosen: copy the PNG data out of the database and store it
                        //Base64 encoded on the application, so it stays independent of the open database.
                        byte[] pngData = Tools.ApplicationIcon.GetCustomIconPngData(database, chosenCustomIcon);
                        application.customIconPngBase64 = Tools.ApplicationIcon.EncodeCustomIcon(pngData);
                        application.iconId = Data.ApplicationItem.NoIcon;
                    }
                    else
                    {
                        //Standard icon chosen.
                        application.customIconPngBase64 = String.Empty;
                        application.iconId = (int)iconPicker.ChosenIconId;
                    }
                }
            }
            finally
            {
                iconPicker.Dispose();
            }
        }


    }
}
