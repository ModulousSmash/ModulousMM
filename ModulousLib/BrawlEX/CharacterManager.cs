﻿using System;
using System.IO;
using BrawlLib.IO;
using BrawlLib.SSBB.ResourceNodes;

namespace ModulousLib.BrawlEX
{
    /// <summary>
    ///     Administers character bullshit
    /// </summary>
    public class CharacterManager
    {
        /// <summary>
        ///     Adds a character to the brawlex in the current SDCard
        /// </summary>
        /// <param name="module_file_path">The path of the module</param>
        /// <param name="cosmetic_file_path">The path to the cosmetics configuration file</param>
        /// <param name="slot_config_file_path">The path to the slot configuration file</param>
        /// <param name="fighter_file_path">The path to the BrawlEX fighter file path</param>
        /// <param name="css_slot_file_path"></param>
        /// <param name="fighter_data_path">The path to the fighter data folder</param>
        /// <param name="character_name">the character internal name</param>
        /// <returns>wether or not the action was succesful</returns>
        private static unsafe bool add_character(string module_file_path, string cosmetic_file_path,
            string slot_config_file_path, string fighter_file_path, string css_slot_file_path, string fighter_data_path,
            string character_name)
        {
            character_name = character_name.ToLower();
            // Stupiduser?
            if (!File.Exists(module_file_path))
            {
                throw new Exception("Module does not exist");
            }
            if (!File.Exists(cosmetic_file_path))
            {
                throw new Exception("Cosmetic file does not exist");
            }
            if (!File.Exists(css_slot_file_path))
            {
                throw new Exception("CSSlot does not exist");
            }
            if (!File.Exists(slot_config_file_path))
            {
                throw new Exception("Slot config file does not exist");
            }
            if (!File.Exists(fighter_file_path))
            {
                throw new Exception("Fighter file does not exist");
            }
            if (!Directory.Exists(fighter_data_path))
            {
                throw new Exception("Fighter data folder does not exist");
            }
            if (character_name.Trim() == "" || character_name == null)
            {
                throw new Exception("Name can't be empty.");
            }
            /*
             * Gets the next available slot 
             * 
            */
            var brawlex_data_folder = Path.Combine(SDCard.sd_card_path + "/private/wii/app/RSBE/pf/BrawlEx/");
            var css_roster_file_location = Path.Combine(brawlex_data_folder + "CSSRoster.dat");
            int character_id;
            int character_count;
            using (var stream = new FileStream(css_roster_file_location, FileMode.Open, FileAccess.ReadWrite))
            {
                //character count is in 0x0D
                stream.Position = 13;
                character_count = stream.ReadByte() + 1;
                stream.Position = 13;
                stream.WriteByte((byte) character_count);
                //0x10 is where the character definitions begin
                var player_position = 0x0E + character_count;
                stream.Position = 0x0E + character_count;
                // This calculates the next available ID, you substract 24 (the normal character count) from the actual character count and
                // this gives you the ammount of new character already on brawlex, after that you add it to 3E, this is because
                // we have already changed the character count, so now brawlex has a minimum of 25, and thus for the formula
                // to work we need to start from 3F
                character_id = (character_count - 0x24) + 0x3E;
                stream.WriteByte((byte) character_id);
                stream.Position = player_position + 1;
                stream.WriteByte(0x29);
                stream.Close();
            }
            /*
             * Module bullshit 
             * 
            */

            var file = FileMap.FromFile(module_file_path);
            var module = new RELNode();
            module.Initialize(null, file);
            var character_id_section = (ModuleSectionNode) module.Children[8];
            var pointerToSectionData = (byte*) character_id_section._dataBuffer.Address;
            File.Copy(module_file_path,
                Path.Combine(SDCard.sd_card_path + "/private/wii/app/RSBE/pf/Module/", Path.GetFileName(module_file_path)), true);
            Console.WriteLine(Convert.ToUInt32(character_id_section.FileOffset, 16).ToString("X"));
            using (
                var stream =
                    new FileStream(
                        Path.Combine(SDCard.sd_card_path,
                            "private/wii/app/RSBE/pf/Module/" + Path.GetFileName(module_file_path)), FileMode.Open,
                        FileAccess.ReadWrite))
            {
                stream.Position = Convert.ToUInt32(character_id_section.FileOffset, 16) + 3;
                stream.WriteByte((byte) character_id);
                stream.Close();
            }
            /*
             * Renames assets and copies them to the appropiate folder. 
             *
            */
            Directory.CreateDirectory(brawlex_data_folder + "CosmeticConfig/");
            Directory.CreateDirectory(brawlex_data_folder + "FighterConfig/");
            Directory.CreateDirectory(brawlex_data_folder + "SlotConfig/");
            Directory.CreateDirectory(brawlex_data_folder + "CSSSlotConfig/");
            File.Copy(cosmetic_file_path,
                Path.Combine(brawlex_data_folder, "CosmeticConfig/Cosmetic" + character_id.ToString("X") + ".dat"), true);
            File.Copy(fighter_file_path,
                Path.Combine(brawlex_data_folder, "FighterConfig/Fighter" + character_id.ToString("X") + ".dat"), true);
            File.Copy(slot_config_file_path,
                Path.Combine(brawlex_data_folder, "SlotConfig/Slot" + character_id.ToString("X") + ".dat"), true);
            File.Copy(css_slot_file_path,
                Path.Combine(brawlex_data_folder, "CSSSlotConfig/CSSSlot" + character_id.ToString("X") + ".dat"), true);

            var character_assets_directory_files = Directory.GetFiles(fighter_data_path);
            foreach (var s in character_assets_directory_files)
            {
                if (!Directory.Exists(SDCard.sd_card_path + "private/wii/app/RSBE/pf/fighter/" + character_name + ""))
                {
                    Directory.CreateDirectory(Path.Combine(SDCard.sd_card_path ,"private/wii/app/RSBE/pf/fighter/",character_name));
                }


                File.Copy(s,Path.Combine(
                    SDCard.sd_card_path , "private/wii/app/RSBE/pf/fighter/" , character_name , "" , Path.GetFileName(s)), true);
            }
            return true;
        }
        /// <summary>
        ///     Adds a character to the brawlex install (if any) in the current SDCard, if not, prompts the user to install brawlex, works with PM DIY
        /// </summary>
        /// <param name="module_file_path">The path of the module</param>
        /// <param name="cosmetic_file_path">The path to the cosmetics configuration file</param>
        /// <param name="slot_config_file_path">The path to the slot configuration file</param>
        /// <param name="fighter_file_path">The path to the BrawlEX fighter file path</param>
        /// <param name="css_slot_file_path">The path to the slot file</param>
        /// <param name="fighter_data_path">The path to the fighter data folder</param>
        /// <param name="character_name">the character internal name</param>
        /// <returns>wether or not the action was succesful</returns>
        public static bool add_character_from_mod_root(string module_file_path, string cosmetic_file_path,
            string slot_config_file_path, string fighter_file_path, string css_slot_file_path, string fighter_data_path,
            string character_name)
        {
            return add_character(Path.Combine(Globals.temporary_path,module_file_path), Path.Combine(Globals.temporary_path,cosmetic_file_path), Path.Combine(Globals.temporary_path,slot_config_file_path), Path.Combine(Globals.temporary_path,fighter_file_path),
                Path.Combine(Globals.temporary_path,css_slot_file_path), Path.Combine(Globals.temporary_path,fighter_data_path), character_name);
        }
        private static unsafe bool add_character_update(string module_file_path, string cosmetic_file_path,
    string slot_config_file_path, string fighter_file_path, string css_slot_file_path, string fighter_data_path,
    string character_name)
        {
            character_name = character_name.ToLower();
            // Stupiduser?
            if (!File.Exists(module_file_path))
            {
                throw new Exception("Module does not exist");
            }
            if (!File.Exists(cosmetic_file_path))
            {
                throw new Exception("Cosmetic file does not exist");
            }
            if (!File.Exists(css_slot_file_path))
            {
                throw new Exception("CSSlot does not exist");
            }
            if (!File.Exists(slot_config_file_path))
            {
                throw new Exception("Slot config file does not exist");
            }
            if (!File.Exists(fighter_file_path))
            {
                throw new Exception("Fighter file does not exist");
            }
            if (!Directory.Exists(fighter_data_path))
            {
                throw new Exception("Fighter data folder does not exist");
            }
            if (character_name.Trim() == "" || character_name == null)
            {
                throw new Exception("Name can't be empty.");
            }
            var brawlex_data_folder = Path.Combine(SDCard.sd_card_path + "private/wii/app/RSBE/pf/BrawlEx");
            var css_roster_file_location = Path.Combine(brawlex_data_folder + "CSSRoster.dat");
            int character_id;
            int character_count;
            /*
             * Module bullshit 
             * 
            */
            File.Copy(module_file_path,
                Path.Combine(SDCard.sd_card_path + "private/wii/app/RSBE/pf/Module/", Path.GetFileName(module_file_path)), true);
            var file = FileMap.FromFile(module_file_path);
            var module = new RELNode();
            module.Initialize(null, file);
            var character_id_section = (ModuleSectionNode)module.Children[8];
            var pointerToSectionData = (byte*)character_id_section._dataBuffer.Address;

            Console.WriteLine(Convert.ToUInt32(character_id_section.FileOffset, 16).ToString("X"));
            using (
                var stream =
                    new FileStream(
                        Path.Combine(SDCard.sd_card_path,
                            "private/wii/app/RSBE/pf/Module/" + Path.GetFileName(module_file_path)), FileMode.Open,
                        FileAccess.Read))
            {
                stream.Position = Convert.ToUInt32(character_id_section.FileOffset, 16) + 3;
                character_id = stream.ReadByte();
                stream.Close();
            }
            /*
             * Renames assets and copies them to the appropiate folder. 
             *
            */
            Directory.CreateDirectory(brawlex_data_folder + "CosmeticConfig/");
            Directory.CreateDirectory(brawlex_data_folder + "FighterConfig/");
            Directory.CreateDirectory(brawlex_data_folder + "SlotConfig/");
            Directory.CreateDirectory(brawlex_data_folder + "CSSSlotConfig/");
            File.Copy(cosmetic_file_path,
                Path.Combine(brawlex_data_folder, "CosmeticConfig/Cosmetic" + character_id.ToString("X") + ".dat"), true);
            File.Copy(fighter_file_path,
                Path.Combine(brawlex_data_folder, "FighterConfig/Fighter" + character_id.ToString("X") + ".dat"), true);
            File.Copy(slot_config_file_path,
                Path.Combine(brawlex_data_folder, "SlotConfig/Slot" + character_id.ToString("X") + ".dat"), true);
            File.Copy(css_slot_file_path,
                Path.Combine(brawlex_data_folder, "CSSSlotConfig/CSSSlot" + character_id.ToString("X") + ".dat"), true);
            var character_assets_directory_files = Directory.GetFiles(fighter_data_path);
            foreach (var s in character_assets_directory_files)
            {
                if (!Directory.Exists(SDCard.sd_card_path + "private/wii/app/RSBE/pf/fighter/" + character_name + ""))
                {
                    Directory.CreateDirectory(Path.Combine(SDCard.sd_card_path, "private/wii/app/RSBE/pf/fighter/", character_name));
                }


                File.Copy(s,
                    SDCard.sd_card_path + "private/wii/app/RSBE/pf/fighter/" + character_name + "" + Path.GetFileName(s));
            }
            return true;
        }
    }
}