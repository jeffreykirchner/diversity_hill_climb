using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Server
{
    public partial class frmReplay : Form
    {
        //string[] replayEventsDf;
        //string[] replayReplayDf;
        string[] replaySummaryDf;

        string[] replayPeriodNumbers = new string[100000];        //period,time - convert tick to time
        int[,] replayPeriodNumbers2 = new int[1000,5000];        //period,time - time to tick

        int[,] replayDFPointers = new int[1000, 5000];           //period, time, starting pointers to Replay DF

        public frmReplay()
        {
            InitializeComponent();
        }

        private void cmdLoadData_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog1.FileName = "";
                OpenFileDialog1.Filter = "Data Files (*.csv)|*.csv| Data Files (*.json)|*.json";
                OpenFileDialog1.InitialDirectory = Application.StartupPath + "\\DataFiles";

                OpenFileDialog1.ShowDialog();

                if (OpenFileDialog1.FileName == "") return;

                Cursor = Cursors.WaitCursor;

                string[] d = new string[7];

                d[0] = "Group_Data_";
                d[1] = "Replay_Data_";
                d[2] = "Round_Data_";
                d[3] = "Parameters_";
                d[4] = "Period_Data_";
                d[5] = ".csv";
                d[6] = ".json";

                string[] d2 = new string[1];
                d2[0] = "\r\n";

                string[] msgtokens2 = OpenFileDialog1.FileName.Split(d, StringSplitOptions.RemoveEmptyEntries);

                string tempFileName = "";

                //tempFileName = msgtokens2[0] + "Events_Data_" + msgtokens2[1] + ".csv";
                //replayEventsDf = File.ReadAllText(tempFileName).Split(d2,StringSplitOptions.RemoveEmptyEntries);

                //tempFileName = msgtokens2[0] + "Replay_Data_" + msgtokens2[1];
                //replayReplayDf = File.ReadAllText(tempFileName).Split(d2, StringSplitOptions.RemoveEmptyEntries);

                //tempFileName = msgtokens2[0] + "Round_Data_" + msgtokens2[1] + ".csv";
                //replaySummaryDf = File.ReadAllText(tempFileName).Split(d2, StringSplitOptions.RemoveEmptyEntries);

                tempFileName = msgtokens2[0] + "Parameters_" + msgtokens2[1] + ".csv";
                File.Copy(tempFileName, Common.sfile, true);               

                Common.loadParameters();

                Common.showInstructions = false;


                //load players
                for(int i=1;i<=Common.numberOfPlayers;i++)
                {
                    Common.playerlist[i] = new player();
                    Common.playerlist[i].inumber = i;
                }

                //load periods
                tempFileName = msgtokens2[0] + "Period_Data_" + msgtokens2[1] + ".json";
                JObject jo = JObject.Parse(File.ReadAllText(tempFileName));

                //JProperty jp1 = (JProperty)jo.Property("1");

                Common.periodList = new Period[Common.numberOfPeriods + 1];
                for (int i = 1; i <= Common.numberOfPeriods; i++)
                {
                    Common.periodList[i] = new Period();                   


                    //JObject jp = new JObject();
                    //jp = ;

                    Common.periodList[i].fromJSON(jo.Property(i.ToString()));
                }

                Common.FrmServer.tempDegree = 360 / (float)Common.circlePointCount;
                Common.dataUnit = Common.FrmServer.circleDataArea / (Common.maxControlPointValue - Common.minControlPointValue);
                Common.FrmServer.zeroLine = Common.returnLocation(0, 0);

                Common.currentPeriod = 1;
                Common.FrmServer.Timer1.Enabled = true;
                Common.FrmServer.refreshStatusDisplay();

                cmdBack.Enabled = false;

                if (Common.numberOfPeriods == 1) cmdNext.Enabled = false;

                Cursor = Cursors.Default;

                //eventDataPointer = 1;

                ////replay datafile pointers
                //for(int i=1;i< replayReplayDf.Length;i++)
                //{
                //    string[] msgtokens = replayReplayDf[i].Split(',');

                //    if (msgtokens[2] == "1" && msgtokens.Length > 5)
                //        replayDFPointers[int.Parse(msgtokens[0]), int.Parse(msgtokens[1])] = i;
                //}
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            try
            {
                cmdNext.Enabled = true;            

                Common.currentPeriod--;
                Common.FrmServer.refreshStatusDisplay();
                Common.FrmServer.refreshScreen();

                if (Common.currentPeriod == 1) cmdBack.Enabled = false;

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            try
            {
                cmdBack.Enabled = true;

                Common.currentPeriod++;
                Common.FrmServer.refreshStatusDisplay();
                Common.FrmServer.refreshScreen();

                if (Common.currentPeriod == Common.numberOfPeriods) cmdNext.Enabled = false;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
             
    }
}
