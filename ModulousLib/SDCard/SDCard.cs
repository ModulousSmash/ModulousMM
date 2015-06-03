using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModulousLib
{
    public class SDCard
    {
        public static string sd_card_path { get; set; }
        public static string sd_card_mod_store_path { get { return sd_card_path + "/Modulous/mod_store"; } set{} }
    }
}
