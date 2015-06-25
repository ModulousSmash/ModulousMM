using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;

namespace ModulousLib
{
    public class SDCard
    {
        public static string sd_card_path { get; set; }

        public static string sd_card_mod_store_path
        {
            get { return Path.Combine(sd_card_path, "Modulous/mod_store"); }
            set { }
        }

        /// <summary>
        ///     Copies from the mod root to the SD Card, doesn't matter if it's a file or directory.
        /// </summary>
        /// <param name="origin">the origin file/directory</param>
        /// <param name="destination">the target path</param>
        public static void copy_from_mod_to_sd_card(string origin, string destination)
        {
            var attr = File.GetAttributes(Path.Combine(Globals.temporary_path, origin));
            if (attr.HasFlag(FileAttributes.Directory))
            {
                //HACK: Fuck you .net
                DirectoryInfo directory_info = new DirectoryInfo(Path.Combine(Globals.temporary_path, origin));
                string directory_target = Path.Combine(sd_card_path, destination, directory_info.Name);
                new Computer().FileSystem.CopyDirectory(Path.Combine(Globals.temporary_path, origin), directory_target);
            }

            else
            {
                File.Copy(Path.Combine(Globals.temporary_path, origin),
                    Path.Combine(sd_card_path, destination, Path.GetFileName(origin)), true);
            }
        }
    }
}