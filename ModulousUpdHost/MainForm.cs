using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;
using ModulousLib;
using ModulousLib.Web;

namespace ModulousUpdHost
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VersionInfo version_info = VersionInfo.get_version_info_from_api();
            var web_client = new WebClient();
            label1.Text = "Updating...";
            if (Directory.Exists(Globals.temporary_path))
            {
                Directory.Delete(Globals.temporary_path,true);
                Directory.CreateDirectory(Globals.temporary_path);
            }
            else
            {
                Directory.CreateDirectory(Globals.temporary_path);
            }
            web_client.DownloadProgressChanged += (s, ex) => { progress_bar.Value = ex.ProgressPercentage; };

            web_client.DownloadFileCompleted += (s, ex) =>
            {
                ZipFile zip = ZipFile.Read(Path.Combine(Globals.temporary_path, "temp.zip"));
                zip.ExtractAll(Path.Combine(Globals.temporary_path, "update_temp/"));
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C xcopy /s " + "\"" + Path.Combine(Globals.temporary_path, "update_temp") + "\" \"" + Application.StartupPath + "\" /Y";
                MessageBox.Show(File.ReadAllText(Path.Combine(Globals.temporary_path, "update_temp/changelog.txt")));
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
                MessageBox.Show("The Manager has been updated to the latest version");
                Process.Start(Path.Combine(Application.StartupPath, "ModulousMM.exe"));
                Application.Exit();
            };
            web_client.DownloadFileAsync(new Uri(version_info.version_path), Path.Combine(Globals.temporary_path, "temp.zip"));
        }
    }
}