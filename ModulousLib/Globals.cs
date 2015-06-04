using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
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
                return Path.Combine(Path.GetTempPath(), "Modulous/");
            }
            set { }
        }
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

    }
}