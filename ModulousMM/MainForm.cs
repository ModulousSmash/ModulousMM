using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using CreativityKitchen;
using ModulousLib;
using ModulousLib.Config;
using ModulousLib.Web;
using Newtonsoft.Json;
using NLua;
using XDMessaging;

namespace ModulousMM
{
    public partial class MainForm : Form
    {

        public bool Resizing;
        public bool Resizing2;
        public bool temp_folder_busy = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            * Console Initialization
            */
            Globals.console_attached = false;
#if DEBUG

            NativeMethods.AllocConsole();
            Globals.console_attached = true;
            Globals.CConsole = new CreativityConsole();
            Console.SetOut(Globals.CConsole);
            Console.WriteLine("INFO#Modulous Manager ON " + Assembly.GetExecutingAssembly().GetName().Version);
            BringToFront();
#endif

            #region Async Command Listening
            XDMessagingClient client = new XDMessagingClient();
            IXDListener listener = client.Listeners.GetListenerForMode(XDTransportMode.HighPerformanceUI);
            listener.RegisterChannel("modulousmmintercoms");
            listener.MessageReceived += (o, es) =>
            {
                if (es.DataGram.Channel == "modulousmmintercoms")
                {
                    string[] command = es.DataGram.Message.Split('#');

                    switch (command[0])
                    {

                        case "install":
                            int id;
                            if (int.TryParse(command[1], out id))
                            {
                                try
                                {
                                    OnlineMod mod = OnlineMod.get_mod_from_API(id);
                                    LocalMod local_mod = mod.get_local_mod();
                                    Console.WriteLine("GOTCHA!!!!");
                                    if (!local_mod.is_installed)
                                    {
                                        install_mod_ui(local_mod);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(
    "The mod you are trying to install does not exist, check the console for details.",
    "API error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Console.WriteLine("ERR#" + ex.Message);
                                    Console.WriteLine("ERR#" + ex.StackTrace);
                                }
                            }
                            break;
                    }
                }
            };
            #endregion
            //loads art
            TempFolder.clean_temp_folder();
            reload_art();
            //HACK:
            //we need to trigger a size change callback
            var mods_size = mods_list_view.Width;
            mods_list_view.Width = 10;
            mods_list_view.Width = mods_size;
            var status_size = status_list_view.Width;
            status_list_view.Width = 10;
            status_list_view.Width = status_size;
            /*
             * Lua initialization
             */
            var state = new Lua();
            state.LoadCLRPackage();

            #region Configuration

            /*
             * Configuration Initialization 
            */
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            var config_file_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Modulous/config.json");
            Console.WriteLine(config_file_path);
            if (
                File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "Modulous/config.json")))
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
                    File.WriteAllText(config_file_path,
                        JsonConvert.SerializeObject(Globals.config_file, Formatting.Indented));
                }
            }
            else if (Directory.Exists(Path.GetDirectoryName(config_file_path)))
            {
                Globals.config_file = new ConfigFile();
                File.WriteAllText(config_file_path,
                    JsonConvert.SerializeObject(Globals.config_file, Formatting.Indented));
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(config_file_path));
                Globals.config_file = new ConfigFile();
                File.WriteAllText(config_file_path,
                    JsonConvert.SerializeObject(Globals.config_file, Formatting.Indented));
            }
            if (Globals.config_file.sd_card_location != null)
            {
                if (Directory.Exists(Globals.config_file.sd_card_location))
                {
                    SDManager.set_sd_card(Globals.config_file.sd_card_location);
                }
                else
                {
                    MessageBox.Show(
                        "Your SD card folder does not exist, or it's empty, you must choose a new one now.",
                        "SDCard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    var folder_browse = new FolderBrowserDialog();
                    folder_browse.Description = "Select a new SD folder";
                    selectsderr:
                    folder_browse.ShowDialog();
                    if (folder_browse.SelectedPath == "")
                    {
                        MessageBox.Show("You must select a new SD Card folder.", "SDCard Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        goto selectsderr;
                    }
                    Globals.config_file.sd_card_location = folder_browse.SelectedPath;
                    SDManager.set_sd_card(folder_browse.SelectedPath);
                    File.WriteAllText(config_file_path,
                        JsonConvert.SerializeObject(Globals.config_file, Formatting.Indented));
                }
            }
            else
            {
                MessageBox.Show("You must select your SD Card or mod installation folder.", "Select your SDCard",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                var folder_browse = new FolderBrowserDialog();
                folder_browse.Description = "Select a new SD folder";
                selectedsd:
                folder_browse.ShowDialog();

                if (folder_browse.SelectedPath == null)
                {
                    MessageBox.Show("You must select a new SD Card folder.", "SDCard Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    SDManager.set_sd_card(folder_browse.SelectedPath);
                    goto selectedsd;
                }
                Globals.config_file.sd_card_location = folder_browse.SelectedPath;
                File.WriteAllText(config_file_path,
                    JsonConvert.SerializeObject(Globals.config_file, Formatting.Indented));
            }
            File.WriteAllText(config_file_path, JsonConvert.SerializeObject(Globals.config_file, Formatting.Indented));

            #endregion

            #region Safe Checks
            Console.WriteLine("INFO#Checking if " + Path.Combine(SDCard.sd_card_path, "private/wii/app/RSBE/pf") + " Exists");
             while(!Directory.Exists(Path.Combine(SDCard.sd_card_path, "private/wii/app/RSBE/pf")) &&
                !Directory.Exists(Path.Combine(SDCard.sd_card_path, "projectm")))
            {
                NativeMethods.SetForegroundWindow(this.Handle);
                DialogResult dialogResult =
TopMostMessageBox.Show("There doesn't seem to be a valid Gecko Brawl/Project M installation in your SD Card, click retry to retry or cancel to select a new SDCard folder", "",
    MessageBoxButtons.RetryCancel);
                if (dialogResult == DialogResult.Retry)
                {
                    continue;
                }
                else
                {
                    var folder_browse = new FolderBrowserDialog();
                    folder_browse.Description = "Select a new SD folder";
                selectsderr:
                    folder_browse.ShowDialog();
                    if (folder_browse.SelectedPath == "")
                    {
                        MessageBox.Show("You must select a new SD Card folder.", "SDCard Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        goto selectsderr;
                    }
                    Globals.config_file.sd_card_location = folder_browse.SelectedPath;
                    SDManager.set_sd_card(folder_browse.SelectedPath);
                    File.WriteAllText(config_file_path,
                        JsonConvert.SerializeObject(Globals.config_file, Formatting.Indented));
                }
            }
            #endregion

            #region Online Mod Gathering


            reload_mods();

            #endregion

            #region Upate Checking
            /*
            foreach (ListViewItem item in mods_list_view.Items)
            {
                LocalMod mod = (LocalMod)item.Tag;
                if (mod.is_installed && mod.is_online)
                {
                    ModVersion version = mod.online_mod.versions[0];
                    float version_number;
                    if (float.TryParse(version.friendly_version, out version_number))
                    {
                        if (version_number > mod.installed_mod.version)
                        {
                            temp_folder_busy = true;
                            Directory.CreateDirectory(ModulousLib.Globals.temporary_path);
                            if (mods_list_view.SelectedItems == null &&
                                mods_list_view.SelectedItems[0].SubItems[4].Text == "Not Installed")
                            {
                                return;
                            }
                            var online_mod = mod.online_mod;
                            var web_client = new WebClient();
                            var file_path = "";
                            var download_item = new ListViewItem(mod.installed_mod.name);
                            download_item.SubItems.Add("Downloading");
                            download_item.SubItems.Add("");
                            download_item = status_list_view.Items.Add(item);

                            web_client.DownloadProgressChanged +=
                                (s, ex) =>
                                {
                                    download_item.SubItems[2].Text = ex.ProgressPercentage.ToString() + "/100";
                                };

                            web_client.DownloadFileCompleted += (s, ex) =>
                            {
                                item.SubItems[1].Text = "Downloaded";
                                OnlineMod.update_mod_from_file(file_path, mod.installed_mod.id);
                                temp_folder_busy = false;
                            };
                            var file_url =
                                new Uri(new Uri(ModulousLib.Globals.site_url), mod.online_mod.versions[0].download_path).ToString();
                            var file_name = "";
                            web_client.DownloadData(file_url);
                            if (!String.IsNullOrEmpty(web_client.ResponseHeaders["Content-Disposition"]))
                            {
                                file_name =
                                    web_client.ResponseHeaders["Content-Disposition"].Substring(
                                        web_client.ResponseHeaders["Content-Disposition"].IndexOf("filename=") + 10)
                                        .Replace("\"", "");
                                file_path = Path.Combine(ModulousLib.Globals.temporary_path, file_name);
                            }
                            Console.WriteLine(file_path);
                            //web_client.DownloadFileAsync(), ModulousLib.Globals.temporary_path);            
                            web_client.DownloadFileAsync(new Uri(file_url), file_path);
                        }
                    }
                }
            }
            */
            #endregion


            //OnlineMod.install_mod_from_file(@"E:\Modulous\mod.zip");
        }

        private void mods_list_view_SizeChanged(object sender, EventArgs e)
        {
            // Don't allow overlapping of SizeChanged calls
            if (!Resizing)
            {
                // Set the resizing flag
                Resizing = true;

                var listView = sender as ListView;
                if (listView != null)
                {
                    float totalColumnWidth = 0;

                    // Get the sum of all column tags
                    for (var i = 0; i < listView.Columns.Count; i++)
                        totalColumnWidth += Convert.ToInt32(listView.Columns[i].Tag);

                    // Calculate the percentage of space each column should 
                    // occupy in reference to the other columns and then set the 
                    // width of the column to that percentage of the visible space.
                    for (var i = 0; i < listView.Columns.Count; i++)
                    {
                        var colPercentage = (Convert.ToInt32(listView.Columns[i].Tag)/totalColumnWidth);
                        listView.Columns[i].Width = (int) (colPercentage*listView.ClientRectangle.Width);
                    }
                }
            }

            // Clear the resizing flag
            Resizing = false;
        }

        private void reload_mods()
        {
            mods_list_view.Items.Clear();
            ModPage mod_page = new ModPage();
            try
            {
                mod_page = ModPage.browse_mods_from_api();
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
                MessageBox.Show(
                "Modulous seems to be down, or you are not connected to the internet",
                "Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Application.Exit();
                return;
            }
            foreach (var mod in mod_page.result)
            {
                ModConfig mod_config = new ModConfig();
                if (mod.versions[0].ksp_version == "Brawl" || mod.versions[0].ksp_version == "ProjectM" ||
                    mod.versions[0].ksp_version == "test" || mod.versions[0].ksp_version == "Project M")
                {
                    var mod_config_found = false;
                    var item = new ListViewItem(mod.name);
                    item.SubItems.Add(mod.author);
                    item.SubItems.Add(mod.versions[0].ksp_version);
                    if (!Directory.Exists(SDCard.sd_card_mod_store_path))
                    {
                        Directory.CreateDirectory(SDCard.sd_card_mod_store_path);
                    }
                    foreach (var directory in Directory.GetDirectories(SDCard.sd_card_mod_store_path))
                    {
                        if (File.Exists(Path.Combine(directory, "mod.json")))
                        {
                            var config = ModConfig.FromFile(Path.Combine(directory, "mod.json"));
                            if (config.online_mod)
                            {
                                if (config.id == mod.id)
                                {
                                    Console.WriteLine("wat");
                                    item.SubItems.Add(config.version.ToString());
                                    item.SubItems.Add(mod.versions[0].friendly_version);
                                    mod_config_found = true;
                                    item.Tag = new LocalMod(mod, config);
                                    mod_config = config;
                                    
                                }
                            }
                        }

                    }
                    if (!mod_config_found)
                    {
                        Console.WriteLine(mod.name);
                        item.Tag = new LocalMod(mod);
                        Console.WriteLine("INFO#Mod Config Not Found");
                        item.SubItems.Add("Not Installed");
                        item.SubItems.Add("Not Installed");
                    }
                    mods_list_view.Items.Add(item);

                }
            }
            foreach (var directory in Directory.GetDirectories(SDCard.sd_card_mod_store_path))
            {
                if (File.Exists(Path.Combine(directory, "mod.json")))
                {
                    var config = ModConfig.FromFile(Path.Combine(directory, "mod.json"));
                    if (!config.online_mod)
                    {
                        Console.WriteLine("INFO#Found Local Mod: " + config.name);
                        var item = new ListViewItem(config.name);
                        item.SubItems.Add(config.author);
                        item.SubItems.Add(config.game);
                        item.SubItems.Add(config.version.ToString());
                        item.SubItems.Add("Not a website mod");
                        item.Tag = new LocalMod(config);
                        mods_list_view.Items.Add(item);
                    }
                }
            }
        }

        private void reload_art()
        {
            // dolphin
            var dolphin_image = Image.FromFile(Path.Combine(Application.StartupPath, "data/images/dolphin.png"));
            dolphin_tool_strip_button.Image = dolphin_image;

            //settings
            var settings_image = Image.FromFile(Path.Combine(Application.StartupPath, "data/images/settings.png"));
            settings_strip_button.Image = settings_image;
            //install sd
            var download_button_image =
                Image.FromFile(Path.Combine(Application.StartupPath, "data/images/sdinstall.png"));
            download_button.BackgroundImage = download_button_image;

            //manual install icon
            var manual_install_image = Image.FromFile(Path.Combine(Application.StartupPath, "data/images/zip.png"));
            manual_install_button.BackgroundImage = manual_install_image;
            //manual install icon
            var lua_image = Image.FromFile(Path.Combine(Application.StartupPath, "data/images/lua.png"));
            run_lua_button.BackgroundImage = lua_image;
        }

        private void installPackageManuallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!temp_folder_busy)
            {
                temp_folder_busy = true;
                var dialog = new OpenFileDialog();
                dialog.Filter = "Zip File (*.zip)|*.zip";
                dialog.ShowDialog();
                if (dialog.FileName == null)
                {
                    return;
                }
                try
                {
                    //OnlineMod.install_mod_from_file(dialog.FileName);
                }
                catch (Exception es)
                {
                    Console.WriteLine("ERR#" + es.StackTrace + es.Message);
                }
                temp_folder_busy = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mod = (OnlineMod) mods_list_view.SelectedItems[0].Tag;
            Console.WriteLine(mod.name);
        }

        private void mods_list_view_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (mods_list_view.SelectedItems[0].Text != null)
            {
                Console.WriteLine();
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (PreRunModQuery.is_mod_queried)
            {
                install_mod_ui(OnlineMod.get_mod_from_API(PreRunModQuery.queried_mod).get_local_mod());
            }
        }

        private void manual_install_button_Click(object sender, EventArgs e)
        {
            if (!temp_folder_busy)
            {
                temp_folder_busy = true;
                var dialog = new OpenFileDialog();
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
                catch (Exception es)
                {
                    Console.WriteLine("ERR#" + es.StackTrace + es.Message);
                }
                temp_folder_busy = false;
            }
        }

        private void run_lua_button_Click(object sender, EventArgs e)
        {
            var file_dialog = new OpenFileDialog();
            file_dialog.Filter = "Lua File (*.lua)|*.lua";
            file_dialog.ShowDialog();
            if (file_dialog.FileName == null)
            {
                return;
            }
            var lua = new Lua();
            lua.LoadCLRPackage();

            lua.DoFile(Path.Combine(Application.StartupPath, "data/lua/init.lua"));
            lua.DoFile(file_dialog.FileName);
            Console.WriteLine("Running Lua...");
        }

        private void download_button_Click(object sender, EventArgs e)
        {
            install_mod_ui((LocalMod)mods_list_view.SelectedItems[0].Tag);
        }

        private void install_mod_ui( LocalMod local_mod)
        {
            if (!local_mod.is_online)
            {
                return;
            }
            if (local_mod.online_mod.get_local_mod().is_installed)
            {
                return;
            }
            else
            {
                
                NativeMethods.SetForegroundWindow(this.Handle);
                DialogResult dialogResult =
TopMostMessageBox.Show("Are you sure you want to install " + local_mod.online_mod.name + "?", "",
    MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            if (!temp_folder_busy)
            {

                temp_folder_busy = true;
                OnlineMod mod = local_mod.online_mod;
                var item = new ListViewItem(mod.name);
                item.SubItems.Add("Downloading...");
                item.SubItems.Add("");
                item = status_list_view.Items.Add(item);
                Directory.CreateDirectory(ModulousLib.Globals.temporary_path);
                if (mods_list_view.SelectedItems == null &&
                    mods_list_view.SelectedItems[0].SubItems[4].Text == "Not Installed")
                {
                    return;
                }

                var web_client = new WebClient();
                var file_path = "";


                web_client.DownloadProgressChanged +=
                    (s, ex) => { item.SubItems[2].Text = ex.ProgressPercentage.ToString() + "/100"; };

                web_client.DownloadFileCompleted += (s, ex) =>
                {
                    item.SubItems[1].Text = "Downloaded";
                    OnlineMod.install_mod_from_file(file_path, mod.id);
                    temp_folder_busy = false;
                    reload_mods();
                };
                var file_url = new Uri(new Uri(ModulousLib.Globals.site_url), mod.versions[0].download_path).ToString();
                var file_name = "";
                web_client.DownloadData(file_url);
                if (!String.IsNullOrEmpty(web_client.ResponseHeaders["Content-Disposition"]))
                {
                    file_name =
                        web_client.ResponseHeaders["Content-Disposition"].Substring(
                            web_client.ResponseHeaders["Content-Disposition"].IndexOf("filename=") + 10)
                            .Replace("\"", "");
                    file_path = Path.Combine(ModulousLib.Globals.temporary_path, file_name);
                }
                Console.WriteLine(file_path);
                //web_client.DownloadFileAsync(), ModulousLib.Globals.temporary_path);            
                web_client.DownloadFileAsync(new Uri(file_url), file_path);
            }
        }
        private void listView1_SizeChanged(object sender, EventArgs e)
        {
            // Don't allow overlapping of SizeChanged calls
            if (!Resizing2)
            {
                // Set the resizing flag
                Resizing2 = true;

                var listView = sender as ListView;
                if (listView != null)
                {
                    float totalColumnWidth = 0;

                    // Get the sum of all column tags
                    for (var i = 0; i < listView.Columns.Count; i++)
                        totalColumnWidth += Convert.ToInt32(listView.Columns[i].Tag);

                    // Calculate the percentage of space each column should 
                    // occupy in reference to the other columns and then set the 
                    // width of the column to that percentage of the visible space.
                    for (var i = 0; i < listView.Columns.Count; i++)
                    {
                        var colPercentage = (Convert.ToInt32(listView.Columns[i].Tag)/totalColumnWidth);
                        listView.Columns[i].Width = (int) (colPercentage*listView.ClientRectangle.Width);
                    }
                }
            }

            // Clear the resizing flag
            Resizing2 = false;
        }

        private void check_for_updates()
        {
            foreach (ListViewItem item in mods_list_view.Items)
            {
                OnlineMod mod = (OnlineMod) item.Tag;
            }
        }

        private KonamiSequence sequence = new KonamiSequence();

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (sequence.IsCompletedBy(e.KeyCode))
            {
                MessageBox.Show("KONAMI!!!");
            }
        }

        private void settings_strip_button_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.ShowDialog();
        }

        private void dolphin_tool_strip_button_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\ALEX\Documents\ladderdolphin\Dolphin-x64\Dolphin.exe");
        }
    }
}