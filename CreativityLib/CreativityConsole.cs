using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
/// <summary>
///  Classes made by creativity kitchen.
/// </summary>
namespace CreativityKitchen{

    /// <summary>
    ///  This class performs an important function.
    /// </summary>
    public class CreativityConsole : TextWriter
    {
        ///<summary>
        ///Extends the default console functions to have coloring, the three codes are WARN#, INFO# and ERR.
        ///</summary>>
        public override void WriteLine(string value)
        {
            base.WriteLine("Cock " + value);
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            sw.AutoFlush = true;
            Console.SetOut(sw);
            string[] data =  value.Split('#');
            Console.WriteLine(generateanswer(data));
            Console.ResetColor();
            Console.SetOut(new CreativityConsole());


        }
        private string generateanswer( string [] data)
        {
            switch (data[0])
            {
                case "WARN":
                    return "Warning: " + data[1];
                    break;
                case "ERR":
                    Console.ForegroundColor = ConsoleColor.Red;
                    return "Error: " + data[1];
                    break;
                case "INFO":
                    Console.ForegroundColor = ConsoleColor.Green;
                    return data[1];
                    break;
                default:
                    return data[0];
            }
        }
        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
