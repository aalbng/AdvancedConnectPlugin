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

namespace AdvancedConnectPlugin.Data
{
    [Serializable()]
    public class ApplicationItem
    {
        //Sentinel meaning "no KeePass icon selected" -> fall back to the executable's own icon.
        public const Int32 NoIcon = -1;

        public String name { get; set; }
        public String method { get; set; }
        public String path { get; set; }
        public String options { get; set; }

        //Optional KeePass standard icon id (index into PwIcon). NoIcon (-1) means "use the exe icon".
        public Int32 iconId { get; set; }

        //Optional self-contained custom icon, stored as Base64 encoded PNG data. This is intentionally
        //database-independent (copied out of the database when chosen), so it keeps working no matter
        //which or how many databases are open. Empty means "no custom icon selected".
        public String customIconPngBase64 { get; set; }

        public ApplicationItem()
        {
            this.iconId = NoIcon;
            this.customIconPngBase64 = String.Empty;
        }
        public ApplicationItem(String name, String method, String path, String options)
            : this()
        {
            this.name = name;
            this.method = method;
            this.path = path;
            this.options = options;
        }

    }
    
}
