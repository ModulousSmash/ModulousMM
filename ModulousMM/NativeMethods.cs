using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ModulousMM
{
    public class NativeMethods
    {
        public enum ProcessAccessFlags : uint
        {

            PROCESS_QUERY_LIMITED_INFORMATION = 0x1000

        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern bool QueryFullProcessImageName(IntPtr hprocess, int dwFlags,
                       StringBuilder lpExeName, out int size);
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess,
                       bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hHandle);
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool QueryFullProcessImageName(IntPtr hProcess, uint dwFlags,
            [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpExeName,
            ref uint lpdwSize);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}