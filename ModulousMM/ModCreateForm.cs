using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;
using ModulousLib;
using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;

namespace ModulousMM
{
    public partial class ModCreateForm : Form
    {
        public string mod_root_folder = "";
        public string lua_script_location = "";
        public ModCreateForm()
        {
            InitializeComponent();
        }

        private void ModCreateForm_Load(object sender, EventArgs e)
        {
            game_combo_box.SelectedIndex = 0;
        }

        private void select_mod_root_button_Click(object sender, EventArgs e)
        {
            var folder_browse = new FolderBrowserDialog();
            folder_browse.Description = "Select your mod root folder";
            folder_browse.ShowDialog();
            if (folder_browse.SelectedPath == "")
            {
                //do nothing lol
            }
            else
            {
                if (!File.Exists(Path.Combine(folder_browse.SelectedPath, "init.lua")))
                {

                    mod_root_folder = folder_browse.SelectedPath;
                }
                else
                {
                    MessageBox.Show(
"The mod root must not contain a file named init.lua.",
"Illegal file found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (File.Exists(Path.Combine(folder_browse.SelectedPath, "mod.json")))
                {
                    MessageBox.Show(
                    "The mod root must not contain a file named mod.json.",
                    "Illegal file found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Zip File (*.zip)|*.zip";
            dialog.ShowDialog();
            if (dialog.FileName.Trim() == "")
            {
                //do nothing lol
                return;
            }
            if (name_text_box.Text.Trim() == "")
            {
                MessageBox.Show(
    "The mod name is empty or invalid.",
    "Mod Naming Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (author_name_text_box.Text.Trim() == "")
            {
                MessageBox.Show(
    "The author name is empty or invalid.",
    "Mod Naming Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (game_combo_box.Text.ToLower() != "brawl" && game_combo_box.Text.ToLower() != "projectm")
            {
                MessageBox.Show(
    "The game name is empty or invalid.",
    "Mod Naming Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (lua_script_location.Trim() == "")
            {
                MessageBox.Show(
    "You must select a valid Lua script.",
    "Lua Script Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (mod_root_folder.Trim() == "")
            {
                MessageBox.Show(
    "You must select a valid mod root folder.",
    "Mod root error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ModConfig config = new ModConfig();

            //id is 0 because it gets changed during install lol
            config.id = 0;
            config.name = name_text_box.Text.Trim();
            config.author = author_name_text_box.Text.Trim();
            config.game = game_combo_box.Text;
            config.online_mod = false;
            config.install_script = "init.lua";
            config.version = (float)numeric_version_select.Value;
            config.requires_brawlex = brawlex_checkbox.Checked;
            TempFolder.clean_temp_folder();
            Directory.CreateDirectory(ModulousLib.Globals.temporary_path);
            DirectoryCopyExt.DirectoryCopy(mod_root_folder, ModulousLib.Globals.temporary_path, true);
            string content = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText( Path.Combine(ModulousLib.Globals.temporary_path, "mod.json") , content);
            File.Copy(lua_script_location, Path.Combine(ModulousLib.Globals.temporary_path, "init.lua"));
            System.IO.Compression.ZipFile.CreateFromDirectory(ModulousLib.Globals.temporary_path, Path.Combine(ModulousLib.Globals.temporary_path, "temp.zip"));
            File.Copy(Path.Combine(ModulousLib.Globals.temporary_path, "temp.zip"), dialog.FileName, true);
        }

        private void select_lua_button_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Lua Script (*.lua)|*.lua";
            dialog.ShowDialog();
            if (dialog.FileName.Trim() == "")
            {
                //do nothing lol
                return;
            }
            else
            {
                lua_script_location = dialog.FileName.Trim();
            }
        }
    }
}
