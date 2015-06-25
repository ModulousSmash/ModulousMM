using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ModulousMM
{
    class TempFolder
    {
        public static void clean_temp_folder()
        {
            if (Directory.Exists(ModulousLib.Globals.temporary_path))
            {
                Directory.Delete(ModulousLib.Globals.temporary_path, true);
            }

        }
    }
}
