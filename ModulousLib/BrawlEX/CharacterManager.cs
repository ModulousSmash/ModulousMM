using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrawlLib.IO;
using BrawlLib.SSBB;
using BrawlLib.SSBB.ResourceNodes;
using System.IO;
namespace ModulousLib.BrawlEX
{
    /// <summary>
    /// Administers character bullshit
    /// </summary>
    public class CharacterManager
    {
        /// <summary>
        /// Adds a character to the brawlex in the current SDCard
        /// </summary>
        /// <param name="module_file_path">The path of the module</param>
        /// <param name="cosmetic_file_path">The path to the cosmetics configuration file</param>
        /// <param name="slot_config_file_path">The path to the slot configuration file</param>
        /// <param name="fighter_file_path">The path to the BrawlEX fighter file path</param>
        /// <param name="css_slot_file_path"></param>
        /// <param name="fighter_data_folder">The path to the fighter data folder</param>
        /// <param name="character_name">the character internal name</param>
        /// <returns>wether or not the action was succesful</returns>
        public static unsafe bool add_character(string module_file_path, string cosmetic_file_path, string slot_config_file_path, string fighter_file_path, string css_slot_file_path ,string fighter_data_path, string character_name)
        {
            character_name = character_name.ToLower();
            // Stupiduser?
            if (!System.IO.File.Exists(module_file_path)) { throw new System.Exception("Module does not exist"); return false; }
            if (!System.IO.File.Exists(cosmetic_file_path)) { throw new System.Exception("Cosmetic file does not exist"); return false; }
            if (!System.IO.File.Exists(css_slot_file_path)) { throw new System.Exception("CSSlot does not exist"); return false; }
            if (!System.IO.File.Exists(slot_config_file_path)) { throw new System.Exception("Slot config file does not exist"); return false; }
            if (!System.IO.File.Exists(fighter_file_path)) { throw new System.Exception("Fighter file does not exist"); return false; }
            if (!System.IO.Directory.Exists(fighter_data_path)) { throw new System.Exception("Fighter data folder does not exist"); return false; }
            if (character_name.Trim() == "" || character_name == null) { throw new System.Exception("Name can't be empty."); return false; }
            /*
             * Gets the next available slot 
             * 
            */
            string brawlex_data_folder = SDCard.sd_card_path + "\\private\\wii\\app\\RSBE\\pf\\BrawlEx";
            string css_roster_file_location = brawlex_data_folder + "\\CSSRoster.dat";
            int character_id;
            int character_count;
            using (var stream = new FileStream(css_roster_file_location, FileMode.Open, FileAccess.ReadWrite))
            {
                //character count is in 0x0D
                stream.Position = 13;
                character_count = (int)stream.ReadByte() + 1;
                stream.Position = 13;
                stream.WriteByte((byte)character_count);
                //0x10 is where the character definitions begin
                int player_position = 0x0E + character_count;
                stream.Position = 0x0E + character_count;
                // This calculates the next available ID, you substract 24 (the normal character count) from the actual character count and
                // this gives you the ammount of new character already on brawlex, after that you add it to 3E, this is because
                // we have already changed the character count, so now brawlex has a minimum of 25, and thus for the formula
                // to work we need to start from 3F
                character_id = (character_count - 0x24) + 0x3E;
                stream.WriteByte((byte)character_id);
                stream.Position = player_position + 1;
                stream.WriteByte(0x29);
                stream.Close();
            }
            /*
             * Module bullshit 
             * 
            */
            System.IO.File.Copy(module_file_path, SDCard.sd_card_path + "\\private\\wii\\app\\RSBE\\pf\\Module\\" + Path.GetFileName(module_file_path));
            FileMap file = FileMap.FromFile(module_file_path);
            RELNode modules = new RELNode();
            modules.Initialize(null, file);
            ModuleSectionNode section = (ModuleSectionNode)modules.Children[8];
            byte* pointerToSectionData = (byte*)section._dataBuffer.Address;
            
            Console.WriteLine(Convert.ToUInt32(section.FileOffset, 16).ToString("X"));
            using (var stream = new FileStream(SDCard.sd_card_path + "\\private\\wii\\app\\RSBE\\pf\\Module\\" + Path.GetFileName(module_file_path), FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = Convert.ToUInt32(section.FileOffset, 16) + 3;
                stream.WriteByte((byte)character_id);
                stream.Close();
            }
            /*
             * Renames assets and copies them to the appropiate folder. 
             *
            */
            System.IO.Directory.CreateDirectory(brawlex_data_folder + "\\CosmeticConfig\\");
            System.IO.Directory.CreateDirectory(brawlex_data_folder + "\\FighterConfig\\");
            System.IO.Directory.CreateDirectory(brawlex_data_folder + "\\SlotConfig\\");
            System.IO.Directory.CreateDirectory(brawlex_data_folder + "\\CSSSlotConfig\\");
            System.IO.File.Copy(cosmetic_file_path, brawlex_data_folder + "\\CosmeticConfig\\Cosmetic" + character_id.ToString("X") + ".dat", true);
            System.IO.File.Copy(fighter_file_path, brawlex_data_folder + "\\FighterConfig\\Fighter" + character_id.ToString("X") + ".dat", true);
            System.IO.File.Copy(slot_config_file_path, brawlex_data_folder + "\\SlotConfig\\Slot" + character_id.ToString("X") + ".dat",true);
            System.IO.File.Copy(css_slot_file_path, brawlex_data_folder + "\\CSSSlotConfig\\CSSSlot" + character_id.ToString("X") + ".dat",true);
            string[] character_assets_directory_files = Directory.GetFiles(fighter_data_path);
            foreach(string s in character_assets_directory_files)
            {
                if (!System.IO.Directory.Exists(SDCard.sd_card_path + "\\private\\wii\\app\\RSBE\\pf\\fighter\\" + character_name + "\\"))
                {
                    System.IO.Directory.CreateDirectory(SDCard.sd_card_path + "\\private\\wii\\app\\RSBE\\pf\\fighter\\" + character_name + "\\");
                }
                
                 
                File.Copy(s, SDCard.sd_card_path + "\\private\\wii\\app\\RSBE\\pf\\fighter\\" + character_name + "\\" + System.IO.Path.GetFileName(s));
            }
            return true;
        }
    }
}