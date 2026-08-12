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
using System.Diagnostics;

namespace AdvancedConnectPlugin.Tools
{
    public static class StartProcess
    {
        //Starts a process and returns it. The caller owns the returned Process and is
        //responsible for disposing it (for example within a using block).
        public static Process Start(String path, String arguments)
        {
            Process process = new Process();
            try
            {
                process.StartInfo.FileName = path;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                return process;
            }
            catch
            {
                //Dispose the handle if the process could not be started, then rethrow
                process.Dispose();
                throw;
            }
        }

    }
}
