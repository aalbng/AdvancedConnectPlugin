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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace AdvancedConnectPlugin.Data
{
    public class RDPConnectionItem : ConnectionItem
    {
        public static String pathToRemoteDesktop = "C:\\Windows\\System32\\mstsc.exe";
        public static String pathToCMDKey = "C:\\Windows\\System32\\cmdkey.exe";
        private Boolean rdpConsoleSession = false;
        private String rdpCustomParameter = String.Empty;
        private String rdpParameter = String.Empty;
        private String cmdkeyParameter = String.Empty;


        public RDPConnectionItem(AdvancedConnectPluginExt plugin, PwEntry keepassEntry, Boolean rdpConsoleSession)
        {
            this.plugin = plugin;
            this.keepassDatabase = this.plugin.keepassHost.Database;
            this.keepassEntry = keepassEntry;
            this.rdpConsoleSession = rdpConsoleSession;
            this.rdpCustomParameter = this.plugin.settings.rdpCustomParameter;
        }


        public Boolean startConnection(out String errorMessage)
        {
            errorMessage = String.Empty;

            //Check if application path exist
            if (File.Exists(Environment.ExpandEnvironmentVariables(RDPConnectionItem.pathToRemoteDesktop)))
            {
                if (File.Exists(Environment.ExpandEnvironmentVariables(RDPConnectionItem.pathToCMDKey)))
                {
                    //Overwrite the default rdp parameter if set in keepass entry
                    if (this.keepassEntry.Strings.ReadSafe(this.plugin.settings.connectionOptionsField).Length > 0)
                    {
                        this.rdpCustomParameter = this.keepassEntry.Strings.ReadSafe(this.plugin.settings.connectionOptionsField);
                    }
                    
                    //Create a thread to allow non gui blocking sleeps
                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true; //Background threads will stop automatically on program close

                        //Fill placeholders in options and start programm (cmdkey sets the rdp credentials)
                        StartProcess.Start(RDPConnectionItem.pathToCMDKey, fillPlaceholders(buildAddingCmdkeyParameter()));

                        //Wait before RDP start
                        Thread.Sleep(TimeSpan.FromMilliseconds(500));

                        //Fill placeholders in options and start remote desktop with thread delay
                        StartProcess.Start(RDPConnectionItem.pathToRemoteDesktop, fillPlaceholders(buildRDPParameter()));

                        //Wait before credential remove
                        Thread.Sleep(TimeSpan.FromMilliseconds(5000));

                        //Fill placeholders in options and start programm with thread delay(cmdkey removes the previous set rdp credentials)
                        StartProcess.Start(RDPConnectionItem.pathToCMDKey, fillPlaceholders(buildRemovingCmdkeyParameter()));
                    }).Start();

                    return true;
                }
                else
                {
                    errorMessage = ("Application '" + RDPConnectionItem.pathToCMDKey + "' not found!");
                    return false;
                }
            }
            else
            {
                errorMessage = ("Application '" + RDPConnectionItem.pathToRemoteDesktop + "' not found!");
                return false;
            }

        }

        private String buildRDPParameter()
        {
            this.rdpParameter = "/v:" + this.keepassEntry.Strings.ReadSafe(this.plugin.settings.rdpConnectionAddressField);
            if (this.rdpConsoleSession) {
                this.rdpParameter = this.rdpParameter + " /admin /console";
            }
            this.rdpParameter = this.rdpParameter + " " + this.rdpCustomParameter;
            return this.rdpParameter;
        }


        //Creating cmdkey parameters with Keepass placeholders
        private String buildAddingCmdkeyParameter()
        {
            this.cmdkeyParameter = "/generic:TERMSRV/" + this.keepassEntry.Strings.ReadSafe(this.plugin.settings.rdpConnectionAddressField).Split(':')[0]
                + " /user:{USERNAME} /pass:{PASSWORD}";
            return this.cmdkeyParameter;
        }

        //Creating cmdkey parameters with Keepass placeholders
        private String buildRemovingCmdkeyParameter()
        {
            this.cmdkeyParameter = "/delete:TERMSRV/" + this.keepassEntry.Strings.ReadSafe(this.plugin.settings.rdpConnectionAddressField).Split(':')[0];
            return this.cmdkeyParameter;
        }


    }
}
