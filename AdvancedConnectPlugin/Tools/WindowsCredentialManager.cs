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
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace AdvancedConnectPlugin.Tools
{
    /**
     * Stores and removes Windows credentials through the native Credential Management API
     * (advapi32.dll). This is a secure replacement for calling cmdkey.exe, because the
     * password never appears on a process command line where other processes could read it.
     *
     * Only used on the Windows RDP code path (mstsc.exe), so the Windows only P/Invoke does
     * not affect Mono/cross platform scenarios.
     */
    public static class WindowsCredentialManager
    {
        //Credential type matching cmdkey /generic
        private const UInt32 CRED_TYPE_GENERIC = 1;

        //Keep the credential only for the current logon session (removed automatically at logoff)
        private const UInt32 CRED_PERSIST_SESSION = 1;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct NativeCredential
        {
            public UInt32 Flags;
            public UInt32 Type;
            public IntPtr TargetName;
            public IntPtr Comment;
            public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
            public UInt32 CredentialBlobSize;
            public IntPtr CredentialBlob;
            public UInt32 Persist;
            public UInt32 AttributeCount;
            public IntPtr Attributes;
            public IntPtr TargetAlias;
            public IntPtr UserName;
        }

        [DllImport("advapi32.dll", EntryPoint = "CredWriteW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern Boolean CredWrite(ref NativeCredential credential, UInt32 flags);

        [DllImport("advapi32.dll", EntryPoint = "CredDeleteW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern Boolean CredDelete(String targetName, UInt32 type, UInt32 flags);

        /**
         * Stores a generic credential for the given target (e.g. "TERMSRV/server").
         * The password is passed directly to the OS and never touches a command line.
         */
        public static void Store(String targetName, String userName, String password)
        {
            if (String.IsNullOrEmpty(targetName))
            {
                throw new ArgumentException("Target name must not be empty.", "targetName");
            }

            //The credential blob is the password encoded as Unicode bytes
            byte[] passwordBytes = Encoding.Unicode.GetBytes(password ?? String.Empty);

            IntPtr targetNamePtr = IntPtr.Zero;
            IntPtr userNamePtr = IntPtr.Zero;
            IntPtr credentialBlobPtr = IntPtr.Zero;
            try
            {
                targetNamePtr = Marshal.StringToCoTaskMemUni(targetName);
                userNamePtr = Marshal.StringToCoTaskMemUni(userName ?? String.Empty);
                credentialBlobPtr = Marshal.AllocCoTaskMem(passwordBytes.Length);
                Marshal.Copy(passwordBytes, 0, credentialBlobPtr, passwordBytes.Length);

                NativeCredential credential = new NativeCredential();
                credential.Type = CRED_TYPE_GENERIC;
                credential.TargetName = targetNamePtr;
                credential.CredentialBlobSize = (UInt32)passwordBytes.Length;
                credential.CredentialBlob = credentialBlobPtr;
                credential.Persist = CRED_PERSIST_SESSION;
                credential.UserName = userNamePtr;

                if (!CredWrite(ref credential, 0))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            finally
            {
                //Zero out the password copy before releasing the unmanaged memory
                if (credentialBlobPtr != IntPtr.Zero)
                {
                    for (int i = 0; i < passwordBytes.Length; i++)
                    {
                        Marshal.WriteByte(credentialBlobPtr, i, 0);
                    }
                    Marshal.FreeCoTaskMem(credentialBlobPtr);
                }
                if (targetNamePtr != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(targetNamePtr);
                }
                if (userNamePtr != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(userNamePtr);
                }
                Array.Clear(passwordBytes, 0, passwordBytes.Length);
            }
        }

        /**
         * Removes a previously stored generic credential.
         * A missing credential (already removed) is not treated as an error.
         */
        public static void Remove(String targetName)
        {
            if (String.IsNullOrEmpty(targetName))
            {
                return;
            }

            if (!CredDelete(targetName, CRED_TYPE_GENERIC, 0))
            {
                const int ERROR_NOT_FOUND = 1168;
                int lastError = Marshal.GetLastWin32Error();
                if (lastError != ERROR_NOT_FOUND)
                {
                    throw new Win32Exception(lastError);
                }
            }
        }
    }
}
