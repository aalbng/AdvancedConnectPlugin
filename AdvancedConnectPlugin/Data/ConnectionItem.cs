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

namespace AdvancedConnectPlugin.Data
{
    public class ConnectionItem
    {
        protected AdvancedConnectPluginExt plugin = null;
        protected PwDatabase keepassDatabase = null;
        protected PwEntry keepassEntry = null;


        /**
         * Replaces all placeholders with keepass compiling engine
         */
        protected String fillPlaceholders(String applicationOptions)
        {
            Boolean encodeAsAutoType = false;
            Boolean encodeQuotesForCommandline = true;
            SprCompileFlags compileFlags = SprCompileFlags.All; // Which placeholders should be replaced
            SprContext replaceContext = new SprContext(this.keepassEntry, this.keepassDatabase, compileFlags, encodeAsAutoType, encodeQuotesForCommandline);

            return SprEngine.Compile(applicationOptions, replaceContext);
        }

    }
}
