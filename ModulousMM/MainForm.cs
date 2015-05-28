using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLua;
using System.Runtime.InteropServices;
using CreativityKitchen;
using ModulousLib;
using BrawlLib.IO;
using BrawlLib.SSBB;
using BrawlLib.SSBB.ResourceNodes;
using System.IO;
namespace ModulousMM
{
    public partial class MainForm : Form
    {
        public static CreativityConsole CConsole;
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        [DllImport("kernel32.dll",
            EntryPoint = "GetStdHandle",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);  
        public MainForm()
        {

            InitializeComponent();
            AllocConsole();
            CConsole = new CreativityConsole();
            Console.SetOut(CConsole);
            Console.WriteLine("INFO#Modulous Manager ON " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            

        }

        private unsafe void Form1_Load(object sender, EventArgs e)
        {
            Lua state = new Lua();
            state.LoadCLRPackage();
            SDManager.set_sd_card(@"C:\Users\ALEX\Documents\ModulousTest\test");

            state.DoFile(Application.StartupPath + "\\data\\lua\\init.lua");
            state.DoFile(Application.StartupPath + "\\data\\lua\\startup.lua");
            state.DoFile(@"E:\ModulousMMDemo\zip\install.lua");
            Mod mod = Mod.get_mod_from_API("http://modulous.net/api/mod/15");

            Console.WriteLine(SDCard.sd_card_path);

        }
    }
}
