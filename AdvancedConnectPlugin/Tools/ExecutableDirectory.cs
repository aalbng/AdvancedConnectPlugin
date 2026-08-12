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
using System.IO;
using System.Reflection;
using System.Text;

namespace AdvancedConnectPlugin.Tools
{
    public static class ExecutableDirectory
    {
        //Returns the directory of the hosting executable (KeePass), also for UNC/network share paths.
        public static string GetExecutableDirectory()
        {
            //Prefer the host (entry) assembly, so a portable configuration can live next to KeePass.exe.
            //Fall back to this plugin assembly if the entry assembly is not available (for example when
            //the plugin was loaded in a context where GetEntryAssembly() returns null).
            Assembly assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

            //Location is a plain filesystem path (handles UNC "\\server\share\..." correctly and needs
            //no URI unescaping), so use it whenever it is available.
            string location = assembly.Location;
            if (!String.IsNullOrEmpty(location))
            {
                return Path.GetDirectoryName(location);
            }

            //Fallback: derive the path from CodeBase. Use Uri.LocalPath (not UriBuilder.Path), because
            //LocalPath preserves the UNC host/share and unescapes the path, whereas UriBuilder.Path
            //would drop the server name and mishandle special characters.
            Uri codeBaseUri = new Uri(assembly.CodeBase);
            return Path.GetDirectoryName(codeBaseUri.LocalPath);
        }
    }
}
