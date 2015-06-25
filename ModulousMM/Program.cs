using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CreativityKitchen.CreativityWin;
using ModulousMM.VistaShit;
using XDMessaging;
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
            TempFolder.clean_temp_folder();
            PreRunModQuery.is_mod_queried = false;

            Registry.RegisterURLProtocol("modulous", Application.ExecutablePath, "URL:Modulous");
            bool is_another_instance_running = false;
            Process[] running_processes = Process.GetProcessesByName("ModulousMM");
            foreach (Process process in running_processes)
            {
                if (!process.ProcessName.Contains("vshost") && process.Id != Process.GetCurrentProcess().Id)
                {
                    is_another_instance_running = true;
                }


            }
            foreach (string arg in args)
            {

                if (arg.Contains("modulous"))
                {
                    string[] arg_data = arg.Replace(@"modulous://", "").Split('/');
                    string command = arg_data[0];

                    switch (command)
                    {
                        case "install":
                            int id;
                            if (int.TryParse(arg_data[1], out id))
                            {
                                if (is_another_instance_running)
                                {

                                    XDMessagingClient client = new XDMessagingClient();
                                    IXDBroadcaster broadcaster =
                                        client.Broadcasters.GetBroadcasterForMode(XDTransportMode.HighPerformanceUI);
                                    broadcaster.SendToChannel("modulousmmintercoms", "install#" + arg_data[1].ToString());
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    PreRunModQuery.is_mod_queried = true;
                                    PreRunModQuery.queried_mod = id;
                                }
                            }
                            break;
                        default:
                            if (is_another_instance_running)
                            {
                                Environment.Exit(0);
                            }
                            break;
                    }
                }
                else
                {
                    if ( is_another_instance_running)
                    {
                        Environment.Exit(0);
                    }
                }
            }
            if (is_another_instance_running)
            {
                Environment.Exit(0);
            }
            Application.Run(new MainForm());
        }
    }
}