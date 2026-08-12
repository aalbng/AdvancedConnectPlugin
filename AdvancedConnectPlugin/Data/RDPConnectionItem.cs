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
using System.IO;
using System.Threading;

namespace AdvancedConnectPlugin.Data
{
    public class RDPConnectionItem : ConnectionItem
    {
        public static String pathToRemoteDesktop = "%SystemRoot%\\System32\\mstsc.exe";

        //Maximum time (ms) to wait for mstsc to become ready before falling back to the safety buffer
        private const Int32 waitForReadyTimeout = 15000;

        private String rdpCustomParameter = String.Empty;


        public RDPConnectionItem(AdvancedConnectPluginExt plugin, PwEntry keepassEntry)
        {
            this.plugin = plugin;
            this.keepassDatabase = this.plugin.keepassHost.Database;
            this.keepassEntry = keepassEntry;
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

                //Snapshot the values the background thread needs on the calling (UI) thread, so the thread
                //works on an immutable copy and never reads/writes shared instance state without synchronization.
                //Placeholder resolution stays inside the thread to keep the exact previous timing/behaviour.
                String customParameter = this.rdpCustomParameter;

                //Create a thread to allow non gui blocking waits
                Thread connectionThread = new Thread(() =>
                {
                    try
                    {
                        //Store the rdp credentials securely through the Windows Credential Manager API
                        WindowsCredentialManager.Store(credentialTarget, userName, password);

                        //Start remote desktop with the already resolved address (avoids a second placeholder resolution)
                        using (System.Diagnostics.Process rdpProcess =
                            StartProcess.Start(Environment.ExpandEnvironmentVariables(RDPConnectionItem.pathToRemoteDesktop), buildRDPParameter(resolvedRdpAddress, customParameter)))
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
                });
                connectionThread.IsBackground = true; //Set before Start() so there is no foreground-thread window
                connectionThread.Name = "AdvancedConnect-RDP";
                connectionThread.Start();

                return true;
            }
            else
            {
                errorMessage = ("Application '" + RDPConnectionItem.pathToRemoteDesktop + "' not found!");
                return false;
            }

        }

        private String buildRDPParameter(String resolvedRdpAddress, String customParameter)
        {
            //Build from the passed-in snapshot only, so no shared instance state is written from the thread.
            //Custom parameter may still contain placeholders that need resolving.
            return "/v:" + resolvedRdpAddress + " " + fillPlaceholders(customParameter);
        }

    }
}
