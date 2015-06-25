using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreativityKitchen.CreativityWin;
namespace RegisterKey
{
    class Program
    {
        static void Main(string[] args)
        {
            Registry.RegisterURLProtocol("modulous", System.Reflection.Assembly.GetExecutingAssembly().CodeBase, "URL:Modulous");
        }
    }
}
