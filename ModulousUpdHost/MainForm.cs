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
using Newtonsoft.Json;

namespace ModulousUpdHost
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            VersionInfo version_info = VersionInfo.get_version_info_from_api();
            var web_client = new WebClient();
            label1.Text = "Updating...";
            if (Directory.Exists(Globals.temporary_path))
            {
                DeleteDirectory(Globals.temporary_path);
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
                //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C xcopy /s " + "\"" + Path.Combine(Globals.temporary_path, "update_temp") + "\" \"" + Application.StartupPath + "\" /Y /C";

                process.StartInfo = startInfo;
                process.Start();

                process.WaitForExit();
                MessageBox.Show(File.ReadAllText(Path.Combine(Globals.temporary_path, "update_temp/changelog.txt")));
                MessageBox.Show("The Manager has been updated to the latest version");
                VersionInfo.get_version_info_from_api();
                File.WriteAllText( Path.Combine(Application.StartupPath, "version.json"),JsonConvert.SerializeObject(version_info));
                string run_path = Path.Combine(Application.StartupPath, "ModulousMM.exe");

                Process.Start(run_path);
                
                Application.Exit();
            };
            web_client.DownloadFileAsync(new Uri(version_info.version_path), Path.Combine(Globals.temporary_path, "temp.zip"));
        }
    }
}