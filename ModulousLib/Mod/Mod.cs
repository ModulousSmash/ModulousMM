using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using Ionic.Zip;
using System.IO;
using NLua;
namespace ModulousLib
{
    /// <summary>
    /// Website Mod metadata object.
    /// </summary>
    public class OnlineMod
    {
        public string tags                  { get; set; }
        public string website               { get; set; }
        public string author                { get; set; } 
        public string description           { get; set; }
        public int downloads                { get; set; }
        public string license               { get; set; }
        public int default_version_id       { get; set; }
        public string name                  { get; set; }
        public string description_html      { get; set; }
        public string donations             { get; set; }
        public string url                   { get; set; }
        public string short_description     { get; set; }
        public List<ModVersion> versions    { get; set; }
        public int id                       { get; set; }
        public string source_code           { get; set; }
        public string other_authors         { get; set; }
        public int followers                { get; set; }
        public OnlineMod()
        {
            
        }
        /// <summary>
        /// Gets all the mod information from the Web API
        /// </summary>
        /// <param name="URL">The API url for the mod.</param>
        /// <returns></returns>
        public static OnlineMod get_mod_from_API(int id)
        {
            using(WebClient client = new WebClient()) {
                string s = client.DownloadString(new Uri(new Uri(Globals.site_url), "api/mod/" + id) );
                return JsonConvert.DeserializeObject<OnlineMod>(s);
            }
        }
        public string get_mod_temp_location()
        {
            return "TODO";
        }
        /// <summary>
        /// Reads the file into a Mod object from a file on the hard drive
        /// </summary>
        /// <param name="file">The file to read from</param>
        /// <returns></returns>
        public static OnlineMod get_mod_from_file(string file)
        {
            string file_contents = System.IO.File.ReadAllText(file);
            return JsonConvert.DeserializeObject<OnlineMod>(file);
        }
        /// <summary>
        /// Saves the mod data to a file
        /// </summary>
        /// <param name="target">File final location</param>
        public void save_to_file(string target)
        {
            System.IO.File.WriteAllText(target, JsonConvert.SerializeObject(this));
        }
        /// <summary>
        /// Installs mod from a zip file
        /// </summary>
        /// <param name="file">Mod file</param>
        public static void install_mod_from_file(string file)
        {
            if (!File.Exists(file))
            {
                throw (new Exception("Ya blew it, zip file is kill."));
            }
            using(ZipFile zip = ZipFile.Read(file))
            {
                ZipEntry e = zip["mod.json"];
                e.Extract(Path.Combine(Path.GetTempPath(), "/mod.json"));
                ModConfig config = ModConfig.FromFile(Path.Combine(Path.GetTempPath(), "/mod.json"));
                if(config.game.ToLower() != "brawl" || config.game.ToLower() != "projectm")
                {
                    throw (new Exception("Ya blew it, the game you are trying to install a mod for is not yet implemented"));
                }
                zip.ExtractAll(Path.Combine(Path.GetTempPath() ,"/Modulous"));
                install_mod_from_metafile(config);
            }
        }
        public static void install_mod_from_metafile(ModConfig config)
        {
            Lua state = new Lua();
            /*
             * Runs the installation thread.
             */
            state.LoadCLRPackage();
            state.LoadFile(Path.Combine(Globals.temporary_path, config.install_script));
        }
    }
    public class ModVersion
    {
        public int id                     { get; set; }
        public string friendly_version    { get; set; }
        public string ksp_version         { get; set; }
        public string changelog           { get; set; }
        public string download_path       { get; set; }
    }
    public class ModConfig
    {
        public string name { get; set; }
        public string author { get; set; }
        public string game { get; set; }
        public bool mod_installable { get; set; }
        public string install_script { get; set; }

        public static ModConfig FromFile(string file)
        {
            string file_contents = System.IO.File.ReadAllText(file);
            return JsonConvert.DeserializeObject<ModConfig>(file);
        }
    }
    public class InstalledMods
    {
        public List<InstalledMod> installed_mods { get; set; }
        public InstalledMods FromFile(string file)
        {
            string file_contents = System.IO.File.ReadAllText(file);
            return JsonConvert.DeserializeObject<InstalledMods>(file);
        }
    }
    public class InstalledMod
    {
        public int id                   { get; set; }
        public bool is_online_mod       { get; set; }
        public void check_for_update(string name)
        {
            OnlineMod mod = OnlineMod.get_mod_from_API(id);
            string file = "asd";
            using (ZipFile zip = ZipFile.Read(file))
            {
                ZipEntry e = zip["mod.json"];
                e.Extract(Path.Combine(Path.GetTempPath(), "/mod.json"));
                ModConfig config = ModConfig.FromFile(Path.Combine(Path.GetTempPath(), "/mod.json"));
                if (config.game.ToLower() != "brawl" || config.game.ToLower() != "projectm")
                {
                    throw (new Exception("Ya blew it, the game you are trying to install a mod for is not yet implemented"));
                }
                zip.ExtractAll(Path.Combine(Path.GetTempPath(), "/Modulous"));
                Lua state = new Lua();
                /*
                 * Runs the installation thread.
                 */
                state.LoadCLRPackage();
                state.LoadFile(Path.Combine(Globals.temporary_path, config.install_script));
            }
        }
    }
}
