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
    public partial class frmSetup2 : Form
    {

        public frmSetup2()
        {
            InitializeComponent();
        }

        private void frmSetup2_Load(object sender, EventArgs e)
        {
            try
            {
                dgGroups.RowCount = Common.numberOfPeriods;
                dgGroups.ColumnCount = Common.numberOfPlayers + 1;

                DataGridViewComboBoxCell dgcbc = new DataGridViewComboBoxCell();
                DataGridViewComboBoxColumn dgvcbc = (DataGridViewComboBoxColumn) dgGroups.Columns[1];


                //for (int i=1;i<= dgvcbc.Items.Count)

                //dgcbc.Items.Add()

                for (int i = 0; i < dgGroups.RowCount; i++)
                {
                    dgGroups[0, i].Value = i + 1;

                    string[] sinstr = INI.getINI(Common.sfile, "periods", (i + 1).ToString()).Split(';');


                    if(sinstr.Length > 1)
                    {
                        int nextToken = 0;
                        for (int j = 1; j <= dgGroups.ColumnCount-1; j++)
                        {
                            if (j > 1) dgGroups[j, i] = (DataGridViewComboBoxCell) dgGroups[1, i].Clone();

                            dgGroups.Columns[j].HeaderText = "Player " + j;

                            if (sinstr.Length > nextToken)
                            {
                                if (sinstr[nextToken] != "")
                                {
                                    dgGroups[j, i].Value = sinstr[nextToken++];
                                }
                            }
                        }
                    }
                }

                dgGroups.CurrentCell.Selected = false;
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
                for(int i=0;i<dgGroups.RowCount;i++)
                {
                    string outstr = "";

                    for(int j=1;j<=dgGroups.ColumnCount-1;j++)
                    {
                        outstr += dgGroups[j, i].Value + ";";

                        if(i==0 && dgGroups[j, i].Value.ToString()=="Sorted")
                        {
                            MessageBox.Show("First period cannot be 'Sorted'","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            return;
                        }
                    }

                    INI.writeINI(Common.sfile, "periods", (i+1).ToString(), outstr);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {                

            } catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdCopyDown_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                int r = dgGroups.CurrentCell.RowIndex;

                for(int i=r;i<dgGroups.RowCount;i++)
                {
                    for(int j=1;j<dgGroups.ColumnCount;j++)
                    {
                        dgGroups[j, i].Value = dgGroups[j, r].Value;
                    }                    
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
}

        private void cmdCopyRight_Click(object sender, EventArgs e)
        {
            try
            {               
                DataGridViewCell dgvc = dgGroups.CurrentCell;

                if (dgvc.Value == null) return;
                if ((string) dgvc.Value == "") return;

                for(int i=dgvc.ColumnIndex+1;i<dgGroups.ColumnCount;i++)
                {
                    dgGroups[i, dgvc.RowIndex].Value = dgvc.Value;
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
