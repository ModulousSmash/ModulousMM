using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ModulousMM.VistaShit
{
    class ProcessVista
    {
        public static string GetExecutablePath(Process Process)
        {
            //If running on Vista or later use the new function
            if (Environment.OSVersion.Version.Major >= 6)
            {
                return GetExecutablePathAboveVista(Process.Id);
            }

            return Process.MainModule.FileName;
        }
        public static string GetExecutablePathAboveVista(int ProcessId)
        {
            var buffer = new StringBuilder(1024);
            IntPtr hprocess = NativeMethods.OpenProcess(NativeMethods.ProcessAccessFlags.PROCESS_QUERY_LIMITED_INFORMATION,
                                          false, ProcessId);
            if (hprocess != IntPtr.Zero)
            {
                try
                {
                    int size = buffer.Capacity;
                    if (NativeMethods.QueryFullProcessImageName(hprocess, 0, buffer, out size))
                    {
                        return buffer.ToString();
                    }
                }
                finally
                {
                    NativeMethods.CloseHandle(hprocess);
                }
            }
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }
 
    }
}
