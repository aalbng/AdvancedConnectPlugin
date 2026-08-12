/*
Copyright 2026 Andreas Albang

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is 
distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
See the License for the specific language governing permissions and limitations under the License.
*/
using AdvancedConnectPlugin.Data;
using KeePass.Plugins;
using KeePassLib;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AdvancedConnectPlugin.Tools
{
    /**
     * Resolves the KeePass icon (standard icon or a self-contained custom icon) selected for a
     * configured application into an Image that can be shown in the context menu.
     *
     * Custom icons are stored database-independently as Base64 encoded PNG data on the application
     * itself, so they keep working regardless of which database (or how many) is open. Returns null
     * when no KeePass icon is selected, so the caller can fall back to the executable's own icon.
     */
    public static class ApplicationIcon
    {
        //Number of KeePass standard icons; the standard icons live at indices [0..standardIconCount).
        private static readonly int standardIconCount = (int)PwIcon.Count;

        //Decodes the stored Base64 PNG data of a custom icon into an Image, or null if none/invalid.
        public static Image DecodeCustomIcon(String customIconPngBase64)
        {
            if (String.IsNullOrEmpty(customIconPngBase64))
            {
                return null;
            }

            try
            {
                byte[] pngBytes = Convert.FromBase64String(customIconPngBase64);
                if (pngBytes == null || pngBytes.Length == 0)
                {
                    return null;
                }

                //Image.FromStream keeps a reference to the stream, so decode into a standalone Bitmap
                //copy and dispose the stream afterwards to avoid a lingering stream dependency.
                using (MemoryStream pngStream = new MemoryStream(pngBytes))
                {
                    using (Image decoded = Image.FromStream(pngStream))
                    {
                        return new Bitmap(decoded);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Encodes raw PNG icon data into the Base64 string stored on the application (empty if none).
        public static String EncodeCustomIcon(byte[] pngBytes)
        {
            if (pngBytes == null || pngBytes.Length == 0)
            {
                return String.Empty;
            }
            return Convert.ToBase64String(pngBytes);
        }

        //Looks up the raw PNG data of a custom database icon by its UUID (null if not found).
        public static byte[] GetCustomIconPngData(PwDatabase database, PwUuid customIconUuid)
        {
            if (database == null || customIconUuid == null || customIconUuid.Equals(PwUuid.Zero))
            {
                return null;
            }

            foreach (PwCustomIcon customIcon in database.CustomIcons)
            {
                if (customIcon.Uuid.Equals(customIconUuid))
                {
                    return customIcon.ImageDataPng;
                }
            }
            return null;
        }

        /**
         * Returns the Image for the icon selected on the given application, or null when no
         * KeePass icon was selected (iconId == NoIcon and no custom icon data).
         */
        public static Image Resolve(IPluginHost keepassHost, ApplicationItem application)
        {
            if (application == null)
            {
                return null;
            }

            //Prefer the self-contained custom icon (database-independent, stored as Base64 PNG).
            Image customIcon = DecodeCustomIcon(application.customIconPngBase64);
            if (customIcon != null)
            {
                return customIcon;
            }

            //Otherwise use the selected standard icon from KeePass' client icon list, if any.
            if (application.iconId != ApplicationItem.NoIcon
                && application.iconId >= 0
                && application.iconId < standardIconCount
                && keepassHost != null)
            {
                ImageList clientIcons = keepassHost.MainWindow.ClientIcons;
                if (clientIcons != null && application.iconId < clientIcons.Images.Count)
                {
                    return clientIcons.Images[application.iconId];
                }
            }

            //No KeePass icon selected -> let the caller fall back to the executable icon.
            return null;
        }
    }
}
