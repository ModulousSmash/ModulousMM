using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CreativityKitchen.CreativityWin
{
    public class Registry
    {
        /// <summary>
        /// Registers an user defined URL protocol for the usage with
        /// the Windows Shell, the Internet Explorer and Office.
        /// 
        /// Example for an URL of an user defined URL protocol:
        /// 
        /// modulous://test
        /// </summary>
        /// <param name="protocolName">Name of the protocol (e.g. "rainbird" für "rainbird://...")</param>
        /// <param name="applicationPath">Complete file system path to the EXE file, which processes the URL being called (the complete URL is handed over as a Command Line Parameter).</param>
        /// <param name="description">Description (e.g. "URL:Rainbird Custom URL")</param>
        public static void RegisterURLProtocol(string protocolName, string applicationPath, string description)
        {
            // Create new key for desired URL protocol
            Microsoft.Win32.RegistryKey myKey = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(protocolName);

            // Assign protocol
            myKey.SetValue(null, description);
            myKey.SetValue("URL Protocol", string.Empty);

            // Register Shell values
            Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(protocolName + "\\Shell");
            Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(protocolName + "\\Shell\\open");
            myKey = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(protocolName + "\\Shell\\open\\command");

            // Specify application handling the URL protocol
            myKey.SetValue(null, "\"" + applicationPath + "\" %1");
        }
    }
}
