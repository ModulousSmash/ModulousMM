using System.Collections.Generic;

namespace ModulousLib.Config
{
    public class SDConfigFile
    {
        public SDConfigFile()
        {
            character_slots = new List<CharacterSlot>();
        }

        public List<CharacterSlot> character_slots { get; set; }
    }

    public class CharacterSlot
    {
        public int character_id { get; set; }
        public int mod_id { get; set; }
        public string character_name { get; set; }
    }
}