using System;
using System.IO;
using System.Reflection;

namespace ModulousLib
{
    public class Globals
    {
        public static string site_url
        {
            get { return "http://modulous.net/"; }
            set { }
        }

        public static string temporary_path
        {
            get { return Path.Combine(Path.GetTempPath(), "Modulous/"); }
            set { }
        }

        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}