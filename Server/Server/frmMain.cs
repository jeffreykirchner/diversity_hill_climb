using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Server
{
    public partial class frmMain : Form
    {
        //network variables
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = default(IPAddress);
        IPEndPoint localEndPoint = default(IPEndPoint);

        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        bool resetPressed = false;

        public Screen mainScreen;

        public Font f6 = new Font("Microsoft Sans Serif", 6, System.Drawing.FontStyle.Bold);
        public Font f8 = new Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold);
        public Font f10 = new Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold);

        public StringFormat fmt = new StringFormat(); //center alignment
        public StringFormat fmtL = new StringFormat(); //center alignment
        public StringFormat fmtR = new StringFormat(); //center alignment

        public Pen p3Blue;
        public Pen p3Gold;
        public Pen p3Black;
        public Pen p10BackArrow;
        public Pen p3StartColor;
        public Pen p3EndColor;
        public Pen p1BlackDash;

        public float circleDiameter;
        public float circleRadius;
        public float circleOffset = 25;
        public float circleDataArea = 250;
        public float tempDegree = 360;
        public PointF zeroLine;

        public Color startColor = Color.CornflowerBlue;
        public Color endColor = Color.Crimson;
        public Color moveColor = Color.LightBlue;

        public int displayPeriod = 1;                             //period to be shown on the status screen.

        public PointF selectionPoint = new Point(0, 0);
        public Color selectionColor = Color.Green;

        public frmMain()
        {
            InitializeComponent();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            Common.start();

            //find the IPv4 address
            for (int i = 1; i <= ipHostInfo.AddressList.Length; i++)
            {
                if (ipHostInfo.AddressList[i - 1].ToString().IndexOf(".") > -1)
                {
                    ipAddress = ipHostInfo.AddressList[i - 1];
                    break;
                }
            }

            //start network listener
            localEndPoint = new IPEndPoint(IPAddress.Any, Common.portNumber);

            listener.Bind(localEndPoint);
            listener.Listen(10);

            bwTakeSocketConnections.RunWorkerAsync();

            //display network info on form
            lblIpAddress.Text = ipAddress.ToString();
            lblLocalHost.Text = SystemInformation.ComputerName;
            lblConnectionCount.Text = "0";

            //graphics
            mainScreen = new Screen(pnlMain, new Rectangle(0, 0, pnlMain.Width, pnlMain.Height));

            fmt.Alignment = StringAlignment.Center;
            fmtL.Alignment = StringAlignment.Near;
            fmtR.Alignment = StringAlignment.Far;

            p1BlackDash = new Pen(new SolidBrush(Color.FromArgb(75, Color.Black)), (float)0.5);
            p1BlackDash.DashStyle = DashStyle.Dot;
            p1BlackDash.Alignment = PenAlignment.Center;

            p3Blue = new Pen(Brushes.CornflowerBlue, 3);
            p3Blue.Alignment = PenAlignment.Center;

            p3Black = new Pen(Brushes.Black, 3);
            p3Black.Alignment = PenAlignment.Center;

            p3Gold = new Pen(Brushes.Gold, 3);
            p3Gold.Alignment = PenAlignment.Center;

            p3StartColor = new Pen(startColor, 3);
            p3StartColor.Alignment = PenAlignment.Center;
            p3StartColor.EndCap = LineCap.Triangle;
            p3StartColor.StartCap = LineCap.Triangle;

            p3EndColor = new Pen(endColor, 3);
            p3EndColor.Alignment = PenAlignment.Center;
            p3EndColor.EndCap = LineCap.Triangle;
            p3EndColor.StartCap = LineCap.Triangle;

            p10BackArrow = new Pen(Brushes.Black, 10);
            p10BackArrow.Alignment = PenAlignment.Center;
            p10BackArrow.EndCap = LineCap.ArrowAnchor;

            circleDiameter = Math.Min(pnlMain.Height, pnlMain.Width) - circleOffset * 2 - circleDataArea * 2;
            circleRadius = circleDiameter / 2;
        }

        private void cmdBegin_Click(object sender, EventArgs e)
        {
            try
            {
                txtError.Text = "";
                TabControl1.TabPages[2].Text = "Messages";

                Common.loadParameters();
                Common.closing = false;

                //if the number of connections does not match the parameters then exit
                if (Common.numberOfPlayers != Common.clientCount)
                {
                    MessageBox.Show("Incorrect number of clients");
                    return;
                }             

                //data files               
                string tempTime = DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour +
                                  "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;

                //store parameters
                string filename = "Parameters_" + tempTime + ".csv";
                filename = Application.StartupPath.ToString() + "\\datafiles\\" + filename;
                INI.writeINI(Common.sfile, "GameSettings", "gameName", "ESI Software2");
                INI.writeINI(Common.sfile, "GameSettings", "gameName", "ESI Software");
                File.Copy(Common.sfile, filename);

                //summary data file
                filename = "Round_Data_" + tempTime + ".csv";
                filename = Application.StartupPath.ToString() + "\\datafiles\\" + filename;

                Common.roundDf = File.CreateText(filename);
                Common.roundDf.AutoFlush = true;

                string str = "Period,Grouping,Group,Round,GroupID,Player,StartLocation,StartValue,EndLocation,EndValue,TurnCount,MaxValue,AutoSubmit,";

                for(int i=1;i<=Common.movesPerTurn;i++)
                {
                    str += "Move" + i + "Direction,";
                    str += "Move" + i + "Distance,";
                }

                Common.roundDf.WriteLine(str);

                //summary data file
                filename = "Period_Data_" + tempTime + ".json";
                filename = Application.StartupPath.ToString() + "\\datafiles\\" + filename;

                Common.periodsDf = File.CreateText(filename);
                Common.periodsDf.AutoFlush = true;

                //events data file
                filename = "Group_Data_" + tempTime + ".csv";
                filename = Application.StartupPath.ToString() + "\\datafiles\\" + filename;

                Common.groupDf = File.CreateText(filename);
                Common.groupDf.AutoFlush = true;

                str = "Period,Grouping,GroupNumber,StartLocation,StartValue,EndLocation,EndValue,MaxValue,TotalTurns,TotalRounds,";
                Common.groupDf.WriteLine(str);

                //replay data file
                //filename = "Replay_Data_" + tempTime + ".csv";
                //filename = Application.StartupPath.ToString() + "\\datafiles\\" + filename;

                //Common.replayDf = File.CreateText(filename);
                //Common.replayDf.AutoFlush = true;

                //str = "Period,Group,Round,Player,Turn,Move";               
                //Common.replayDf.WriteLine(str);

                //summary table
                dgMain.RowCount = Common.numberOfPlayers;

                for (int i = 1; i <= Common.numberOfPlayers; i++)
                {
                    dgMain[0, i - 1].Value = i;
                    dgMain[1, i - 1].Value = Common.playerlist[i].sp.remoteComputerName;
                    dgMain[3, i - 1].Value = string.Format(Common.culture, "{0:C}", 0);

                    Common.playerlist[i].groupNumberRandom = -1;
                    Common.playerlist[i].groupNumberSorted = -1;
                    Common.playerlist[i].totalInduvidualScore = 0;

                    Common.playerlist[i].groupNumber = new int[Common.numberOfPeriods + 1];

                    Common.playerlist[i].loadGroupings();

                    dgMain[6, i - 1].Value = Common.playerlist[i].groupAssignments[1];
                }

                //asign random group                
                //int counter = 0;
                //int currentGroup = 1;
                //while(counter<Common.numberOfPlayers)
                //{
                //    int r = Rand.rand(Common.numberOfPlayers, 1);

                //    if(Common.playerlist[r].groupNumberRandom == -1)
                //    {
                //        Common.playerlist[r].groupNumberRandom = currentGroup;
                //        counter++;

                //        if (counter % Common.groupSize == 0)
                //            currentGroup++;
                //    }
                //}

                //find number of groups               
                //Common.numberOfGroups = 1;
                //for (int i = 1; i <= Common.numberOfPlayers; i++)
                //{
                //    if (Common.playerlist[i].groupNumberRandom > Common.numberOfGroups)
                //        Common.numberOfGroups = Common.playerlist[i].groupNumberRandom;                   
                //}

                dgMain.CurrentCell.Selected = false;

                //setup screen
                cmdBegin.Enabled = false;
                cmdEndEarly.Enabled = true;
                cmdExchange.Enabled = false;
                cmdExit.Enabled = false;
                cmdLoad.Enabled = false;
                cmdSetup1.Enabled = false;
                cmdSetup2.Enabled = false;
                cmdSetup3.Enabled = false;
                cmdSetup4.Enabled = false;
                cmdSetup5.Enabled = false;
                cmdReplay.Enabled = false;

                //load distributions
                Common.loadValueDistributions();

                Common.periodList = new Period[Common.numberOfPeriods+1];
                //setup periods

                tempDegree = 360 / (float)Common.circlePointCount;
                Common.dataUnit = circleDataArea / (Common.maxControlPointValue - Common.minControlPointValue);
                zeroLine = Common.returnLocation(0, 0);

                if (Common.showInstructions)
                {
                    Common.currentPeriod = 0;
                    displayPeriod = 0;
                }
                else
                {
                    Common.currentPeriod = 1;
                    displayPeriod = 1;
                }

                for (int i=1;i<=Common.numberOfPeriods;i++)
                {
                    Common.periodList[i] = new Period();
                    Common.periodList[i].setup(i);                    
                }               

                for (int i = 1; i <= Common.numberOfPlayers; i++)
                {
                    if (Common.playerlist[i].groupAssignments[1] == "Sorted")
                    {
                        MessageBox.Show("You cannot start with a Sorted period.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }                                               

                if (Common.playerlist[1].groupAssignments[1] != "Individual")
                {
                    Common.calcGroups();
                }

                for (int i = 1; i <= Common.numberOfPlayers; i++)
                {                   
                    Common.playerlist[i].setupGroupNumber();
                }

                //instruction period setup
                if (Common.showInstructions)
                {
                    Common.periodList[0] = new Period();
                    Common.periodList[0].setup(0);
                }

                Common.periodList[Common.currentPeriod].setupPeriod();               

                //send signal to begin to clients
                string outstr = "";

                for (int i = 1; i <= Common.numberOfPlayers; i++)
                {
                    Common.playerlist[i].sendBegin(outstr);
                }

                Common.checkin = 0;
                Common.FrmServer.lblTimeRemaining.Text = "-";

                Timer1.Enabled = true;
                refreshStatusDisplay();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }

        }
       

        private void cmdReset_Click(object sender, EventArgs e)
        {
            try
            {               

                for (int i = 1; i <= Common.clientCount; i++)
                {
                    Common.playerlist[i].sendReset();
                }

                Application.DoEvents();
                Common.closing = true;

                if (Common.roundDf != null) Common.roundDf.Close();
                if (Common.eventsDf != null) Common.eventsDf.Close();
                if (Common.groupDf != null) Common.groupDf.Close();
                if (Common.periodsDf != null) Common.periodsDf.Close();

                bwTakeSocketConnections.CancelAsync();                
                listener.Close();

                cmdBegin.Enabled = true;
                cmdEndEarly.Enabled = false;
                cmdExchange.Enabled = true;
                cmdExit.Enabled = true;
                cmdLoad.Enabled = true;
                cmdSetup1.Enabled = true;
                cmdSetup2.Enabled = true;
                cmdSetup3.Enabled = true;
                cmdSetup4.Enabled = true;
                cmdSetup5.Enabled = true;
                cmdReplay.Enabled = true;

                dgMain.RowCount = 0;
                Timer1.Enabled = false;
                Timer2.Enabled = false;
                Common.FrmServer.lblTimeRemaining.Text = "-";

                if (Common.FrmReplay != null)
                    if (Common.FrmReplay.Visible)
                        Common.FrmReplay.Close();
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
                //save current parameters to a text file so they can be loaded at a later time                
                SaveFileDialog1.FileName = "";
                SaveFileDialog1.Filter = "Parameter Files (*.txt)|*.txt";
                SaveFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
                SaveFileDialog1.ShowDialog();

                if (string.IsNullOrEmpty(SaveFileDialog1.FileName))
                {
                    return;
                }

               File.Copy(Common.sfile, SaveFileDialog1.FileName,true);
                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string tempS="";
                string sinstr="";

                //dispaly open file dialog to select file
                OpenFileDialog1.FileName = "";
                OpenFileDialog1.Filter = "Parameter Files (*.txt)|*.txt";
                OpenFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;

                OpenFileDialog1.ShowDialog();

                //if filename is not empty then continue with load
                if (string.IsNullOrEmpty(OpenFileDialog1.FileName))
                {
                    return;
                }

                tempS = OpenFileDialog1.FileName;

                sinstr = INI.getINI(tempS, "gameSettings", "gameName");

                //check that this is correct type of file to load
                if (sinstr != "ESI Software")
                {
                    MessageBox.Show("Invalid file","Error",MessageBoxButtons.OK ,MessageBoxIcon.Error);
                    return;
                }

                File.Copy(OpenFileDialog1.FileName, Common.sfile , true);

                //load new parameters into server
                Common.loadParameters();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSetup1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.FrmSetup1 != null)
                    if (Common.FrmSetup1.Visible)
                        return;

                Common.FrmSetup1 = new frmSetup1();
                Common.FrmSetup1.Show();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSetup2_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (Common.FrmSetup2 != null)
                    if (Common.FrmSetup2.Visible)
                        return;

                Common.FrmSetup2 = new frmSetup2();
                Common.FrmSetup2.Show();

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSetup3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.FrmSetup3 != null)
                    if (Common.FrmSetup3.Visible)
                        return;

                Common.FrmSetup3 = new frmSetup3();
                Common.FrmSetup3.Show();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSetup4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.FrmSetup4 != null)
                    if (Common.FrmSetup4.Visible)
                        return;

                Common.FrmSetup4 = new frmSetup4();
                Common.FrmSetup4.Show();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSetup5_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.FrmSetup5 != null)
                    if (Common.FrmSetup5.Visible)
                        return;

                Common.FrmSetup5 = new frmSetup5();
                Common.FrmSetup5.Show();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdEndEarly_Click(object sender, EventArgs e)
        {
            try
            {
                cmdEndEarly.Enabled = false;

                Common.numberOfPeriods = Common.currentPeriod;

                for (int i = 1; i <= Common.numberOfPlayers; i++)
                {
                    Common.playerlist[i].sendEndEarly();
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdExchange_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            try
            {
                Common.closing = true;

                Close();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            EventLog.AppEventLog_Close();
        }

        

        //tread safe handling of incoming message from a client
        delegate void StringArgReturningVoidDelegate(object sender, ListEventArgs e);

        private void setTakeMessage(object sender, ListEventArgs e)
        {
            // InvokeRequired required compares the thread ID of the  
            // calling thread to the thread ID of the creating thread.  
            // If these threads are different, it returns true.  
            if (this.txtMain.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(setTakeMessage);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                Common.takeMessage(e.Data);
            }
        }

        //thread safe update to number of clients
        delegate void StringArgReturningVoidDelegate2(string text);

        private void setConnectionsLabel(string text)
        {
            // InvokeRequired required compares the thread ID of the  
            // calling thread to the thread ID of the creating thread.  
            // If these threads are different, it returns true.  
            if (this.lblConnectionCount.InvokeRequired)
            {
                StringArgReturningVoidDelegate2 d = new StringArgReturningVoidDelegate2(setConnectionsLabel);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lblConnectionCount.Text = text;
            }
        }

        //tread safe handling txterror
        delegate void StringDelegateTxtError(string text);

        public void setTxtError(string text)
        {
            // InvokeRequired required compares the thread ID of the  
            // calling thread to the thread ID of the creating thread.  
            // If these threads are different, it returns true.  
            if (this.txtMain.InvokeRequired)
            {
                StringDelegateTxtError d = new StringDelegateTxtError(setTxtError);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                txtError.Text += text;
            }
        }

        //tread safe handling TabControl1 tab messages
        delegate void StringVoidDelegateTabMessages(string text);

        public void setTabMessages(string text)
        {
            // InvokeRequired required compares the thread ID of the  
            // calling thread to the thread ID of the creating thread.  
            // If these threads are different, it returns true.  
            if (this.txtMain.InvokeRequired)
            {
                StringVoidDelegateTabMessages d = new StringVoidDelegateTabMessages(setTabMessages);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                TabControl1.TabPages[2].Text = text;
            }
        }

        //upate the number of connected clients
        public void refreshConnectionsLabel()
        {
            try
            {
                setConnectionsLabel(Common.clientCount.ToString());
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //loop taking new network connections from the clients
        private void bwTakeSocketConnections_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                bool go = true;

                while (go)
                {
                    Socket tempSocket = listener.Accept();

                    Common.clientCount += 1;
                    Common.playerlist[Common.clientCount] = new player();

                    Common.playerlist[Common.clientCount].sp.socketHandler = tempSocket;

                    Common.playerlist[Common.clientCount].sp.startReceive();

                    Common.playerlist[Common.clientCount].sp.messageReceived += new EventHandler<ListEventArgs>(setTakeMessage);

                    Common.playerlist[Common.clientCount].inumber = Common.clientCount;
                    Common.playerlist[Common.clientCount].sp.inumber = Common.clientCount;

                    refreshConnectionsLabel();

                    if (cmdBegin.Enabled == false)
                    {
                        Common.playerlist[Common.clientCount].sendInvalidConnection();
                        Common.clientCount -= 1;
                    }                

                    if (resetPressed)
                        go = false;
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //reset the network listeners
        private void bwTakeSocketConnections_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                resetPressed = false;

                for (int i = 1; i <= Common.clientCount; i++)
                {
                    Common.playerlist[i].sp.stopping = true;
                    Common.playerlist[i].sp = null;
                }

                Common.clientCount = 0;

                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                listener.Bind(localEndPoint);
                listener.Listen(10);

                bwTakeSocketConnections.RunWorkerAsync();
                refreshConnectionsLabel();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (PrintDialog1.ShowDialog() == DialogResult.OK)
                {
                    PrintDocument1.Print();
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                int i = 0;
                System.Drawing.Font f = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
                int tempN = 0;

                // e.Graphics.DrawString(filename, f, Brushes.Black, 10, 10)

                f = new System.Drawing.Font("Arial", 15, System.Drawing.FontStyle.Bold);

                e.Graphics.DrawString("Name", f, System.Drawing.Brushes.Black, 10, 30);
                e.Graphics.DrawString("Earnings", f, System.Drawing.Brushes.Black, 400, 30);

                f = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);

                tempN = 55;

                for (i = 1; i <= dgMain.RowCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        e.Graphics.FillRectangle(System.Drawing.Brushes.Aqua, 0, tempN, 500, 19);
                    }
                    e.Graphics.DrawString(dgMain.Rows[i - 1].Cells[1].Value.ToString(), f, System.Drawing.Brushes.Black, 10, tempN);
                    e.Graphics.DrawString(dgMain.Rows[i - 1].Cells[3].Value.ToString(), f, System.Drawing.Brushes.Black, 400, tempN);

                    tempN += 20;
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void frmMain_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            EventLog.AppEventLog_Close();
        }        

        private void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                refreshScreen();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        
        private void TabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                TabControl1.Refresh();
            }
             catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        // thread safe return handle to panel2
        public delegate void getPanel2Callback(ref IntPtr handle);

        public void getPanel2(ref IntPtr handle)
        {
            if (this.pnlMain.InvokeRequired)
            {
                getPanel2Callback d = new getPanel2Callback(getPanel2);
                this.Invoke(d, new object[] { handle });
            }
            else
            {
                handle = pnlMain.Handle;
            }

        }

        private void cmdReplay_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.FrmReplay != null)
                    if (Common.FrmReplay.Visible)
                        return;

                Common.FrmReplay = new frmReplay();
                Common.FrmReplay.Show();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void refreshStatusDisplay()
        {
            try
            {
                displayPeriod = Common.currentPeriod;

                Period p = Common.periodList[displayPeriod];

                if (p == null) return;

                lblPeriod.Text = displayPeriod.ToString();
                //lblGrouping.Text = p.groupAssignment;
                lblStartingValue.Text = string.Format("{0:0.00}", Math.Round(p.circlePoints[p.startLocation].value, 2), 2);
                lblMaxValue.Text = string.Format("{0:0.00}", p.maxValue);

                dgGroup.RowCount = p.periodGroupCount;

                for (int i = 1; i <= p.periodGroupCount; i++)
                {
                    dgGroup[0, i - 1].Value = i;

                    string strMembers = "";
                    int turnCounter = 0;

                    for (int j = 1; j <= p.periodGroups[i].periodGroupPlayerCount; j++)
                    {
                        if (strMembers != "") strMembers += ", ";
                        strMembers += p.periodGroups[i].periodGroupPlayers[j].playerNumber;

                        for (int k = 1; k <= p.periodGroups[i].roundCount; k++)
                        {
                            if (p.periodGroups[i].periodGroupPlayers[j].periodGroupPlayerRounds[k] != null)
                            {
                                turnCounter += p.periodGroups[i].periodGroupPlayers[j].periodGroupPlayerRounds[k].turnCount;
                            }
                        }
                    }

                    dgGroup[1, i - 1].Value = strMembers;
                    dgGroup[2, i - 1].Value = turnCounter;
                    if (p.periodGroups[i].endingLocation != 0)
                    {
                        dgGroup[3, i - 1].Value = string.Format("{0:0.00}", Math.Round(p.circlePoints[p.periodGroups[i].endingLocation].value, 2), 2);
                    }
                    dgGroup[4, i - 1].Value = p.periodGroups[i].groupAssignment;
                }

                refreshStatusDisplay2();             

                refreshScreen();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void refreshStatusDisplay2()
        {
            try
            {
                Period p = Common.periodList[displayPeriod];
                if (p == null) return;

                if (dgGroup.CurrentRow == null) return;

                int tempPG = dgGroup.CurrentRow.Index + 1;
                PeriodGroup pg = p.periodGroups[tempPG];
                if (pg == null) return;

                //show turn by turn moves              

                int turnCounter2 = 1;
                if (dgTurns.RowCount < turnCounter2) dgTurns.RowCount++;

                dgTurns[0, turnCounter2 - 1].Value = "";
                dgTurns[1, turnCounter2 - 1].Value = "";
                dgTurns[2, turnCounter2 - 1].Value = "";
                dgTurns[3, turnCounter2 - 1].Value = "";
                dgTurns[4, turnCounter2 - 1].Value = "Start";
                dgTurns[5, turnCounter2 - 1].Value = "";
                dgTurns[6, turnCounter2 - 1].Value = string.Format("{0:0.00}", Math.Round(p.circlePoints[p.startLocation].value, 2), 2);

                for (int k = 1; k <= pg.roundCount; k++)
                {
                    for (int j = 1; j <= pg.periodGroupPlayerCount; j++)
                    {
                        if (pg.periodGroupPlayers[j].periodGroupPlayerRounds[k] != null)
                        {                       

                            for (int i = 1; i <= pg.periodGroupPlayers[j].periodGroupPlayerRounds[k].turnCount; i++)
                            {

                                for (int h = 1; h <= pg.periodGroupPlayers[j].periodGroupPlayerRounds[k].turns[i].turnMovesCount; h++)
                                {
                                    TurnMove tm = pg.periodGroupPlayers[j].periodGroupPlayerRounds[k].turns[i].turnMoves[h];
                                    turnCounter2++;

                                    if (dgTurns.RowCount < turnCounter2) dgTurns.RowCount++;

                                    dgTurns[0, turnCounter2 - 1].Value = k;
                                    dgTurns[1, turnCounter2 - 1].Value = pg.periodGroupPlayers[j].playerNumber;
                                    dgTurns[2, turnCounter2 - 1].Value = i;
                                    dgTurns[3, turnCounter2 - 1].Value = h;

                                    if (tm.direction == "Clockwise")
                                        dgTurns[4, turnCounter2 - 1].Value = "CW";
                                    else
                                        dgTurns[4, turnCounter2 - 1].Value = "CCW";

                                    dgTurns[5, turnCounter2 - 1].Value = tm.distance;
                                    dgTurns[6, turnCounter2 - 1].Value = string.Format("{0:0.00}", Math.Round(p.circlePoints[tm.circlePointEnd].value, 2), 2);

                                }
                            }

                        }
                    }
                }

                if (dgTurns.RowCount > turnCounter2) dgTurns.RowCount = turnCounter2;
                if (dgTurns.RowCount > 0) dgTurns.Rows[0].Selected = true;
                dgTurns_selectionChangedAction(0);
            }
             catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }


        public void refreshScreen()
        {
            try
            {
                Period p = Common.periodList[displayPeriod];
                PeriodGroup pg = null;

                if (p.periodGroups == null) return;

                if (dgGroup.RowCount > 0 && dgGroup.CurrentRow != null)
                {
                    if (dgGroup.CurrentRow.Index + 1 >= 1 && dgGroup.CurrentRow.Index + 1 <= p.periodGroupCount)
                    {
                        if (p.periodGroups[dgGroup.CurrentRow.Index + 1] != null)
                        {
                            pg = p.periodGroups[dgGroup.CurrentRow.Index + 1];
                        }
                    }
                    else
                    {
                        dgGroup.Rows[0].Selected = true;
                        pg = p.periodGroups[dgGroup.CurrentRow.Index + 1];
                    }
                }

                if (mainScreen == null) return;

                mainScreen.erase1();
                Graphics g = mainScreen.GetGraphics();

                g.SmoothingMode = SmoothingMode.AntiAlias;

                //legend
                g.FillRectangle(new SolidBrush(startColor), new RectangleF(pnlMain.Width - 170, pnlMain.Height - 115 + 15, 20, 20));
                g.DrawString("Starting Location", f10, Brushes.Black, pnlMain.Width - 140, pnlMain.Height - 115 + 18, fmtL);

                g.FillRectangle(new SolidBrush(moveColor), new RectangleF(pnlMain.Width - 170, pnlMain.Height - 115 + 45, 20, 20));
                g.DrawString("Moves", f10, Brushes.Black, pnlMain.Width - 140, pnlMain.Height - 115 + 48, fmtL);

                g.FillRectangle(new SolidBrush(endColor), new RectangleF(pnlMain.Width - 170, pnlMain.Height - 115 + 75, 20, 20));
                g.DrawString("Ending Location", f10, Brushes.Black, pnlMain.Width - 140, pnlMain.Height - 115 + 78, fmtL);

                //tick marks

                float degreeCounter = -90;

                g.TranslateTransform((float)pnlMain.Width / (float)2, (float)pnlMain.Height / (float)2);

                g.DrawEllipse(p3Black, new RectangleF(new PointF(-circleDiameter / (float)2, -circleDiameter / (float)2), new SizeF(circleDiameter, circleDiameter)));

                for (int i = 1; i <= Common.circlePointCount; i += 10)
                {
                    double tempLength = 10;

                    if (i % 50 == 1)
                        tempLength = 20;

                    g.DrawLine(Pens.Black,
                          new PointF((float)circleRadius * (float)Math.Cos(degreeCounter * (Math.PI / 180)),
                                    (float)circleRadius * (float)Math.Sin(degreeCounter * (Math.PI / 180))),
                          new PointF((float)(circleRadius - tempLength) * (float)Math.Cos(degreeCounter * (Math.PI / 180)),
                                    (float)(circleRadius - tempLength) * (float)Math.Sin(degreeCounter * (Math.PI / 180))));

                    if (tempLength == 20)
                        g.DrawString(i.ToString(),
                                 f6,
                                 Brushes.Black,
                                 new PointF((float)(circleRadius - tempLength - 10) * (float)Math.Cos(degreeCounter * (Math.PI / 180)),
                                            (float)(circleRadius - tempLength - 10) * (float)Math.Sin(degreeCounter * (Math.PI / 180)) - 3),
                                 fmt);

                    degreeCounter += tempDegree * 10;
                }

                //zero line
                g.DrawEllipse(p1BlackDash, new RectangleF(-zeroLine.X, -zeroLine.X, zeroLine.X * 2, zeroLine.X * 2));
                g.DrawString("Zero", f8, Brushes.DimGray, new PointF(0, -zeroLine.X), fmt);

                GraphicsPath gp = new GraphicsPath();
                gp.AddEllipse(new RectangleF(-zeroLine.X, -zeroLine.X, zeroLine.X * 2, zeroLine.X * 2));                

                //draw line
                for (int i = 1; i < Common.circlePointCount; i++)
                {
                    g.DrawLine(Pens.LightGray, p.circlePoints[i].location, p.circlePoints[i + 1].location);
                }

                g.DrawLine(Pens.LightGray, p.circlePoints[1].location, p.circlePoints[Common.circlePointCount].location);

                //draw results
                if (pg != null)
                {
                    pg.draw(g);

                    g.DrawLine(p3Gold, new Point(0, 0), selectionPoint);
                    g.DrawLine(new Pen(selectionColor), new Point(0, 0), selectionPoint);
                }

                //center point
                g.FillEllipse(Brushes.Black, new RectangleF(-5, -5, 10, 10));

                if(pg != null) pg.drawLabels(g);

                g.ResetTransform();

                mainScreen.flip();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
               
        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (Common.periodList == null) return;
                
                Period p = Common.periodList[displayPeriod];
                if (p == null) return;
                if (dgGroup.CurrentRow == null) return;
                if (p.periodGroups == null) return;

                PeriodGroup pg = p.periodGroups[dgGroup.CurrentRow.Index + 1];

                if (pg == null) return;
                if (dgGroup.RowCount == 0) return;


                clearMouseOver();

                //check over end label
                if (pg.labelEnd.isOver(mainScreen.GetGraphics(), e.Location))
                {
                    pg.labelEnd.mouseIsOver = true;
                    refreshScreen();
                    return;
                }

                //check over turn labels
                for (int i = pg.periodGroupPlayerCount; i >= 1; i--)
                {
                    if (pg.periodGroupPlayers[i].labelStart.isOver(mainScreen.GetGraphics(), e.Location))
                    {
                        pg.periodGroupPlayers[i].labelStart.mouseIsOver = true;
                        
                        refreshScreen();

                        return;
                    }

                    if (pg.periodGroupPlayers[i].labelEnd.isOver(mainScreen.GetGraphics(), e.Location))
                    {
                        pg.periodGroupPlayers[i].labelEnd.mouseIsOver = true;
                       
                        refreshScreen();
                        return;
                    }
                }

                //check over start labels
                if (pg.labelStart.isOver(mainScreen.GetGraphics(), e.Location))
                {
                    pg.labelStart.mouseIsOver = true;
                    refreshScreen();
                    return;
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }        

        public void clearMouseOver()
        {
            try
            {

                Period p = Common.periodList[displayPeriod];

                for(int j=1;j<=p.periodGroupCount;j++)
                {
                    p.periodGroups[j].labelStart.mouseIsOver = false;
                    p.periodGroups[j].labelEnd.mouseIsOver = false;

                    for (int i = p.periodGroups[j].periodGroupPlayerCount; i >= 1; i--)
                    {        
                        p.periodGroups[j].periodGroupPlayers[i].labelStart.mouseIsOver = false;
                        p.periodGroups[j].periodGroupPlayers[i].labelEnd.mouseIsOver = false;
                    }
                }
               
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void dgGroup_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                clearMouseOver();
                refreshStatusDisplay2();
                refreshScreen();               
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if(Common.timeRemaining == 0)
                {
                    Timer2.Enabled = false;
                }
                else
                {
                    Common.timeRemaining--;
                }

                lblTimeRemaining.Text = Common.timeConversion(Common.timeRemaining);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void dgTurns_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                dgTurns_selectionChangedAction(dgTurns.CurrentRow.Index);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void dgTurns_selectionChangedAction(int r)
        {
            try
            {
                if (dgTurns.RowCount == 0) return;
                if (dgTurns.RowCount < r-1) return;

                if (dgTurns[4, r].Value == null) return;

                Period p = Common.periodList[displayPeriod];
                if (p == null) return;

                int tempPG = dgGroup.CurrentRow.Index + 1;
                PeriodGroup pg = p.periodGroups[tempPG];
                if (pg == null) return;

                if (dgTurns[4, r].Value.ToString() == "Start")
                {
                    if (pg.startingLocation == 0) return;
                    selectionPoint = Common.periodList[Common.currentPeriod].circlePoints[pg.startingLocation].location;
                    selectionColor = startColor;
                }
                else if (dgTurns[4, r].Value.ToString() == "End")
                {
                    if (pg.endingLocation == 0) return;
                    selectionPoint = Common.periodList[Common.currentPeriod].circlePoints[pg.endingLocation].location;
                    selectionColor = endColor;
                }
                else
                {
                    int tempTurn = int.Parse(dgTurns[2, r].Value.ToString());
                    int tempMove = int.Parse(dgTurns[3, r].Value.ToString());
                    int tempRound = int.Parse(dgTurns[0, r].Value.ToString()); ;
                    int tempPlayer;

                    tempPlayer = int.Parse(dgTurns[1, r].Value.ToString());                  

                    selectionPoint = pg.getMoveLocation(tempPlayer, tempTurn, tempMove, tempRound,displayPeriod);
                    selectionColor = moveColor;

                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
    
}
