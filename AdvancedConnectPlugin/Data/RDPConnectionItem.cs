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

        //Maximum time (ms) to wait for mstsc to become ready before falling back to the safety buffer
        private const Int32 waitForReadyTimeout = 15000;

        private Boolean rdpConsoleSession = false;
        private String rdpCustomParameter = String.Empty;
        private String rdpParameter = String.Empty;


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
                //Overwrite the default rdp parameter if set in keepass entry
                if (this.keepassEntry.Strings.ReadSafe(this.plugin.settings.connectionOptionsField).Length > 0)
                {
                    this.rdpCustomParameter = this.keepassEntry.Strings.ReadSafe(this.plugin.settings.connectionOptionsField);
                }

                //Resolve the credential target host, using the same placeholder resolution mstsc receives
                //(cmdkey/mstsc use the host without a port)
                //
                //Known limitation: the credential is keyed only by host (TERMSRV/<host>). Starting two
                //connections to the SAME host almost simultaneously makes them share this credential entry,
                //so the first cleanup may remove it before the second mstsc instance has read it. This
                //matches the behaviour of the original cmdkey approach and is bounded by CRED_PERSIST_SESSION
                //(the credential is removed automatically at logoff). Synchronising this was deemed
                //disproportionate for the expected usage (interactive, one connection at a time).
                String resolvedRdpAddress = fillPlaceholders(this.keepassEntry.Strings.ReadSafe(this.plugin.settings.rdpConnectionAddressField));
                String credentialTarget = "TERMSRV/" + resolvedRdpAddress.Split(':')[0];

                //Resolve username and password directly, so they are never placed on a command line
                String userName = resolveField("{USERNAME}");
                String password = resolveField("{PASSWORD}");

                //Buffer (ms) to keep the credential available after mstsc signals it is ready.
                //Guard against invalid (negative) configuration values by falling back to a sane default.
                Int32 credentialCleanupDelay = this.plugin.settings.rdpCredentialCleanupDelay;
                if (credentialCleanupDelay < 0)
                {
                    credentialCleanupDelay = 2000;
                }

                //Create a thread to allow non gui blocking waits
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true; //Background threads will stop automatically on program close

                    try
                    {
                        //Store the rdp credentials securely through the Windows Credential Manager API
                        WindowsCredentialManager.Store(credentialTarget, userName, password);

                        //Start remote desktop with the already resolved address (avoids a second placeholder resolution)
                        using (System.Diagnostics.Process rdpProcess =
                            StartProcess.Start(RDPConnectionItem.pathToRemoteDesktop, buildRDPParameter(resolvedRdpAddress)))
                        {
                            try
                            {
                                //Wait (generously) until mstsc has built up its message loop, i.e. is ready to
                                //read the credentials, instead of guessing with a fixed delay.
                                rdpProcess.WaitForInputIdle(RDPConnectionItem.waitForReadyTimeout);
                            }
                            catch (InvalidOperationException)
                            {
                                //WaitForInputIdle is only valid for GUI processes; ignore if not applicable
                            }

                            //Configurable safety buffer to make sure the credential was read before removing it
                            if (credentialCleanupDelay > 0)
                            {
                                Thread.Sleep(credentialCleanupDelay);
                            }
                        }
                    }
                    catch (Exception startException)
                    {
                        //An unhandled exception on this thread would terminate KeePass
                        showStartError(RDPConnectionItem.pathToRemoteDesktop, startException);
                    }
                    finally
                    {
                        //Always remove the temporary credential, even if the start failed.
                        //See the "Known limitation" note above regarding concurrent connections to the same host.
                        try { WindowsCredentialManager.Remove(credentialTarget); }
                        catch (Exception) { }
                    }
                }).Start();

                return true;
            }
            else
            {
                errorMessage = ("Application '" + RDPConnectionItem.pathToRemoteDesktop + "' not found!");
                return false;
            }

        }

        private String buildRDPParameter(String resolvedRdpAddress)
        {
            this.rdpParameter = "/v:" + resolvedRdpAddress;
            if (this.rdpConsoleSession) {
                this.rdpParameter = this.rdpParameter + " /admin /console";
            }
            //Custom parameter may still contain placeholders that need resolving
            this.rdpParameter = this.rdpParameter + " " + fillPlaceholders(this.rdpCustomParameter);
            return this.rdpParameter;
        }

    }
}
