using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace ModulousLib
{
    public class SDCard
    {
        public static string sd_card_path { get; set; }
        public static string sd_card_mod_store_path { get { return Path.Combine(sd_card_path,"Modulous/mod_store"); } set{} }
        /// <summary>
        /// Copies from the mod root to the SD Card, doesn't matter if it's a file or directory.
        /// </summary>
        /// <param name="origin">the origin file/directory</param>
        /// <param name="destination">the target path</param>
        public static void copy_from_mod_to_sd_card(string origin, string destination)
        {
            FileAttributes attr = File.GetAttributes(Path.Combine(Globals.temporary_path, origin));
            Console.WriteLine("Hey Lua");
            if (attr.HasFlag(FileAttributes.Directory))
            {
                new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory(Path.Combine(Globals.temporary_path, origin), Path.Combine(sd_card_path,destination));
            }

            else
            {
                File.Copy(Path.Combine(Globals.temporary_path, origin), Path.Combine(sd_card_path,destination, Path.GetFileName(origin)));
            }

        }
    }
}
