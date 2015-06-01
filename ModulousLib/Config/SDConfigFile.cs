using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModulousLib.Config
{
    public class SDConfigFile
    {
        public List<CharacterSlot> character_slots      { get; set; }
        public SDConfigFile()
        {
            character_slots = new List<CharacterSlot>();
        }
    }
    public class CharacterSlot{
        public int character_id                         { get; set; }
        public int mod_id                               { get; set; }
        public string character_name                    { get; set; }
    }
}
