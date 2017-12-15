using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ОПдляКК
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            System.IO.File.Create("set.ini");
            fPort f2 = new fPort();
            f2.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }
    }
}
