using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class frmConnect : Form
    {
        public frmConnect()
        {
            InitializeComponent();
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            this.Hide();
            Common.myIPAddress = this.txtIP.Text;
            Common.myPortNumber = int.Parse(this.txtPort.Text);


            Common.FrmClient.setupSC();

            INI.writeINI(Common.sfile, "Settings", "ip", Common.myIPAddress);
            INI.writeINI(Common.sfile, "Settings", "port", txtPort.Text);
        }

        private void frmConnect_Load(object sender, EventArgs e)
        {
            txtIP.Text = INI.getINI(Common.sfile, "Settings", "ip");
            txtPort.Text = INI.getINI(Common.sfile, "Settings", "port");
        }
    }
}
