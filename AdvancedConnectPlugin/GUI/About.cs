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
using System.Reflection;
using System.Windows.Forms;

namespace AdvancedConnectPlugin.GUI
{
    public partial class About : Form
    {
        private AdvancedConnectPluginExt plugin = null;

        public About(AdvancedConnectPluginExt plugin)
        {
            this.plugin = plugin;

            InitializeComponent();

            //Customize GUI from Designer
            this.Icon = (System.Drawing.Icon)this.plugin.pluginIcon;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

                        this.labelTitle.Text = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyTitleAttribute), false)).Title;
            this.labelVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.labelCopyright.Text = ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute), false)).Copyright + " " + ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute), false)).Company;

        }
    }
}
