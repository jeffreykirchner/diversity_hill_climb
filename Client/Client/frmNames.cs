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
    public partial class frmNames : Form
    {
        public frmNames()
        {
            InitializeComponent();
        }

        private void cmdSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "<Your Name Here>")
                    return;

                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                    return;

                string str = "";

                str = txtName.Text + ";";
                str += txtIDNumber.Text + ";";

                Common.FrmClient.SC.sendMessage("SUBJECT_NAME", str);

                cmdSubmit.Visible = false;
                txtName.Visible = false;
                txtIDNumber.Visible = false;

                lbl1.Visible = true;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void txtName_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (txtName.Text == "<Your Name Here>")
                {
                    txtName.Text = "";
                    txtName.ForeColor = Color.Black;
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void txtIDNumber_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (txtIDNumber.Text == "<Your Student ID Number>")
                {
                    txtIDNumber.Text = "";
                    txtIDNumber.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void frmNames_Load(object sender, EventArgs e)
        {
            try
            {
                cmdSubmit.Focus();
                AcceptButton = cmdSubmit;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
