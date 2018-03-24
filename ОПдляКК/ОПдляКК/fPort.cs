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
    public partial class fPort : Form
    {
        public fPort()
        {
            InitializeComponent();
            InitializeComboBox();
         
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pCancel_Click(object sender, EventArgs e)
        {
            fPort.ActiveForm.Close();
        }

        private void fPortListPorts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void fPortSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void InitializeComboBox()
        {
            string[] employees = System.IO.Ports.SerialPort.GetPortNames(); // спиок портов
            fPortListPorts.Items.AddRange(employees);
            this.fPortListPorts.SelectedIndexChanged +=
                new System.EventHandler(fPortListPorts_SelectedIndexChanged);

            string[] speed = new string[]{"110", "300", "600", "1200",
                "2400", "4800", "9600", "14400", "19200", "38400", "56000", "57600", "115200", "128000", "256000" };
            fPortSpeed.Items.AddRange(speed);
            this.fPortSpeed.TabIndex = 0;
            this.fPortSpeed.SelectedIndex = 6;
            this.fPortSpeed.IntegralHeight = false;
            this.fPortSpeed.SelectedIndexChanged +=
                new System.EventHandler(fPortSpeed_SelectedIndexChanged);
        }


    }
}
