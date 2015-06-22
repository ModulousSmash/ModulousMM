using System.Collections.Generic;
using CreativityKitchen;
using ModulousLib;
using ModulousLib.Config;

namespace ModulousMM
{
    internal class Globals
    {
        public static ConfigFile config_file { get; set; }
        public static List<OnlineMod> installed_mods { get; set; }
        public static bool site_offline { get; set; }
        public static bool console_attached { get; set; }
        public static CreativityConsole CConsole { get; set; }
    }
}