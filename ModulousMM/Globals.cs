using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModulousLib.Config;
using ModulousLib;
namespace ModulousMM
{
    class Globals
    {
        public static ConfigFile config_file{get;set;}
        public static List<OnlineMod> installed_mods { get; set; }
    }
}
