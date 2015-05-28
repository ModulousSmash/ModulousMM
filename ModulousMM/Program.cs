using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CreativityKitchen;
using Microsoft.Win32;
using BrawlLib.IO;
using BrawlLib.SSBB;
using BrawlLib.SSBB.ResourceNodes;
namespace ModulousMM
{

    static class Program
    {
        public static CreativityConsole CConsole;
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        [DllImport("kernel32.dll",
            EntryPoint = "GetStdHandle",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);  
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            CreativityKitchen.CreativityWin.Registry.RegisterURLProtocol("modulous", Application.ExecutablePath, "URL:Modulous");

            Application.Run(new MainForm());
        }
    }
}
