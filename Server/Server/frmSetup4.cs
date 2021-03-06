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
    public partial class frmSetup4 : Form
    {
        public CirclePoint[] circlePoints;
        public int controlPointCount = 0;
        public double maxValue;                               //max possible value this period 
        public int[] maxValueLocations = new int[1000];       //locations of max value
        public int maxValueLocationCount = 0;                 //number of locations that have max value

        public frmSetup4()
        {
            InitializeComponent();
        }        

        private void frmSetup4_Load(object sender, EventArgs e)
        {
            try
            {
                txtStartLocation.Text = INI.getINI(Common.sfile, "instructions", "startingLocation");
                controlPointCount = int.Parse(INI.getINI(Common.sfile, "instructions", "controlPointCount"));

                dgInstructions.RowCount = controlPointCount;

                for(int i=1;i<=controlPointCount;i++)
                {
                    dgInstructions[0, i - 1].Value = i;

                    string[] mstokens = INI.getINI(Common.sfile, "instructions", i.ToString()).Split(';');

                    dgInstructions[1, i - 1].Value = mstokens[0];
                    dgInstructions[2, i - 1].Value = mstokens[1];
                }

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
                INI.writeINI(Common.sfile, "instructions", "startingLocation", txtStartLocation.Text);
                INI.writeINI(Common.sfile, "instructions", "controlPointCount", dgInstructions.RowCount.ToString());

                for (int i = 1; i <= controlPointCount; i++)
                {
                    string str = "";

                    str = dgInstructions[1, i - 1].Value.ToString() + ";";
                    str += dgInstructions[2, i - 1].Value.ToString() + ";";

                    INI.writeINI(Common.sfile, "instructions", i.ToString(), str);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdGenerate_Click(object sender, EventArgs e)
        {
            try 
            {
                Common.loadValueDistributions();

                dgInstructions.RowCount = 0;
                controlPointCount = 0;

                Common.setupCirclePoints(ref circlePoints, ref maxValue, ref maxValueLocationCount, ref maxValueLocations, 0);

                for (int i = 1; i <= Common.circlePointCount; i++)
                {
                    if(circlePoints[i].controlPoint)
                    {
                        controlPointCount++;
                        dgInstructions.RowCount = controlPointCount;

                        dgInstructions[0, dgInstructions.RowCount - 1].Value = controlPointCount;
                        dgInstructions[1, dgInstructions.RowCount - 1].Value = i;
                        dgInstructions[2, dgInstructions.RowCount - 1].Value = circlePoints[i].value;
                    }
                }

                txtStartLocation.Text = Rand.rand(Common.circlePointCount, 1).ToString();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
