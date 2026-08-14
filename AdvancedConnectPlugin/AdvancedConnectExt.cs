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
using KeePass.Plugins;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

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

        //Set by buildConfigPath() when the configuration was loaded from the current working directory
        //(an untrusted location). The warning is shown after the settings are loaded, so it can be
        //suppressed via the SuppressWorkingDirectoryWarning option.
        private Boolean configLoadedFromWorkingDirectory = false;


        //Keepass start; Load plugin
        public override bool Initialize(IPluginHost keepassHost)
        {
            //Reference KeePass main application object
            this.keepassHost = keepassHost;

            //Loading problems must be surfaced to the user (not silently swallowed) and must abort the
            //plugin load cleanly, so KeePass does not end up with a half-initialized plugin.
            try
            {
                //Load embedded icon (fail loudly if the resource is missing, because the menu and the
                //About dialog rely on it and would otherwise crash later with a less obvious error).
                this.pluginIcon = loadPluginIcon();

                //Load\Create Config 
                buildConfigPath();
                settings = new Data.Settings(this);
                settings = this.settings.load();

                //Warn (unless suppressed) when the configuration was loaded from the current working
                //directory. This is done after loading the settings so the user's suppression choice
                //(stored in the configuration itself) is available.
                warnIfConfigLoadedFromWorkingDirectory();

                //Extend Tools Menu (Options window)
                this.toolsMenuExtension = new GUI.ToolsMenuExtension(this);
                this.toolsMenuExtension.extendToolsMenu();

                //Contextmenu extension (Add handlers)
                this.contextMenuExtension = new GUI.ContextMenuExtension(this);
                this.contextMenuExtension.extendEntryContextMenu();
            }
            catch (Exception initializeException)
            {
                //Show the concrete reason and abort the load, instead of throwing an unhandled exception
                //(which KeePass would only report generically) or continuing in a broken state.
                MessageBox.Show(
                    "The Advanced Connect Plugin could not be loaded." + Environment.NewLine + Environment.NewLine
                    + initializeException.Message,
                    "Advanced Connect Plugin - Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            return true;
        }

        //Loads the embedded plugin icon. Throws a descriptive exception if the resource is missing,
        //so a packaging/build problem becomes immediately visible instead of causing a later crash.
        private Icon loadPluginIcon()
        {
            System.Reflection.Assembly pluginAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            String resourceName = pluginAssembly.GetName().Name + ".Icon.ico";

            using (Stream iconStream = pluginAssembly.GetManifestResourceStream(resourceName))
            {
                if (iconStream == null)
                {
                    throw new InvalidOperationException("Embedded icon resource '" + resourceName + "' was not found.");
                }

                return new Icon(iconStream);
            }
        }

        //Keepass shutdown; Unload plugin
        public override void Terminate()
        {
            //Terminate() may be called even if Initialize() aborted early, so guard against not-yet-created members.
            if (this.toolsMenuExtension != null)
            {
                this.toolsMenuExtension.removeToolsMenuExtensions();
            }
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
            else if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), configFileName)))
            {
                //Set directory to the current working directory (portable configuration).
                configDirectory = Directory.GetCurrentDirectory();
                this.pathToPluginConfigFile = Path.Combine(configDirectory, configFileName);

                //Remember that the configuration comes from the (untrusted) working directory. The
                //actual warning is shown later (after the settings are loaded), so it can be
                //suppressed via the SuppressWorkingDirectoryWarning option.
                this.configLoadedFromWorkingDirectory = true;
            }
            else
            {
                //Set and create directory within appdata roaming (user configuration)
                configDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KeePass/");
                System.IO.Directory.CreateDirectory(configDirectory);
                this.pathToPluginConfigFile = Path.Combine(configDirectory, configFileName);
            }
        }

        //Shows a warning when the configuration was loaded from the current working directory, unless
        //the user disabled it via the SuppressWorkingDirectoryWarning option.
        //The current working directory is inherited from whatever started KeePass (for example the
        //folder of a double-clicked .kdbx file) and is not necessarily a trusted location. Because the
        //configuration defines executable paths, command lines and placeholder substitution
        //({USERNAME}/{PASSWORD}), loading it from an untrusted directory can mean running an
        //attacker-chosen program with the user's stored credentials. Suppressing this warning is only
        //safe when the working directory is protected by strict file system permissions (see README).
        private void warnIfConfigLoadedFromWorkingDirectory()
        {
            if (!this.configLoadedFromWorkingDirectory)
            {
                return;
            }
            if (this.settings != null && this.settings.suppressWorkingDirectoryWarning)
            {
                return;
            }

            //Show the warning with a "Do not show again" checkbox, so the user can disable it directly.
            //Suppressing it is only safe when the working directory is protected by strict file system
            //permissions (see README).
            using (GUI.WorkingDirectoryWarningDialog warningDialog =
                new GUI.WorkingDirectoryWarningDialog(this.pluginIcon, this.pathToPluginConfigFile))
            {
                //The dialog is shown during plugin load, before the KeePass main window exists, so it is
                //centered on the screen (StartPosition = CenterScreen) rather than on an owner window.
                warningDialog.ShowDialog();

                //Checkbox ticked = disable the warning from now on and persist the choice.
                if (warningDialog.DoNotShowAgain && this.settings != null)
                {
                    this.settings.suppressWorkingDirectoryWarning = true;

                    String saveErrorMessage;
                    if (!this.settings.save(out saveErrorMessage))
                    {
                        MessageBox.Show(
                            "The warning was disabled for this session, but the configuration could not be saved, "
                            + "so the warning may appear again:"
                            + Environment.NewLine + Environment.NewLine + saveErrorMessage,
                            "Advanced Connect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        //Keepass update check
        public override string UpdateUrl
        {
            get { return "https://raw.githubusercontent.com/aalbng/AdvancedConnectPlugin/master/version.txt"; }
        }
    }
}
