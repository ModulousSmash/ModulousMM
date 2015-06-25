using System;
using System.IO;
using System.Text;

/// <summary>
///  Classes made by creativity kitchen.
/// </summary>

namespace CreativityKitchen
{
    /// <summary>
    ///     This class performs an important function.
    /// </summary>
    public class CreativityConsole : TextWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }

        /// <summary>
        ///     Extends the default console functions to have coloring, the three codes are WARN#, INFO# and ERR.
        /// </summary>
        /// >
        public override void WriteLine(string value)
        {
            base.WriteLine("Cock " + value);
            var sw = new StreamWriter(Console.OpenStandardOutput());
            sw.AutoFlush = true;
            Console.SetOut(sw);
            var data = value.Split('#');
            Console.WriteLine(generateanswer(data));
            Console.ResetColor();
            Console.SetOut(new CreativityConsole());
        }

        private string generateanswer(string[] data)
        {
            switch (data[0])
            {
                case "WARN":
                    return "Warning: " + data[1];
                case "ERR":
                    Console.ForegroundColor = ConsoleColor.Red;
                    return "Error: " + data[1];
                case "INFO":
                    Console.ForegroundColor = ConsoleColor.Green;
                    return data[1];
                default:
                    return data[0];
            }
        }
    }
}