using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ModulousLib
{
    class Globals
    {
        public static string site_url 
        { 
            get
            { 
                return "http://modulous.net/"; 
            } 
            set{}
        }
        public static string temporary_path
        {
            get
            {
                return Path.Combine(Path.GetTempPath(), "/Modulous/");
            }
            set { }
        }

    }
}