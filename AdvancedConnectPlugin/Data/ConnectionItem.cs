/*
Copyright 2016 TGW Software Services GmbH

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is 
distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
See the License for the specific language governing permissions and limitations under the License.
*/
using KeePass.Util.Spr;
using KeePassLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AdvancedConnectPlugin.Data
{
    public class ConnectionItem
    {
        protected AdvancedConnectPluginExt plugin = null;
        protected PwDatabase keepassDatabase = null;
        protected PwEntry keepassEntry = null;

        //Matches keepass custom field references ({S:Name}) the compiling engine could not resolve.
        //Deliberately limited to that form, because command lines legitimately contain other braces
        //(for example "find . -exec rm {} \;" or json fragments), which must not be touched.
        private static readonly Regex unresolvedFieldPlaceholder =
            new Regex(@"\{S:[^}]*\}", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        //Collects the references removed while resolving one connection start. fillPlaceholders is
        //called several times per start (path, options, and three times for rdp), so the warning is
        //reported once for the whole start instead of once per call.
        private readonly List<String> removedFieldReferences = new List<String>();


        /**
         * Replaces all placeholders with keepass compiling engine and replace os environment variables.
         * Custom field references that stay unresolved are removed instead of being passed on literally.
         */
        protected String fillPlaceholders(String applicationOptions)
        {
            String resolvedPathOrOptions = String.Empty;

            //Resolv keepass variables
            Boolean encodeAsAutoType = false;
            Boolean encodeQuotesForCommandline = true;
            SprCompileFlags compileFlags = SprCompileFlags.All; // Which placeholders should be replaced
            SprContext replaceContext = new SprContext(this.keepassEntry, this.keepassDatabase, compileFlags, encodeAsAutoType, encodeQuotesForCommandline);
            resolvedPathOrOptions = SprEngine.Compile(applicationOptions, replaceContext);

            //Drop custom field references that could not be resolved (missing or misspelled field)
            //and remember them, so the user can be told about it once the start is prepared
            resolvedPathOrOptions = unresolvedFieldPlaceholder.Replace(resolvedPathOrOptions, delegate(Match removedReference)
            {
                if (!this.removedFieldReferences.Contains(removedReference.Value))
                {
                    this.removedFieldReferences.Add(removedReference.Value);
                }
                return String.Empty;
            });

            //Resolv OS variables
            resolvedPathOrOptions = Environment.ExpandEnvironmentVariables(resolvedPathOrOptions);

            return resolvedPathOrOptions;
        }

        /**
         * Tells the user which custom field references were removed, unless the warning is switched
         * off in the options. Called after all placeholders of one start have been resolved and
         * before the application is launched. Runs on a background thread, so the dialog is
         * marshalled onto the user interface thread and nothing is allowed to escape.
         */
        protected void showMissingFieldWarning()
        {
            try
            {
                if (this.removedFieldReferences.Count == 0) { return; }
                if (this.plugin == null || this.plugin.settings == null) { return; }
                if (!this.plugin.settings.warnOnMissingFieldReference) { this.removedFieldReferences.Clear(); return; }

                StringBuilder warning = new StringBuilder();
                warning.Append("The following custom fields do not exist on this entry and were removed from the command line:");
                warning.Append(Environment.NewLine);
                foreach (String removedReference in this.removedFieldReferences)
                {
                    warning.Append(Environment.NewLine);
                    warning.Append("    ");
                    warning.Append(removedReference);
                }
                warning.Append(Environment.NewLine);
                warning.Append(Environment.NewLine);
                warning.Append("You can switch this warning off in the AdvancedConnect options.");

                Form mainWindow = (this.plugin.keepassHost != null) ? this.plugin.keepassHost.MainWindow : null;
                MethodInvoker showMessage = delegate
                {
                    MessageBox.Show(warning.ToString(), "AdvancedConnect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                };

                if (mainWindow != null && mainWindow.IsHandleCreated && mainWindow.InvokeRequired)
                {
                    mainWindow.Invoke(showMessage);
                }
                else
                {
                    showMessage();
                }
            }
            catch (Exception)
            {
                //Warning the user must never take KeePass down; deliberately ignored
            }
            finally
            {
                this.removedFieldReferences.Clear();
            }
        }

    }
}
