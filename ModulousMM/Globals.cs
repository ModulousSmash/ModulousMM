using System.Collections.Generic;
using ModulousLib;
using ModulousLib.Config;

namespace ModulousMM
{
    internal class Globals
    {
        public static ConfigFile config_file { get; set; }
        public static List<OnlineMod> installed_mods { get; set; }
    }
}