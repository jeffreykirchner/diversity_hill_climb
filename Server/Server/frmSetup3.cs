using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class frmSetup3 : Form
    {

        int levels;
        public frmSetup3()
        {
            InitializeComponent();
        }

        private void frmSetup3_Load(object sender, EventArgs e)
        {
            try
            {
                levels = int.Parse(INI.getINI(Common.sfile, "valueDistribution", "levels"));
                load();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                save();   
                this.Close();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdPlus_Click(object sender, EventArgs e)
        {
            try
            {
                levels++;
                load();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdMinus_Click(object sender, EventArgs e)
        {
            try
            {
                if (levels == 1) return;

                save();

                levels--;

                load();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void load()
        {
            try
            {
                lblLevels.Text = levels.ToString();
                DataGridView1.RowCount = levels;

                for (int i = 1; i <= levels; i++)
                {
                    string[] msgtokens = INI.getINI(Common.sfile, "valueDistribution", i.ToString()).Split(';');
                    int nextToken = 0;

                    if (msgtokens.Length >= 4)
                    {

                        DataGridView1[0, i - 1].Value = msgtokens[nextToken++];
                        DataGridView1[1, i - 1].Value = msgtokens[nextToken++];
                        DataGridView1[2, i - 1].Value = msgtokens[nextToken++];
                        DataGridView1[3, i - 1].Value = msgtokens[nextToken++];
                    }

                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void save()
        {
            try
            {
                INI.writeINI(Common.sfile, "valueDistribution", "levels", levels.ToString());

                for (int i = 1; i <= levels; i++)
                {
                    string str = "";

                    for (int j = 1; j <= DataGridView1.ColumnCount; j++)
                    {
                        str += DataGridView1[j - 1, i - 1].Value + ";";
                    }

                    INI.writeINI(Common.sfile, "valueDistribution", i.ToString(), str);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
