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
using KeePassLib;
using System;
using System.IO;
using System.Threading;

namespace AdvancedConnectPlugin.Data
{
    public class CustomConnectionItem : ConnectionItem
    {
        private ApplicationItem application = null;
        private String customConnectionOptions = String.Empty;

        public CustomConnectionItem(AdvancedConnectPluginExt plugin, ApplicationItem application, PwEntry keepassEntry)
        {
            this.plugin = plugin;
            this.keepassDatabase = this.plugin.keepassHost.Database;
            this.application = application;
            this.keepassEntry = keepassEntry;
            this.customConnectionOptions = this.application.options;
        }
                
        public Boolean startConnection(out String errorMessage)
        {
            errorMessage = String.Empty;

            //Check if application path exist with resolved OS variables
            if (File.Exists(Environment.ExpandEnvironmentVariables(this.application.path)))
            {
                //Overwrite application options if set in keepass entry
                if (this.keepassEntry.Strings.ReadSafe(this.plugin.settings.connectionOptionsField).Length > 0)
                {
                    this.customConnectionOptions = this.keepassEntry.Strings.ReadSafe(this.plugin.settings.connectionOptionsField);
                }
                
                //Create a thread to allow non gui blocking sleeps
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true; //Background threads will stop automatically on program close
                    
                    //Fill placeholders in options and start programm
                    StartProcess.Start(fillPlaceholders(this.application.path), fillPlaceholders(this.customConnectionOptions));
                }).Start();


                return true;
            }else
            {
                errorMessage = ("Application '" + application.path + "' not found!");
                return false;
            }

        }

    }
}
