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
    public partial class frmTestMode : Form
    {
        public frmTestMode()
        {
            InitializeComponent();
        }

        private void cmdTestMode_Click(object sender, EventArgs e)
        {
            try
            {

                if (Common.Frm1.timer2.Enabled)
                {
                    Common.Frm1.timer2.Enabled = false;
                    cmdTestMode.Text = "Return Control";
                }
                else
                {
                    Common.Frm1.timer2.Enabled = true;
                    cmdTestMode.Text = "Take Control";
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
