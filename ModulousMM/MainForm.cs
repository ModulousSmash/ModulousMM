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
using ModulousLib.Web;
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
            //we need to trigger a size change callback
            int mods_size = mods_list_view.Width;
            mods_list_view.Width = 10;
            mods_list_view.Width = mods_size;
            /*
             * Lua initialization
             */
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
                catch (Exception ex)
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
            if (Globals.config_file.sd_card_location != null)
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
            ModPage mod_page = ModPage.browse_mods_from_api();

            foreach (OnlineMod mod in mod_page.result)
            {
                    //Globals.installed_mods.Add(mod);
                if (mod.versions[0].ksp_version == "Brawl" || mod.versions[0].ksp_version == "ProjectM")
                {
                    ListViewItem item = new ListViewItem(mod.name);
                    item.SubItems.Add(mod.author);
                    item.SubItems.Add(mod.versions[0].ksp_version);
                    mods_list_view.Items.Add(item);
                }
            }
            Console.WriteLine("INFO#" + mod_page.result[0].name);
            //OnlineMod.install_mod_from_file(@"E:\Modulous\mod.zip");
        }
        public bool Resizing = false;
        private void mods_list_view_SizeChanged(object sender, EventArgs e)
        {
            // Don't allow overlapping of SizeChanged calls
            if (!Resizing)
            {
                // Set the resizing flag
                Resizing = true;

                ListView listView = sender as ListView;
                if (listView != null)
                {
                    float totalColumnWidth = 0;

                    // Get the sum of all column tags
                    for (int i = 0; i < listView.Columns.Count; i++)
                        totalColumnWidth += Convert.ToInt32(listView.Columns[i].Tag);

                    // Calculate the percentage of space each column should 
                    // occupy in reference to the other columns and then set the 
                    // width of the column to that percentage of the visible space.
                    for (int i = 0; i < listView.Columns.Count; i++)
                    {
                        float colPercentage = (Convert.ToInt32(listView.Columns[i].Tag) / totalColumnWidth);
                        listView.Columns[i].Width = (int)(colPercentage * listView.ClientRectangle.Width);
                    }
                }
            }

            // Clear the resizing flag
            Resizing = false;
        }

        private void installPackageManuallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Zip File (*.zip)|*.zip";
            dialog.ShowDialog();
            if (dialog.FileName == null)
            {
                return;
            }
            try
            {
                OnlineMod.install_mod_from_file(dialog.FileName);
            }
            catch(Exception es)
            {
                Console.WriteLine("ERROR#" + es.StackTrace + es.Message);
            }
        }


    }
}