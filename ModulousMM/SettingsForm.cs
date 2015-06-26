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
using ModulousLib;
using Newtonsoft.Json;

namespace ModulousMM
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }
        private KonamiSequence sequence = new KonamiSequence();

        private void SettingsForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (sequence.IsCompletedBy(e.KeyCode))
            {
                DebugForm form = new DebugForm();
                form.Show();
            }
        }

        private void sd_folder_browse_button_Click(object sender, EventArgs e)
        {
            var config_file_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
    "Modulous/config.json");
            var folder_browse = new FolderBrowserDialog();
            folder_browse.Description = "Select a new SD folder";
            folder_browse.ShowDialog();
            if (folder_browse.SelectedPath == "")
            {
                //do nothing lol
            }
            else
            {
                SDManager.set_sd_card(folder_browse.SelectedPath);
                Globals.config_file.sd_card_location = folder_browse.SelectedPath;
                reload_settings();
                File.WriteAllText(config_file_path,
                    JsonConvert.SerializeObject(Globals.config_file, Formatting.Indented));
                if (!Directory.Exists(SDCard.sd_card_mod_store_path))
                {
                    Directory.CreateDirectory(SDCard.sd_card_mod_store_path);
                }
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            reload_settings();
        }

        private void reload_settings()
        {
            sd_folder_browse_textbox.Text = Globals.config_file.sd_card_location;
            dolphin_location_text_box.Text = Globals.config_file.dolphin_location;
        }

        private void open_mod_creator_Click(object sender, EventArgs e)
        {
            ModCreateForm mod_form = new ModCreateForm();
            mod_form.Show();
        }

        private void browse_dolphin_path_button_Click(object sender, EventArgs e)
        {
            var config_file_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
"Modulous/config.json");
            var file_browse = new OpenFileDialog();
            file_browse.Filter = "Portable Executable File (*.exe)|*.exe";
            file_browse.ShowDialog();
            if (file_browse.FileName != null)
            {
                //do nothing lol
            }
            else
            {
                Globals.config_file.dolphin_location = file_browse.FileName;
                File.WriteAllText(config_file_path,
                    JsonConvert.SerializeObject(Globals.config_file, Formatting.Indented));
            }
        }

        private void enable_debug_features_Click(object sender, EventArgs e)
        {
            if (!Globals.console_attached)
            {
                NativeMethods.AllocConsole();
                Globals.console_attached = true;
            }
        }
    }
}
