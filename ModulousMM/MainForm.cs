using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLua;
using System.Runtime.InteropServices;
using CreativityKitchen;
using ModulousLib;
using System.IO;
using ModulousLib.Config;
using Newtonsoft.Json;
using MetroFramework.Forms;
using MetroFramework;
namespace ModulousMM
{
    public partial class MainForm : Form
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
        public MainForm()
        {
            
            InitializeComponent();
            AllocConsole();
            CConsole = new CreativityConsole();
            Console.SetOut(CConsole);
            Console.WriteLine("INFO#Modulous Manager ON " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            

        }

        private unsafe void Form1_Load(object sender, EventArgs e)
        {
            Lua state = new Lua();
            state.LoadCLRPackage();
            SDManager.set_sd_card(@"C:\Users\ALEX\Documents\ModulousTest\test");
            #region Configuration
            /*
             * Configuration Initialization 
            */
            Console.WriteLine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            string config_file_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Modulous/config.json");
            Console.WriteLine(config_file_path);
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Modulous/config.json")))
            {
                try
                {
                    Globals.config_file = new ConfigFile().FromFile(config_file_path);
                }
                catch(Exception ex)
                {
                    File.Delete(config_file_path);
                    Globals.config_file = new ConfigFile();
                    Console.WriteLine("ERR#" + ex.StackTrace);
                    Console.WriteLine("ERR#" + ex.Message);
                    File.WriteAllText(config_file_path, JsonConvert.SerializeObject(Globals.config_file, Formatting.Indented));
                }
            }
            else if (Directory.Exists(Path.GetDirectoryName(config_file_path)))
            {
                Globals.config_file = new ConfigFile();
                File.WriteAllText(config_file_path, JsonConvert.SerializeObject(Globals.config_file, Formatting.Indented));
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(config_file_path));
                Globals.config_file = new ConfigFile();
                File.WriteAllText(config_file_path, JsonConvert.SerializeObject(Globals.config_file, Formatting.Indented));
            }
            if(Globals.config_file.sd_card_location != null)
            {
                if (Directory.Exists(Globals.config_file.sd_card_location))
                {
                    SDManager.set_sd_card(Globals.config_file.sd_card_location);
                }
                else
                {
                    MessageBox.Show("Your SD card folder does not exist, or it's empty, you must choose a new one now.", "SDCard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FolderBrowserDialog folder_browse = new FolderBrowserDialog();
                    folder_browse.Description = "Select a new SD folder";
                selectsderr:
                    folder_browse.ShowDialog();
                    if (folder_browse.SelectedPath == null)
                    {
                        MessageBox.Show("You must select a new SD Card folder.", "SDCard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SDManager.set_sd_card(folder_browse.SelectedPath);
                        goto selectsderr;
                    }
                    Globals.config_file.sd_card_location = folder_browse.SelectedPath;
                    File.WriteAllText(config_file_path, JsonConvert.SerializeObject(Globals.config_file, Formatting.Indented));
                }
            }
            else
            {
                MessageBox.Show("You must select your SD Card or mod installation folder.", "Select your SDCard", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FolderBrowserDialog folder_browse = new FolderBrowserDialog();
                folder_browse.Description = "Select a new SD folder";
            selectedsd:
                folder_browse.ShowDialog();

                if (folder_browse.SelectedPath == null)
                {
                    MessageBox.Show("You must select a new SD Card folder.", "SDCard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SDManager.set_sd_card(folder_browse.SelectedPath);
                    goto selectedsd;
                }
                Globals.config_file.sd_card_location = folder_browse.SelectedPath;
                File.WriteAllText(config_file_path, JsonConvert.SerializeObject(Globals.config_file, Formatting.Indented));
            }
            File.WriteAllText(config_file_path, JsonConvert.SerializeObject(Globals.config_file, Formatting.Indented));
            #endregion
            Console.WriteLine("INFO#Found SDCARD at: " + Globals.config_file.sd_card_location);
            //state.DoFile(Application.StartupPath + "\\data\\lua\\init.lua");
            //state.DoFile(Application.StartupPath + "\\data\\lua\\startup.lua");
            //state.DoFile(@"E:\ModulousMMDemo\zip\install.lua");
            //Mod mod = Mod.get_mod_from_API("http://modulous.net/api/mod/15");

        }
        
    }
}
