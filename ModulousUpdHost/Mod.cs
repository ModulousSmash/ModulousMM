using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Ionic.Zip;
using ModulousLib;
using Newtonsoft.Json;

namespace ModulousLib
{
    /// <summary>
    ///     Website Mod metadata object.
    /// </summary>
    public class OnlineMod
    {
        public string tags { get; set; }
        public string website { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public int downloads { get; set; }
        public string license { get; set; }
        public int default_version_id { get; set; }
        public string name { get; set; }
        public string description_html { get; set; }
        public string donations { get; set; }
        public string url { get; set; }
        public string short_description { get; set; }
        public List<ModVersion> versions { get; set; }
        public int id { get; set; }
        public string source_code { get; set; }
        public string other_authors { get; set; }
        public int followers { get; set; }

        /// <summary>
        ///     Gets all the mod information from the Web API
        /// </summary>
        /// <param name="id">The id for the mod.</param>
        /// <returns></returns>
        public static OnlineMod get_mod_from_API(int id)
        {
            using (var client = new WebClient())
            {
                var s = client.DownloadString(new Uri(new Uri(Globals.site_url), "api/mod/" + id));
                return JsonConvert.DeserializeObject<OnlineMod>(s);
            }
        }
        /// <summary>
        ///     Reads the file into a Mod object from a file on the hard drive
        /// </summary>
        /// <param name="file">The file to read from</param>
        /// <returns></returns>
        public static OnlineMod get_mod_from_file(string file)
        {
            var file_contents = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<OnlineMod>(file_contents);
        }

        /// <summary>
        ///     Saves the mod data to a file
        /// </summary>
        /// <param name="target">File final location</param>
        public void save_to_file(string target)
        {
            File.WriteAllText(target, JsonConvert.SerializeObject(this));
        }

        public static void install_mod_from_site(int id)
        {
        }

        /// <summary>
        ///     Installs mod from a zip file
        /// </summary>
        /// <param name="file">Mod file</param>

        public LocalMod get_local_mod()
        {
            OnlineMod mod = this;
            ModConfig mod_config = new ModConfig();
            foreach (var directory in Directory.GetDirectories(SDCard.sd_card_mod_store_path))
            {
                if (File.Exists(Path.Combine(directory, "mod.json")))
                {
                    var config = ModConfig.FromFile(Path.Combine(directory, "mod.json"));
                    if (config.online_mod)
                    {
                        if (config.id == mod.id)
                        {
                            Console.WriteLine("wat");
                            mod_config = config;
                            return new LocalMod(mod, mod_config);


                        }
                    }
                }

            }
                return new LocalMod(mod);
        }
    }

    public class ModVersion
    {
        public int id { get; set; }
        public string friendly_version { get; set; }
        public string ksp_version { get; set; }
        public string changelog { get; set; }
        public string download_path { get; set; }
    }

    public class ModConfig
    {
        public int id;
        public string name { get; set; }
        public string author { get; set; }
        public string game { get; set; }
        public bool online_mod { get; set; }
        public bool requires_brawlex { get; set; }
        public string install_script { get; set; }
        public float version { get; set; }

        public static ModConfig FromFile(string file)
        {
            var file_contents = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<ModConfig>(file_contents);
        }
    }

    public class LocalMod
    {
        public OnlineMod online_mod { get; set; }
        public ModConfig installed_mod { get; set; }
        public bool is_online { get; set; }
        public bool is_installed { get; set; }

        public LocalMod(OnlineMod online, ModConfig installed)
        {
            online_mod = online;
            installed_mod = installed;
            is_online = true;
            is_installed = true;
        }

        public LocalMod(ModConfig installed)
        {
            installed_mod = installed;
            is_online = false;
            is_installed = true;
        }

        public LocalMod(OnlineMod online)
        {
            online_mod = online;
            is_installed = false;
            is_online = true;
        }
    }
}