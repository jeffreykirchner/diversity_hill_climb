using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Client
{
    public static class Common
    {
        //globals
        public static string sfile;
        public static string myIPAddress = "localhost";
        public static int myPortNumber = 12345;
        public static int inumber;
        public static bool clientClosing = false;

        //forms
        public static frmConnect FrmConnect;
        public static frm1 Frm1;
        public static frmMain FrmClient;
        public static frmNames FrmNames;
        public static frmInstructions FrmInstructions;
        public static frmTestMode FrmTestMode;

        //keeps track of message count
        public static int messageCounter = 0;
        public static int[] playerMessageCounter = new int[101];

        //global parameters
        public static int numberOfPlayers;                     //number of players needed
        public static int numberOfPeriods;                     //number of periods in the experiment         
        public static int instructionX;                        //x location of intstruction window
        public static int instructionY;                        //y location of intstruction window
        public static int windowX;                             //x location of main window
        public static int windowY;                             //y location of main window
        public static bool showInstructions;                   //show instructions before experiment starts
        public static bool testMode;                           //turn on auto test system
                     
        public static int currentPeriod;                       //current period
        
        public static Player[] playerlist = new Player[100];

        public static double earnings=0;                       //total earnings in cents
        
        public static string[] commandLineArgs;

        public static int groupSize;                          //number of subjects in a group                        
        public static int circlePointCount;                   //number of points around the circle
        public static int circleControlPointLow;              //low jump range of control points around the circle determining shape
        public static int circleControlPointHigh;             //high jump range of control points around the circle determining shape
        public static int minControlPointValue;               //lowest height of control point
        public static int maxControlPointValue;               //max height of a control point  
        public static int movesPerTurn;                       //number moves a subjects gets per turn
        public static int maxTurnsPerPeriod;                  //max number of turns a subject gets in a period if continuing to climb       
        public static bool showFullCircle;                    //show the entire landscape after the period is over  
        public static int maxRoundsPerPeriod;                 //max number of rounds each period if group improves

        public static Period[] periodList;
        public static PeriodGroup[] periodGroups;             //group or induvidal playing a game , period
        public static MoveDirections[,] moveDirectionsList;   //period,moves

        public static bool drawFullCircle = false;
        public static bool showProgramResults = false;
        public static bool drawProgramResults = false;

        public static string gamePhase = "";                   //submit or results

        public static float dataUnit;                          //point to scale conversion

        public static int showProgramResultsWidth = 1917;
        public static int hideProgramResultsWidth = 1325;

        public static int timeRemaining = 0;                  //time remaining in game phase         
        public static int periodLength = 0;                   //period length in seconds
        public static int readyToGoOnLength = 0;              //ready to go on length in seconds
        
        public static void start()
        {

            sfile = Application.StartupPath + @"\client.ini";

            commandLineArgs = Environment.GetCommandLineArgs();

            if (commandLineArgs.Length>1)
            {
                INI.writeINI(sfile, "Settings", "ip", commandLineArgs[1]);
            }                       

            myIPAddress = INI.getINI(sfile, "Settings", "ip").ToString();
            myPortNumber = int.Parse(INI.getINI(sfile, "Settings", "port"));
            
            EventLog.AppEventLog_Init();
        }

        public static void takeMessage(List<string> sinstr)
        {

            try
            {
                string str = sinstr[0];

                string[] tempa = { "<SEP>" };
                string[] msgtokens = str.Split(tempa, StringSplitOptions.None);

                string id = msgtokens[0];
                string message = msgtokens[1];

                switch (id)
                {
                    case "SHOW_NAME":
                        takeShowName(message);
                        break;
                    case "RESET":
                        takeReset(message);
                        break;
                    case "END_EARLY":
                        takeEndEarly(message);
                        break;
                    case "FINISHED_INSTRUCTIONS":
                        takeFinishedInstructions(message);
                        break;
                    case "INVALID_CONNECTION":
                        takeInvalidConnection(message);
                        break;
                    case "BEGIN":                        
                        takeBegin(message);
                        break;
                    case "REQUEST_COMPUTER_NAME":
                        takeRequestComputerName(message);
                        break;
                    case "START_NEXT_PERIOD":
                        takeStartNextPeriod(message);
                        break;
                    case "01":
                        takePeriodResults(message);
                        break;
                    case "02":
                        
                        break;
                    case "03":
                        
                        break;
                    case "04":
                        
                        break;
                    case "05":
                        
                        break;
                    case "06":
                        
                        break;
                    case "07":
                        
                        break;
                    case "08":
                        
                        break;
                    case "09":
                        
                        break;
                    case "10":

                        break;
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeBegin(string str)
        {
            try
            {
                //initialize forms
                Frm1 = new frm1();
                Frm1.Show();

                string[] msgtokens = str.Split(';');
                int nextToken = 0;                

                //take parameters
                numberOfPlayers = int.Parse(msgtokens[nextToken++]);
                numberOfPeriods = int.Parse(msgtokens[nextToken++]);
                instructionX = int.Parse(msgtokens[nextToken++]);
                instructionY = int.Parse(msgtokens[nextToken++]);
                windowX = int.Parse(msgtokens[nextToken++]);
                windowY = int.Parse(msgtokens[nextToken++]);
                showInstructions = bool.Parse(msgtokens[nextToken++]);
                inumber = int.Parse(msgtokens[nextToken++]);
                testMode = bool.Parse(msgtokens[nextToken++]);

                groupSize = int.Parse(msgtokens[nextToken++]);                
                circlePointCount = int.Parse(msgtokens[nextToken++]);
                circleControlPointLow = int.Parse(msgtokens[nextToken++]);
                circleControlPointHigh = int.Parse(msgtokens[nextToken++]);
                minControlPointValue = int.Parse(msgtokens[nextToken++]);
                maxControlPointValue = int.Parse(msgtokens[nextToken++]);
                movesPerTurn = int.Parse(msgtokens[nextToken++]);
                maxTurnsPerPeriod = int.Parse(msgtokens[nextToken++]);                
                showFullCircle = bool.Parse(msgtokens[nextToken++]);
                maxRoundsPerPeriod = int.Parse(msgtokens[nextToken++]);

                periodLength = int.Parse(msgtokens[nextToken++]);
                readyToGoOnLength = int.Parse(msgtokens[nextToken++]);

                periodList = new Period[numberOfPeriods + 1];
                moveDirectionsList = new MoveDirections[numberOfPeriods + 1, movesPerTurn + 1];

                Frm1.tempDegree = 360 / (float)circlePointCount;              
                dataUnit = Common.Frm1.circleDataArea / (Common.maxControlPointValue - Common.minControlPointValue);
                Frm1.zeroLine = Common.returnLocation(0, 0);

                if(Common.showInstructions)
                {
                    currentPeriod = 0;
                }
                else
                {
                    currentPeriod = 1;
                }                

                periodList[currentPeriod] = new Period();
                periodList[currentPeriod].fromString(ref msgtokens, ref nextToken);

                float degreeCounter = -90;

                for (int j = 1; j <= Common.circlePointCount; j++)
                {
                    periodList[currentPeriod].circlePoints[j].setup(degreeCounter);
                    degreeCounter += Frm1.tempDegree;
                }

                periodGroups = new PeriodGroup[Common.numberOfPeriods + 1];
                periodGroups[currentPeriod] = new PeriodGroup();
                periodGroups[currentPeriod].groupAssignment = msgtokens[nextToken++];

                setupPeriodGroups(ref msgtokens, ref nextToken);

                Common.FrmClient.Hide();
                Frm1.Text = "Client " + inumber;

                for (int i = 1; i <= numberOfPlayers; i++)
                {
                    playerMessageCounter[i] = 1;
                }             
                          
                FrmClient.SC.sendMessage("COMPUTER_NAME", Environment.MachineName + ";");

                if (showInstructions)
                {
                    FrmInstructions = new frmInstructions();
                    FrmInstructions.Show();
                    FrmInstructions.Location = new System.Drawing.Point(instructionX, instructionY);

                    Frm1.Location = new System.Drawing.Point(windowX, windowY);
                    showProgramResults = false;
                    drawFullCircle = true;
                    drawProgramResults = true;                   

                    Frm1.selectionTurn = 0;
                    Frm1.selectionMove = 0;
                    Frm1.selectionIndex = 0;
                    Frm1.selectionRound = 0;

                    Common.Frm1.Width = hideProgramResultsWidth;
                }
                else
                {
                                        
                }

                if(testMode)
                {
                    Frm1.timer2.Interval = Rand.rand(1500, 500);
                    Frm1.timer2.Enabled = true;

                    FrmTestMode = new frmTestMode();
                    FrmTestMode.Show();
                }

                Common.Frm1.dgMoves.RowCount = movesPerTurn;
                for(int i=1;i<=movesPerTurn;i++)
                {
                    Common.Frm1.dgMoves[0, i - 1].Value = i.ToString();
                }

                setupPeriod();
               
                Common.Frm1.timer1.Enabled = true;

                Common.Frm1.lblDistance.Text = Common.Frm1.lblDistance.Text.Replace("NNNN", (circlePointCount / 2).ToString());
                Common.Frm1.lblMaxTurns.Text = Common.Frm1.lblMaxTurns.Text.Replace("N1", maxTurnsPerPeriod.ToString());
                Common.Frm1.lblMaxTurns.Text = Common.Frm1.lblMaxTurns.Text.Replace("N2", movesPerTurn.ToString());
                                
                Common.Frm1.txtTotalEarnings.Text = "0";
                Common.Frm1.txtTimeRemaining.Text = "-";

                if(showInstructions)
                {
                    Common.Frm1.cmdSubmit.Visible = false;
                }

                EventLog.appEventLog_Write("Client: " + inumber);               
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeShowName(string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                FrmNames = new frmNames();
                FrmNames.Show();

                FrmNames.lblEarnings.Text = "Your Earnings Are: " +  msgtokens[nextToken++];

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeEndEarly(string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                numberOfPeriods = int.Parse(msgtokens[nextToken++]);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeFinishedInstructions(string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                Common.currentPeriod = int.Parse(msgtokens[nextToken++]);
               
                FrmInstructions.Close();
                showInstructions = false;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeRequestComputerName(string str)
        {
            try
            {
                str = "a;";
                // Thread.Sleep(200);
                //Program.frmClient.SC.sendMessage("COMPUTER_NAME","a");
                FrmClient.SC.sendMessage("COMPUTER_NAME", Environment.MachineName + ";");
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeReset(string str)
        {
            try
            {
                //string[] msgtokens = str.Split(';');
                //int nextToken = 0;

                closeClient();
                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }


        public static void closeClient()
        {
            try
            {

               // FrmClient.Timer1.Enabled = false;
                clientClosing = true;
                if(Frm1 != null) Frm1.timer1.Enabled = false;
                

                FrmClient.bwSocket.CancelAsync();
                FrmClient.SC.close();

                //.Close()
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                FrmClient.Close();
            }
        }

        public static void takeInvalidConnection(string str)
        {
            try
            {
               List<string> list = new List<string>();

               FrmClient.SC_ConnectionError(null, new ListEventArgs(list));
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeClientMessage(string message)
        {
            try
            {
                string[] msgtokens = message.Split(';');
                int nextToken = 0;

                string tempId = msgtokens[nextToken++];
                string tempMessage = msgtokens[nextToken++];                

                playerMessageCounter[int.Parse(tempId)] = int.Parse(tempMessage)+1;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static int returnDistance(int x1,int y1,int x2, int y2)
        {
            return Convert.ToInt32(Math.Round(Math.Sqrt(Math.Pow((x2 - x1),2) + Math.Pow((y2 - y1),2))));
        }

        public static string timeConversion(int sec)
        {
            try
            {
                // appEventLog_Write("time conversion :" & sec)
                return((sec / 60).ToString("D2")+ ":" + (sec % 60).ToString("D2"));
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error timeConversion:", ex);
                return  "";
            }

        }

        public static void takeStartNextPeriod(string message)
        {
            try
            {
                if(Common.FrmInstructions != null  && Common.FrmInstructions.Visible)
                {
                    Common.FrmInstructions.Close();
                    Common.showInstructions = false;
                    Common.showProgramResults = false;

                    if (Common.currentPeriod == 0)
                    {
                        clearProgram();
                    }
                }

                string[] msgtokens = message.Split(';');
                int nextToken = 0;

                currentPeriod = int.Parse(msgtokens[nextToken++]);                           

                earnings = double.Parse(msgtokens[nextToken++]);

                showInstructions = bool.Parse(msgtokens[nextToken++]);

                periodList[currentPeriod] = new Period();
                periodList[currentPeriod].fromString(ref msgtokens, ref nextToken);

                periodGroups[currentPeriod] = new PeriodGroup();
                periodGroups[currentPeriod].groupAssignment = msgtokens[nextToken++];
                setupPeriodGroups(ref msgtokens, ref nextToken);

                float degreeCounter = -90;

                for (int j = 1; j <= Common.circlePointCount; j += 1)
                {
                    periodList[currentPeriod].circlePoints[j].setup(degreeCounter);
                    degreeCounter += Frm1.tempDegree;
                }

                setupPeriod();

                if(showInstructions)
                {
                    FrmInstructions = new frmInstructions();
                    FrmInstructions.Show();
                    FrmInstructions.Location = new System.Drawing.Point(instructionX, instructionY);

                    if (periodGroups[currentPeriod].groupAssignment == "Individual")
                    {
                        FrmInstructions.currentInstruction = 9;
                    }
                    else
                    {
                        FrmInstructions.currentInstruction = 7;
                    }
                   
                    FrmInstructions.nextInstruction();
                    FrmInstructions.cmdBack.Visible = false;
                    FrmInstructions.cmdNext.Visible = false;
                    FrmInstructions.cmdStart.Visible = false;
                }

                //submission timer
                if(currentPeriod > 1 && showInstructions == false)
                {
                    Common.timeRemaining = periodLength;
                    Common.Frm1.timer4.Enabled = true;
                    Common.Frm1.txtTimeRemaining.Text = Common.timeConversion(Common.timeRemaining);
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }               

        public static void takePeriodResults(string message)
        {
            try
            {
                string[] msgtokens = message.Split(';');
                int nextToken = 0;

                gamePhase = "results";

                Period p = periodList[Common.currentPeriod];

                Frm1.txtTotalEarnings.Text = string.Format("{0:0.00}", Math.Round(double.Parse(msgtokens[nextToken++]), 2));
                Frm1.txtPeriodEarnings.Text = string.Format("{0:0.00}",Math.Round(double.Parse(msgtokens[nextToken++]), 2));                

                periodGroups[currentPeriod].fromString(ref msgtokens,ref nextToken);

                for(int i=1;i<=movesPerTurn;i++)
                {
                    moveDirectionsList[currentPeriod, i] = new MoveDirections();
                    moveDirectionsList[currentPeriod, i].fromString(ref msgtokens, ref nextToken);
                }

                if (Common.currentPeriod > 0)
                {
                    Frm1.cmdReady.Visible = true;
                    Frm1.txtMessages.Text = "Press the 'Ready to Go On' button.";

                    if (showInstructions  && periodGroups[currentPeriod].groupAssignment != "Individual")
                    {                       
                        FrmInstructions.Location = new System.Drawing.Point(instructionX, instructionY);
                        FrmInstructions.currentInstruction = 8;
                        FrmInstructions.nextInstruction();
                        FrmInstructions.cmdBack.Visible = false;
                        FrmInstructions.cmdNext.Visible = false;
                        FrmInstructions.cmdStart.Visible = false;

                        string tempS = string.Format("{0:0.00}",
                                Math.Round(p.circlePoints[Common.periodGroups[Common.currentPeriod].startingLocation].value, 2));

                        FrmInstructions.RepRTBfield2("startValue", tempS);

                        tempS = string.Format("{0:0.00}",
                                Math.Round(p.circlePoints[Common.periodGroups[Common.currentPeriod].endingLocation].value, 2));

                        FrmInstructions.RepRTBfield2("endValue",tempS);
                    }
                }
                else
                {
                   
                }

                //fill in start row
                if (showProgramResults)
                {
                    Frm1.lblStartingValue.Text = Math.Round(p.circlePoints[periodGroups[currentPeriod].startingLocation].value, 2).ToString();
                    Frm1.lblEndingValue.Text = Math.Round(p.circlePoints[periodGroups[currentPeriod].endingLocation].value, 2).ToString();
                    Frm1.lblMaxValue.Text= string.Format("{0:0.00}", p.maxValue);

                    DataGridView dg = Frm1.dgResults;
                    dg.RowCount++;
                    dg[0, dg.RowCount - 1].Value = "";
                    dg[1, dg.RowCount - 1].Value = "";
                    dg[2, dg.RowCount - 1].Value = "";
                    dg[3, dg.RowCount - 1].Value = "";
                    dg[4, dg.RowCount - 1].Value = "Start";
                    dg[5, dg.RowCount - 1].Value = "";
                    dg[6, dg.RowCount - 1].Value = Math.Round(periodList[currentPeriod].circlePoints[periodGroups[currentPeriod].startingLocation].value, 2);

                    dg.Rows[0].DefaultCellStyle.ForeColor = Common.Frm1.startColor;
                    dg.Rows[0].DefaultCellStyle.SelectionForeColor = Common.Frm1.startColor;

                    //fill out result table
                    for (int j = 1; j <= periodGroups[currentPeriod].roundCount; j++)
                    {
                        for (int i = 1; i <= periodGroups[currentPeriod].periodGroupPlayerCount; i++)
                        {
                            
                            PeriodGroupPlayer pgp = periodGroups[currentPeriod].periodGroupPlayers[i];
                            //if(pgp.playerIndex == inumber)
                            //{
                            pgp.fillResultsTable(pgp.playerNumber,j);
                            // break;
                            //}
                        }                       
                    }

                    dg.RowCount++;
                    dg[0, dg.RowCount - 1].Value = "";
                    dg[1, dg.RowCount - 1].Value = "";
                    dg[2, dg.RowCount - 1].Value = "";
                    dg[3, dg.RowCount - 1].Value = "";
                    dg[4, dg.RowCount - 1].Value = "End";
                    dg[5, dg.RowCount - 1].Value = "";
                    dg[6, dg.RowCount - 1].Value = Math.Round(periodList[currentPeriod].circlePoints[periodGroups[currentPeriod].endingLocation].value, 2);

                    dg.Rows[dg.RowCount - 1].DefaultCellStyle.ForeColor = Common.Frm1.endColor;
                    dg.Rows[dg.RowCount - 1].DefaultCellStyle.SelectionForeColor = Common.Frm1.endColor;

                    Frm1.dgResults.Rows[0].Selected = true;
                }

                if(Frm1.gbGroups.Visible)
                {

                    Frm1.dgGroup.RowCount = periodGroups[currentPeriod].periodGroupPlayerCount * periodGroups[currentPeriod].roundCount;

                    int counter = 0;
                    for(int j=1;j<= periodGroups[currentPeriod].roundCount;j++)
                    {
                        for (int i=1;i <= periodGroups[currentPeriod].periodGroupPlayerCount;i++)
                        {
                            //Period p = periodList[Common.currentPeriod];

                            Frm1.dgGroup[0, counter].Value = j;
                            Frm1.dgGroup[1, counter].Value = i;

                            int pn = periodGroups[currentPeriod].periodGroupPlayers[i].playerNumber;
                            Frm1.dgGroup[2, counter].Value = "P" + pn;

                            if (pn == inumber)
                            {
                                Frm1.dgGroup[2, counter].Value = Frm1.dgGroup[2, counter].Value.ToString() + " (You)";
                            }

                            string s = string.Format("{0:0.00}",
                                                     Math.Round(p.circlePoints[periodGroups[currentPeriod].periodGroupPlayers[i].periodGroupPlayerRounds[j].startingLocation].value, 2));
                            string e = string.Format("{0:0.00}",
                                                    Math.Round(p.circlePoints[periodGroups[currentPeriod].periodGroupPlayers[i].periodGroupPlayerRounds[j].endingLocation].value, 2));
                        
                            Frm1.dgGroup[3, counter].Value = s;
                            Frm1.dgGroup[4, counter].Value = e;
                            Frm1.dgGroup[5, counter].Value = periodGroups[currentPeriod].periodGroupPlayers[i].periodGroupPlayerRounds[j].turnCount;                            

                            counter ++;
                        }
                    }                   

                    
                }

                gamePhase = "results";

                Frm1.selectionPoint = periodList[currentPeriod].circlePoints[periodGroups[currentPeriod].startingLocation].location;
                Frm1.selectionColor = Frm1.startColor;
               
                Frm1.selectionStartCirclePoint = periodGroups[currentPeriod].startingLocation;
                Frm1.selectionEndCirclePoint = periodGroups[currentPeriod].startingLocation;

                if(Common.showProgramResults)
                {
                    Frm1.selectionTurn = 0;
                    Frm1.selectionMove = 0;
                    Frm1.selectionIndex = 0;
                    Frm1.selectionRound = 0;

                    Frm1.Width = showProgramResultsWidth;
                }
                else
                {
                    Frm1.selectionTurn = 1000;
                    Frm1.selectionMove = 1000;
                    Frm1.selectionIndex = 1000;
                    Frm1.selectionRound = 1000;

                    Frm1.Width = hideProgramResultsWidth;
                }               

                Common.Frm1.gpProgramResults.Visible = showProgramResults;

                //ready to go on timer
                if (currentPeriod > 1  && showInstructions == false)
                {
                    Common.timeRemaining = readyToGoOnLength;
                    Common.Frm1.timer4.Enabled = true;
                    Common.Frm1.txtTimeRemaining.Text = Common.timeConversion(Common.timeRemaining);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void setupPeriodGroups(ref string[] msgtokens, ref int nextToken)
        {
            try
            {
                if (periodGroups[currentPeriod].groupAssignment == "Individual")
                {
                    Common.Frm1.gbGroups.Visible = false;
                }
                else
                {                    
                    DataGridView dg = Common.Frm1.dgGroup;

                    Common.Frm1.gbGroups.Visible = true;

                    dg.RowCount = int.Parse(msgtokens[nextToken++]);

                    for (int i = 1; i <= dg.RowCount; i++)
                    {
                        dg[0, i - 1].Value = 1;
                        dg[1, i - 1].Value = i;

                        int p = int.Parse(msgtokens[nextToken++]);

                        dg[2, i - 1].Value = "P" + p;

                        if (p == inumber)
                        {
                            dg[2, i - 1].Value = dg[2, i - 1].Value.ToString() +  " (You)";
                        }

                        dg[3, i - 1].Value = "";
                        dg[4, i - 1].Value = "";
                        dg[5, i - 1].Value = "";

                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void setupPeriod()
        {
            try
            {
                if (!Common.showInstructions)
                {
                    if (showFullCircle)
                    {
                        showProgramResults = true;
                        drawFullCircle = true;
                        drawProgramResults = true;

                        Frm1.selectionTurn = 0;
                        Frm1.selectionMove = 0;
                        Frm1.selectionIndex = 0;
                        Frm1.selectionRound = 0;
                    }
                    else
                    {
                        showProgramResults = false;
                        drawFullCircle = false;
                        drawProgramResults = true;

                        Frm1.selectionTurn = 1000;
                        Frm1.selectionMove = 1000;
                        Frm1.selectionIndex = 1000;
                        Frm1.selectionRound = 1000;
                    }

                    if (!showProgramResults)
                    {
                        Common.Frm1.Width = hideProgramResultsWidth;
                    }
                    else
                    {
                        Common.Frm1.Width = showProgramResultsWidth;
                    }

                    Common.Frm1.txtMessages.Text = "Enter your program then press the 'Submit' button.";
                }               

                gamePhase = "submit";                

                Common.Frm1.cmdSubmit.Visible = true;
                Common.Frm1.cmdReady.Visible = false;
                Common.Frm1.refreshScreen();
                
                Common.Frm1.lblEndingValue.Text = "-";
                Common.Frm1.lblStartingValue.Text = "-";
                Common.Frm1.lblMaxValue.Text = "-";

                resetScreen();

                Common.Frm1.dgMoves.CurrentCell.Selected = false;                

                Common.Frm1.txtPeriod.Text = Common.currentPeriod.ToString();
                Common.Frm1.txtPeriodEarnings.Text = "-";

                if(periodGroups[Common.currentPeriod].groupAssignment=="Individual")
                {
                    Common.Frm1.dgResults.Columns[0].Visible = false;
                }
                else
                {
                    Common.Frm1.dgResults.Columns[0].Visible = true;
                }

                Common.Frm1.gpProgramResults.Visible = showProgramResults;
                Common.Frm1.dgMoves.CurrentCell.Selected = false;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void resetScreen()
        {
            try
            {
                if (Common.testMode && Common.Frm1.timer2.Enabled)
                {
                    clearProgram();
                }

                Frm1.dgResults.RowCount = 0;

                Frm1.dgMoves.Columns[1].ReadOnly = false;
                Frm1.dgMoves.Columns[2].ReadOnly = false;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void clearProgram()
        {
            try
            {
                DataGridView dg = Common.Frm1.dgMoves;
                for (int i = 1; i <= dg.RowCount; i++)
                {
                    dg[1, i - 1].Value = null;
                    dg[2, i - 1].Value = "";
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static bool validateNumber(string str,bool allowDecimal,bool allowNegative)
        {
            try
            {
                if(allowDecimal)
                {
                    if (!double.TryParse(str, out double d))
                        return false;
                    else if (!allowNegative && d < 0)
                        return false;
                        

                }
                else
                {                   
                    if (!int.TryParse(str, out int i))
                        return false;
                    else if (!allowNegative && i < 0)
                        return false;
                }                

                return true;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return false;
            }
        }

        public static PointF returnLocation(float tempD,float tempV)
        {
            try
            {               

                float tempDistance = Common.Frm1.circleRadius + Common.dataUnit * (tempV - (float)Common.minControlPointValue);

                return(new PointF(tempDistance * (float)Math.Cos(tempD * (Math.PI / 180)),
                                  tempDistance * (float)Math.Sin(tempD * (Math.PI / 180)))
                      );

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return (new PointF(0, 0));
            }
        }
    }
}
