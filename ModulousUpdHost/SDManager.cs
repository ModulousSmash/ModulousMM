using System.IO;

namespace ModulousLib
{
    public class SDManager
    {
        /// <summary>
        ///     Adds the SD card root
        /// </summary>
        /// <param name="sdcard_root">The location of the SD card</param>
        public static void set_sd_card(string sdcard_root)
        {
            if (Directory.Exists(sdcard_root))
            {
                SDCard.sd_card_path = sdcard_root;
            }
        }
    }
}