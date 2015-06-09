using System;
using System.Windows.Forms;
using CreativityKitchen.CreativityWin;

namespace ModulousMM
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Registry.RegisterURLProtocol("modulous", Application.ExecutablePath, "URL:Modulous");


            Application.Run(new MainForm());
        }
    }
}