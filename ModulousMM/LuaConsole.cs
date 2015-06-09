using System;

namespace ModulousMM
{
    internal class LuaConsole
    {
        public static void Write(string message)
        {
            Console.WriteLine("LUA: ");
            Console.WriteLine(message);
        }
    }
}