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
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Registry.RegisterURLProtocol("modulous", Application.ExecutablePath, "URL:Modulous");
            foreach (string arg in args)
            {
                if (arg.Contains("modulous"))
                {
                    string[] arg_data = arg.Replace(@"modulous://", "").Split('/');
                    string command = arg_data[0];
                }
            }

            Application.Run(new MainForm());
        }
    }
}