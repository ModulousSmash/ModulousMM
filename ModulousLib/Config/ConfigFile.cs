using System;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// Contains configuration file classes
/// </summary>

namespace ModulousLib.Config
{
    /// <summary>
    ///     Local configuration file
    /// </summary>
    public class ConfigFile
    {
        public string sd_card_location { get; set; }
        public float version { get; set; }
        public ConfigFile FromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var jsoncontent = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<ConfigFile>(jsoncontent);
            }
            throw (new SystemException("The configuration file does not exist"));
        }
    }
}