using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
namespace ModulousLib
{
    /// <summary>
    /// Website Mod metadata object.
    /// </summary>
    public class OnlineMod
    {
        public string tags                  { get; set; }
        public string website               { get; set; }
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
        public int bg_offset_y              { get; set; }
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
        public static OnlineMod get_mod_from_API(string URL)
        {
            using(WebClient client = new WebClient()) {
                string s = client.DownloadString(URL);
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
    }
    public class ModVersion
    {
        public int id                     { get; set; }
        public string friendly_version    { get; set; }
        public string ksp_version         { get; set; }
        public string changelog           { get; set; }
        public string download_path       { get; set; }
    }
}