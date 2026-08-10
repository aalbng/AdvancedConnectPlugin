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

            //Check for unresolved custom field references and warn the user (unless suppressed in settings)
            if (this.plugin == null || this.plugin.settings == null || !this.plugin.settings.suppressUnresolvedFieldWarning)
            {
                MatchCollection unresolvedMatches = unresolvedFieldPlaceholder.Matches(resolvedPathOrOptions);
                if (unresolvedMatches.Count > 0)
                {
                    List<String> fieldNames = new List<String>();
                    foreach (Match match in unresolvedMatches)
                    {
                        fieldNames.Add(match.Value);
                    }
                    showUnresolvedFieldWarning(fieldNames);
                }
            }

            //Drop custom field references that could not be resolved (missing or misspelled field)
            resolvedPathOrOptions = unresolvedFieldPlaceholder.Replace(resolvedPathOrOptions, String.Empty);

            //Resolv OS variables
            resolvedPathOrOptions = Environment.ExpandEnvironmentVariables(resolvedPathOrOptions);

            return resolvedPathOrOptions;
        }

        /**
         * Shows a warning about unresolved custom field references.
         * Must be called from background thread, so it marshals to the UI thread.
         */
        protected void showUnresolvedFieldWarning(List<String> unresolvedFields)
        {
            try
            {
                //Resolve the KeePass main window to marshal the message box onto the UI thread
                Form mainWindow = null;
                if (this.plugin != null && this.plugin.keepassHost != null)
                {
                    mainWindow = this.plugin.keepassHost.MainWindow;
                }

                //Build a short list of unresolved fields (max 5)
                String fieldList = String.Empty;
                int displayCount = Math.Min(unresolvedFields.Count, 5);
                for (int i = 0; i < displayCount; i++)
                {
                    fieldList += unresolvedFields[i];
                    if (i < displayCount - 1)
                    {
                        fieldList += ", ";
                    }
                }
                if (unresolvedFields.Count > 5)
                {
                    fieldList += "...";
                }

                MethodInvoker showMessage = delegate
                {
                    MessageBox.Show(
                        "The following custom field(s) could not be resolved and will be removed:"
                        + Environment.NewLine + Environment.NewLine
                        + fieldList
                        + Environment.NewLine + Environment.NewLine
                        + "Please check your custom field names.",
                        "Warning - Unresolved Fields",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
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
                //Showing a warning must never take KeePass down; deliberately ignored
            }
        }

        /**
         * Reports a failed application start to the user.
         * Connections are started on a background thread, where an unhandled exception
         * would terminate the whole KeePass process, so nothing may escape from here.
         */
        protected void showStartError(String applicationPath, Exception startException)
        {
            try
            {
                //Resolve the KeePass main window to marshal the message box onto the UI thread
                Form mainWindow = null;
                if (this.plugin != null && this.plugin.keepassHost != null)
                {
                    mainWindow = this.plugin.keepassHost.MainWindow;
                }

                MethodInvoker showMessage = delegate
                {
                    MessageBox.Show(("Application '" + applicationPath + "' could not be started."
                        + Environment.NewLine + Environment.NewLine + startException.Message),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //Reporting a failure must never take KeePass down; deliberately ignored
            }
        }

    }
}
