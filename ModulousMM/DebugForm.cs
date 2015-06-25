using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CreativityKitchen;

namespace ModulousMM
{
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
        }

        private void update_mod_from_file_button_Click(object sender, EventArgs e)
        {

        }

        private void Alloc_console_button_Click(object sender, EventArgs e)
        {
            if (!Globals.console_attached)
            {
                NativeMethods.AllocConsole();
                Globals.console_attached = true;
                Globals.CConsole = new CreativityConsole();
                Console.SetOut(Globals.CConsole);
                Console.WriteLine("INFO#Modulous Manager ON " + Assembly.GetExecutingAssembly().GetName().Version);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModCreateForm mod_form = new ModCreateForm();
            mod_form.Show();
        }
    }
}
