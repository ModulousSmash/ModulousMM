using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;

/// <summary>
/// Contains configuration file classes
/// </summary>
namespace ModulousLib.Config
{
    /// <summary>
    /// Local configuration file
    /// </summary>
    public class ConfigFile
    {
        public string sd_card_location { get; set; }
        public ConfigFile FromFile(string file_path) 
        {
            if(File.Exists(file_path))
            {
                string jsoncontent = File.ReadAllText(file_path);
                return JsonConvert.DeserializeObject<ConfigFile>(jsoncontent);
            }
            else
            {
                throw (new SystemException("The configuration file does not exist"));
            }
        }
    }
}
