using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Ionic.Zip;
using ModulousLib;
using ModulousMM;
using Newtonsoft.Json;
using NLua;

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
        public static void install_mod_from_file(string file, int id = 0)
        {
            Directory.CreateDirectory(Globals.temporary_path);
            if (!File.Exists(file))
            {
                throw (new Exception("Ya blew it, zip file is kill."));
            }
            using (var zip = ZipFile.Read(file))
            {
                var e = zip["mod.json"];
                e.Extract(Path.Combine(Globals.temporary_path));
                var config = ModConfig.FromFile(Path.Combine(Globals.temporary_path, "mod.json"));
                if (config.game.ToLower() != "brawl" && config.game.ToLower() != "projectm")
                {
                    throw (new Exception(
                        "Ya blew it, the game you are trying to install a mod for is not yet implemented"));
                }
                File.Delete(Path.Combine(Globals.temporary_path, "mod.json"));
                zip.ExtractAll(Globals.temporary_path);
                install_mod_from_metafile(config, id);
            }
        }

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
        public static void update_mod_from_file(string file, int id = 0)
        {
            Directory.CreateDirectory(Globals.temporary_path);
            if (!File.Exists(file))
            {
                throw (new Exception("Ya blew it, zip file is kill."));
            }
            using (var zip = ZipFile.Read(file))
            {
                var e = zip["mod.json"];
                e.Extract(Path.Combine(Globals.temporary_path));
                var config = ModConfig.FromFile(Path.Combine(Globals.temporary_path, "mod.json"));
                if (config.game.ToLower() != "brawl" && config.game.ToLower() != "projectm")
                {
                    throw (new Exception(
                        "Ya blew it, the game you are trying to install a mod for is not yet implemented"));
                }
                File.Delete(Path.Combine(Globals.temporary_path, "mod.json"));
                zip.ExtractAll(Globals.temporary_path);
                install_mod_from_metafile(config, id);
            }
            Directory.Delete(Globals.temporary_path, true);
        }
        public static void install_mod_from_metafile(ModConfig config, int id = 0)
        {
            Directory.CreateDirectory(Globals.temporary_path);
            /*
             * Runs the installation thread.
             */
            if (config.requires_brawlex == true)
            {
                if (!File.Exists(Path.Combine(SDCard.sd_card_path, "private/wii/app/RSBE/pf/BrawlEx/CSSRoster.dat")))
                {
                    TopMostMessageBox.Show("BrawlEX + CSS Extension is required for this mod to install.", "BrawlEX Error");

                    return;
                }
                if (!Directory.Exists(Path.Combine(SDCard.sd_card_path, "private/wii/app/RSBE/pf/BrawlEx/CosmeticConfig")));
                {
                    Directory.CreateDirectory(Path.Combine(SDCard.sd_card_path, "private/wii/app/RSBE/pf/BrawlEx/CosmeticConfig")))
                }
                if (!Directory.Exists(Path.Combine(SDCard.sd_card_path, "private/wii/app/RSBE/pf/BrawlEx/CSSSlotConfig")));
                {
                    Directory.CreateDirectory(Path.Combine(SDCard.sd_card_path, "private/wii/app/RSBE/pf/BrawlEx/CSSSlotConfig")))
                }
                if (!Directory.Exists(Path.Combine(SDCard.sd_card_path, "private/wii/app/RSBE/pf/BrawlEx/FighterConfig")));
                {
                    Directory.CreateDirectory(Path.Combine(SDCard.sd_card_path, "private/wii/app/RSBE/pf/BrawlEx/FighterConfig")))
                }
                if (!Directory.Exists(Path.Combine(SDCard.sd_card_path, "private/wii/app/RSBE/pf/BrawlEx/SlotConfig")));
                {
                    Directory.CreateDirectory(Path.Combine(SDCard.sd_card_path, "private/wii/app/RSBE/pf/BrawlEx/SlotConfig")))
                }
            }
            var state = new Lua();
            state.LoadCLRPackage();
            state.DoFile(Path.Combine(Globals.AssemblyDirectory, "data/lua/init.lua"));
            state.DoFile(Path.Combine(Globals.temporary_path, config.install_script));
            Directory.CreateDirectory(Path.Combine(new string[] {SDCard.sd_card_mod_store_path, config.name}));
            var json_contents = File.ReadAllText(Path.Combine(Globals.temporary_path, "mod.json"));
            var mod = JsonConvert.DeserializeObject<ModConfig>(json_contents);
            mod.id = id;
            if (id != 0)
            {
                mod.online_mod = true;
            }
            var content = JsonConvert.SerializeObject(mod, Formatting.Indented);
            File.WriteAllText(Path.Combine(Globals.temporary_path, "mod.json"), content);
            File.Copy(Path.Combine(Globals.temporary_path, "mod.json"),
                Path.Combine(new string[] {SDCard.sd_card_mod_store_path, config.name, "mod.json"}));
            File.Copy(Path.Combine(Globals.temporary_path, "init.lua"),
                Path.Combine(new string[] { SDCard.sd_card_mod_store_path, config.name, "init.lua" }));
        }
        public static void update_mod_from_metafile(ModConfig config, int id = 0)
        {
            Directory.CreateDirectory(Globals.temporary_path);
            /*
             * Runs the installation thread.
             */
            var state = new Lua();
            state.LoadCLRPackage();
            state.DoFile(Path.Combine(Globals.AssemblyDirectory, "data/lua/init.lua"));
            state.DoFile(Path.Combine(Globals.temporary_path, config.install_script));
            Directory.CreateDirectory(Path.Combine(new string[] { SDCard.sd_card_mod_store_path, config.name }));
            var json_contents = File.ReadAllText(Path.Combine(Globals.temporary_path, "mod.json"));
            var mod = JsonConvert.DeserializeObject<ModConfig>(json_contents);
            mod.id = id;
            var content = JsonConvert.SerializeObject(mod, Formatting.Indented);
            File.WriteAllText(Path.Combine(Globals.temporary_path, "mod.json"), content);
            File.Copy(Path.Combine(Globals.temporary_path, "mod.json"),
                Path.Combine(new string[] { SDCard.sd_card_mod_store_path, config.name, "mod.json" }));
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