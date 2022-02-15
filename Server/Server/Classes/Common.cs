using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using System.Drawing;
using System.Drawing.Drawing2D;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Server
{
    public static class Common
    {
        public static string sfile;                               //location of server.ini       
        public static int clientCount = 0;                        //number of connected clients
        public static player[] playerlist = new player[1000];     //list of connected clients

        //forms
        public static frmMain FrmServer;
        public static frmSetup1 FrmSetup1;
        public static frmSetup2 FrmSetup2;
        public static frmSetup3 FrmSetup3;
        public static frmSetup4 FrmSetup4;
        public static frmSetup5 FrmSetup5;
        public static frmReplay FrmReplay;

        //data files
        public static StreamWriter roundDf;                  //summary data file
        public static StreamWriter eventsDf;                   //events data file
        public static StreamWriter periodsDf;                  //period data file
        public static StreamWriter groupDf;                   //events data file

        //global parameters
        public static int numberOfPlayers;                     //number of players needed
        public static int numberOfPeriods;                     //number of periods in the experiment  
        public static int portNumber;                          //port for socket communication 
        public static int instructionX;                        //x location of intstruction window
        public static int instructionY;                        //y location of intstruction window
        public static int windowX;                             //x location of main window
        public static int windowY;                             //y location of main window
        public static bool showInstructions;                   //show instructions before experiment starts
        public static bool testMode;                           //turn on auto test system
        public static int checkin=0;                           //global counter for subject actions 

        public static Period[] periodList;                     //list of period information and control

        public static ValueDistribution[] valueDistributions;  //list of value distribution blocks
        public static int valueDistributionCount;              //

        public static int currentPeriod = 0;                   //current period      
        public static bool closing = false;                    //true once the software is reset

        public static int groupSize;                          //number of subjects in a group        
        //public static int numberOfGroups;                     //total number of groups 
                          
        public static int circlePointCount;                   //number of points around the circle
        public static int circleControlPointLow;              //low jump range of control points around the circle determining shape
        public static int circleControlPointHigh;             //high jump range of control points around the circle determining shape
        public static int minControlPointValue;               //lowest height of control point
        public static int maxControlPointValue;               //max height of a control point  
        public static int movesPerTurn;                       //number moves a subjects gets per turn
        public static int maxTurnsPerPeriod;                  //max number of turns a subject gets in a period if continuing to climb        
        public static bool showFullCircle;                    //show the entire landscape after the period is over  
        public static int maxRoundsPerPeriod;                 //max number of rounds each period if group improves

        public static int timeRemaining = 0;                  //time remaining in game phase      
        public static int periodLength=0;                     //period length in secionds
        public static int readyToGoOnLength=0;                //ready to go on lenght in seconds

        //currency
        public static System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

        public static float dataUnit;                   //point to scale conversion

        public static void start()
        {
            sfile = Application.StartupPath + @"\server.ini";
            EventLog.AppEventLog_Init();

            loadParameters();

            culture.NumberFormat.CurrencyNegativePattern = 1;
        }

        public static void loadParameters()
        {
            numberOfPlayers =int.Parse(INI.getINI(sfile, "gameSettings", "numberOfPlayers"));
            numberOfPeriods = int.Parse(INI.getINI(sfile, "gameSettings", "numberOfPeriods"));
            portNumber = int.Parse(INI.getINI(sfile, "gameSettings", "port"));
            instructionX = int.Parse(INI.getINI(sfile, "gameSettings", "instructionX"));
            instructionY = int.Parse(INI.getINI(sfile, "gameSettings", "instructionY"));
            windowX = int.Parse(INI.getINI(sfile, "gameSettings", "windowX"));
            windowY = int.Parse(INI.getINI(sfile, "gameSettings", "windowY"));            

            showInstructions =bool.Parse(INI.getINI(sfile, "gameSettings", "showInstructions"));
            testMode = bool.Parse(INI.getINI(sfile, "gameSettings", "testMode"));
            showFullCircle = bool.Parse(INI.getINI(sfile, "gameSettings", "showFullCircle"));

            groupSize = int.Parse(INI.getINI(sfile, "gameSettings", "groupSize"));            
            circlePointCount = int.Parse(INI.getINI(sfile, "gameSettings", "circlePointCount"));
            circleControlPointLow = int.Parse(INI.getINI(sfile, "gameSettings", "circleControlPointLow"));
            circleControlPointHigh = int.Parse(INI.getINI(sfile, "gameSettings", "circleControlPointHigh"));
            minControlPointValue = int.Parse(INI.getINI(sfile, "gameSettings", "minControlPointValue"));
            maxControlPointValue = int.Parse(INI.getINI(sfile, "gameSettings", "maxControlPointValue"));
            movesPerTurn = int.Parse(INI.getINI(sfile, "gameSettings", "movesPerTurn"));
            maxTurnsPerPeriod = int.Parse(INI.getINI(sfile, "gameSettings", "maxTurnsPerPeriod"));            
            maxRoundsPerPeriod = int.Parse(INI.getINI(sfile, "gameSettings", "maxRoundsPerPeriod"));
                        
            if (!int.TryParse(INI.getINI(sfile, "gameSettings", "periodLength"), out periodLength)) periodLength = 15;
            if (!int.TryParse(INI.getINI(sfile, "gameSettings", "readyToGoOnLength"), out readyToGoOnLength)) readyToGoOnLength = 60;
        }

        //process incoming message from a client
        public static void takeMessage(List<string> sinstr)
        {
            try
            {

                int index = int.Parse(sinstr[0]);
                string str = sinstr[1];

                string[] tempa = new string[4];
                tempa[1] = "<SEP>";
                tempa[2] = "<EOF>";
                string[] msgtokens = str.Split(tempa, 3, StringSplitOptions.None);

                string id = msgtokens[0];
                string message = msgtokens[1];

                switch (id)
                {
                    case "COMPUTER_NAME":
                        takeRemoteComputerName(index, message);
                        break;
                    case "SUBJECT_NAME":
                        takeName(index, message);
                        break;
                    case "FINSHED_INSTRUCTIONS":
                        takeFinishedInstructions(index, message);
                        break;
                    case "INSTRUCTION_PAGE":
                        takeInstructionPage(index, message);
                        break;
                    case "CLIENT_ERROR":
                        takeClientError(index, message);
                        break;
                    case "01":
                        takeMoveDirections(index, message);
                        break;
                    case "02":
                        takeReadyToGoOn(index, message);
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

        static void takeName(int index, string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                playerlist[index].name = msgtokens[nextToken++].Replace(",", "<COMMA>");
                playerlist[index].studentID = msgtokens[nextToken++].Replace(",", "<COMMA>");

                FrmServer.dgMain[1, index - 1].Value = playerlist[index].name;

                checkin += 1;

                if (checkin == numberOfPlayers)
                {
                    checkin = 0;

                    //summary price data

                    roundDf.WriteLine(",");
                    roundDf.WriteLine("Number,Name,Student ID,Earnings");

                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                        string outstr = "";

                        outstr = playerlist[i].inumber + ",";
                        outstr += playerlist[i].name + ",";
                        outstr += playerlist[i].studentID + ",";
                        outstr += String.Format(culture, "{0:C}", playerlist[i].earnings / 100) + ",";

                        roundDf.WriteLine(outstr);
                    }

                    roundDf.Close();
                    //eventsDf.Close();
                    groupDf.Close();
                    periodsDf.Close();
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        static void takeFinishedInstructions(int index, string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                //int nextToken = 0;

                checkin += 1;

                FrmServer.dgMain[2, index - 1].Value = "Waiting";

                if (checkin == numberOfPlayers)
                {
                    MessageBox.Show("Start Experiment?", "Start", MessageBoxButtons.OK, MessageBoxIcon.Question);

                    checkin = 0;
                    showInstructions = false;

                    Common.currentPeriod = 1;

                    setupNextPeriod();
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        static void takeInstructionPage(int index, string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                int tempPage = int.Parse(msgtokens[nextToken++]);

                Common.FrmServer.dgMain[2, index - 1].Value = "Page " + tempPage;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        static void takeClientError(int index,string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                EventLog.appEventLog_Write("client " + index.ToString() + " error :" + msgtokens[nextToken]);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        static void takeRemoteComputerName(int index, string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                playerlist[index].sp.remoteComputerName = msgtokens[nextToken++];
                FrmServer.dgMain[1, index - 1].Value = playerlist[index].sp.remoteComputerName;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        static void takeMoveDirections(int index, string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                if (playerlist[index].movesSubmitted[Common.currentPeriod]) return;
                playerlist[index].movesSubmitted[Common.currentPeriod] = true;

                for (int i=1;i<=Common.movesPerTurn;i++)
                {
                    playerlist[index].moveDirectionsList[currentPeriod, i] = new MoveDirections();

                    string tempDirection = "";
                    if (msgtokens[nextToken++].Contains("Counter"))
                        tempDirection = "Counter Clockwise";
                    else
                        tempDirection = "Clockwise";

                    playerlist[index].moveDirectionsList[currentPeriod, i].direction = tempDirection;

                    if (int.TryParse(msgtokens[nextToken++], out int v))
                    {
                        if (v > 0 && v <= Common.circlePointCount / 2)
                            playerlist[index].moveDirectionsList[currentPeriod, i].distance = v;
                        else
                            playerlist[index].moveDirectionsList[currentPeriod, i].distance = 0;
                    }
                    else
                    {
                        playerlist[index].moveDirectionsList[currentPeriod, i].distance = 0;
                    }                    
                }

                playerlist[index].autoSubmit[Common.currentPeriod] = bool.Parse(msgtokens[nextToken++]);

                if(showInstructions)
                {
                    periodList[Common.currentPeriod].doPeriod(index);
                    playerlist[index].sendPeriodResults();
                }
                else
                {
                    Common.FrmServer.dgMain[2, index - 1].Value = "Waiting";

                    //check everone submitted
                    int counter = 0;

                    for(int i=1;i<=Common.numberOfPlayers;i++)
                    {
                        if (playerlist[i].movesSubmitted[Common.currentPeriod]) counter++;
                    }

                    if(counter == Common.numberOfPlayers)
                    {
                        Common.checkin = 0;                       

                        if (Common.currentPeriod > 1)
                        {
                            Common.FrmServer.Timer2.Enabled = true;
                            Common.timeRemaining = readyToGoOnLength;
                            Common.FrmServer.lblTimeRemaining.Text = Common.timeConversion(Common.timeRemaining);
                        }

                        periodList[Common.currentPeriod].doPeriod(-1);

                        Common.FrmServer.refreshStatusDisplay();

                        for(int i=1;i<=Common.numberOfPlayers;i++)
                        {
                            playerlist[i].sendPeriodResults();
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeReadyToGoOn(int index, string str)
        {
            try
            {
                //checkin++;

                if (playerlist[index].readyToGoOnPressed[Common.currentPeriod]) return;
                playerlist[index].readyToGoOnPressed[Common.currentPeriod] = true;

                Common.FrmServer.dgMain[2, index - 1].Value = "Waiting";

                int counter = 0;

                for (int i = 1; i <= Common.numberOfPlayers; i++)
                {
                    if (playerlist[i].readyToGoOnPressed[Common.currentPeriod]) counter++;
                }

                if (counter == numberOfPlayers)
                {
                    checkin = 0;
                    Common.FrmServer.Timer2.Enabled = false;

                    if (currentPeriod == numberOfPeriods)
                    {
                       //end game
                       for(int i=1;i<=Common.numberOfPlayers;i++)
                       {
                          playerlist[i].sendShowName();
                       }

                       Cursor.Current = Cursors.WaitCursor;
                       Common.writePeriodJSON();
                       Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        Common.currentPeriod++;
                        setupNextPeriod();
                    }                   
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void setupNextPeriod()
        {
            try
            {
                bool b = false;
                if (playerlist[1].groupAssignments[Common.currentPeriod - 1] == "Individual" &&
                    playerlist[1].groupAssignments[Common.currentPeriod] != "Individual")
                {
                    MessageBox.Show("Start the Group Phase?", "Groups", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    calcGroups();
                    b = true;                    
                }
                else if (playerlist[1].groupAssignments[Common.currentPeriod] == "Individual" &&
                         playerlist[1].groupAssignments[Common.currentPeriod -1] != "Individual")
                {
                    MessageBox.Show("Start the Individual Phase?", "Individual", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    b = true;
                }

                for (int i = 1; i <= Common.numberOfPlayers; i++)
                {
                    Common.playerlist[i].setupGroupNumber();
                }

                Common.periodList[Common.currentPeriod].setupPeriod();

                if(b)
                {
                    if (INI.getINI(Common.sfile, "gameSettings", "showInstructions") == "True")
                        Common.showInstructions = true;
                }
                else
                {
                    //time remaining display
                    if (Common.currentPeriod > 1)
                    {
                        Common.FrmServer.Timer2.Enabled = true;
                        Common.timeRemaining = periodLength;
                        Common.FrmServer.lblTimeRemaining.Text = Common.timeConversion(Common.timeRemaining);
                    }
                }

                for (int i = 1; i <= Common.numberOfPlayers; i++)
                {
                    playerlist[i].sendStartNextPeriod();
                }

                if(b)
                {
                    Common.showInstructions = false;
                }
                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void startNextPeriod()
        {
            try
            {
                //record summary data
                for (int i = 1; i <= numberOfPlayers; i++)
                {
                    playerlist[i].writeSummaryData();
                }

                //update displayed earnings
                
                for (int i = 1; i <= numberOfPlayers; i++)
                {
                    Common.FrmServer.dgMain[3, i - 1].Value = String.Format(culture, "{0:C}", playerlist[i].earnings / 100);
                }
                

                if (currentPeriod == numberOfPeriods)
                {

                    //end game
                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                        playerlist[i].sendShowName();
                    }
                }
                else
                {
                    if (!Common.showInstructions) currentPeriod += 1;                    

                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                        playerlist[i].sendStartNextPeriod();
                    }                    
                }
                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void calcGroups()
        {
            try
            {
                int sortedCount = 0;
                int randomCount = 0;
                int firstSortedIndex=0;
                for (int i = 1; i <= numberOfPlayers; i++)
                {
                    playerlist[i].calcTotalIndividualScore();
                    playerlist[i].groupNumberRandom = 0;

                    if(playerlist[i].groupAssignments[Common.currentPeriod] == "Sorted")
                    {
                        sortedCount++;
                        if (firstSortedIndex == 0) firstSortedIndex = i;
                    }
                    else
                    {
                        randomCount++;
                    }
                }

                //sorted groups
                int group = 1;
                int counter = 0;

                if (firstSortedIndex != 0)
                {
                    int[] sList = new int[sortedCount + 1];

                    sList[1] = firstSortedIndex;

                    for (int i = firstSortedIndex + 1; i <= numberOfPlayers; i++)
                    {
                        if (playerlist[i].groupAssignments[Common.currentPeriod] == "Sorted")
                        {
                            int[] spots = new int[Common.numberOfPlayers + 1];
                            int spotCount = 0;

                            for (int j = 1; j < i; j++)
                            {
                                if (playerlist[i].totalInduvidualScore > playerlist[sList[j]].totalInduvidualScore)
                                {
                                    spots[++spotCount] = j;
                                    break;
                                }
                                else if (playerlist[i].totalInduvidualScore == playerlist[sList[j]].totalInduvidualScore)
                                {
                                    spots[++spotCount] = j;

                                    if (sList[j + 1] == 0)
                                    {
                                        spots[++spotCount] = j + 1;
                                        break;
                                    }
                                    else if (playerlist[sList[j]].totalInduvidualScore != playerlist[sList[j + 1]].totalInduvidualScore)
                                    {
                                        spots[++spotCount] = j + 1;
                                        break;
                                    }
                                }
                                else
                                {
                                    //add spot for random broken ties

                                }

                            }

                            //not better than any in list
                            if (spotCount == 0) spots[++spotCount] = i;

                            int spot = spots[Rand.rand(spotCount, 1)];

                            for (int j = sortedCount; j > spot; j--)
                            {
                                sList[j] = sList[j - 1];
                            }

                            sList[spot] = i;
                        }
                    }                                    

                    for (int i = 1; i <= sortedCount; i++)
                    {
                        playerlist[sList[i]].groupNumberSorted = group;

                        counter++;
                        if (counter == Common.groupSize || i == sortedCount)
                        {
                            group++;
                            counter = 0;
                        }
                    }
                }

                //random groupings
                counter = 0;
                int counter2 = 0;
                while(counter2 < randomCount)
                {
                    int p = Rand.rand(Common.numberOfPlayers, 1);

                    if(playerlist[p].groupAssignments[Common.currentPeriod] == "Random" &&
                       playerlist[p].groupNumberRandom == 0 )
                    {
                        playerlist[p].groupNumberRandom = group;
                        counter2++;

                        counter++;
                        if (counter == Common.groupSize)
                        {
                            group++;
                            counter = 0;
                        }
                    }
                }              

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static int returnDistance(int x1, int y1, int x2, int y2)
        {
            return Convert.ToInt32(Math.Round(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2))));
        }

        public static string timeConversion(int sec)
        {
            try
            {
                // appEventLog_Write("time conversion :" & sec)
                return ((sec / 60).ToString("D2") + ":" + (sec % 60).ToString("D2"));
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error timeConversion:", ex);
                return "";
            }

        }

        public static void writeEvent(int p,string eventName,string eventInfo)
        {
            try
            {
                // "Period,Second,MilliSecond,Player,EventName,EventInfo,"

                if (Common.showInstructions) return;

                string outstr = "";

                outstr = currentPeriod.ToString() + ",";                
                outstr += p.ToString() + ",";
                outstr += eventName + ",";
                outstr += eventInfo;
                
                if(!Common.closing) Common.eventsDf.WriteLine(outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);                
            }
        }

        public static string dZero(double s)
        {
            try
            {
                return (string.Format("{0:0.00}", s));
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return "0.00";
            }
        }

        public static PointF returnLocation(float tempD, float tempV)
        {
            try
            {

                float tempDistance = Common.FrmServer.circleRadius + Common.dataUnit * (tempV - (float)Common.minControlPointValue);

                return (new PointF(tempDistance * (float)Math.Cos(tempD * (Math.PI / 180)),
                                  tempDistance * (float)Math.Sin(tempD * (Math.PI / 180)))
                      );

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return (new PointF(0, 0));
            }
        }

        public static void writePeriodJSON()
        {
            try
            {
                JObject jo = new JObject();

                for(int i=1;i<=Common.numberOfPeriods;i++)
                {
                    jo.Add(periodList[i].getJson(i));
                }

                Common.periodsDf.WriteLine(jo.ToString());
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void setupCirclePointInstructions(ref CirclePoint[] circlePoints,
                                                        ref double maxValue,
                                                        ref int maxValueLocationCount,
                                                        ref int[] maxValueLocations,
                                                        int periodNumber)
        {
            try
            {
                maxValue = -10000;
                maxValueLocationCount = 0;

                //initialze all circle points
                circlePoints = new CirclePoint[Common.circlePointCount + 1];

                int controlPointCount = int.Parse(INI.getINI(Common.sfile, "instructions", "controlPointCount"));

                for(int i=1;i<=controlPointCount;i++)
                {
                    string[] msgtokens = INI.getINI(Common.sfile, "instructions", i.ToString()).Split(';');
                    int l = int.Parse(msgtokens[0]);
                    float v = float.Parse(msgtokens[1]);

                    circlePoints[l] = new CirclePoint();
                    circlePoints[l].value = v;
                    circlePoints[l].controlPoint = true;

                    //store locations that have the highest value
                    if (maxValue < circlePoints[l].value)
                    {
                        maxValue = circlePoints[l].value;
                        maxValueLocationCount = 1;
                        maxValueLocations[maxValueLocationCount] = l;
                    }
                    else if (maxValue == circlePoints[l].value)
                    {
                        maxValueLocations[++maxValueLocationCount] = l;
                    }
                }

                setupCirclePoints2(ref circlePoints, ref maxValue, ref maxValueLocationCount, ref maxValueLocations, periodNumber);

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void setupCirclePoints(ref CirclePoint[] circlePoints,
                                             ref double maxValue,
                                             ref int maxValueLocationCount,
                                             ref int[] maxValueLocations,
                                             int periodNumber)
        {
            try
            {
                //initialze all circle points
                circlePoints = new CirclePoint[Common.circlePointCount + 1];

                int startIndex = Rand.rand(Common.circlePointCount, 1);
                startIndex = 1965;
                int indexCounter = startIndex;
                bool goneOverTheTop = false;

                maxValue = -10000;
                maxValueLocationCount = 0;

                //set control points
                bool go = true;

                //for (int i = 1; i <= Common.circlePointCount; i++)
                //{
                //    if (circlePoints[i] != null)
                //    {
                //        int a = 1;
                //    }
                //}

                while (go)
                {
                    circlePoints[indexCounter] = new CirclePoint();
                    //circlePoints[indexCounter].value = Rand.rand(Common.maxControlPointValue, Common.minControlPointValue);
                    int distribution1 = Rand.rand(100, 1);
                    int distribution2 = 0;
                    int distribution3 = 0;

                    for (int i = 1; i <= Common.valueDistributionCount; i++)
                    {
                        if (distribution1 >= Common.valueDistributions[i].drawStart &&
                           distribution1 <= Common.valueDistributions[i].drawEnd)
                        {
                            distribution3 = Common.valueDistributions[i].valueRangeEnd;
                            distribution2 = Common.valueDistributions[i].valueRangeStart;
                            break;
                        }
                    }

                    circlePoints[indexCounter].value = Rand.rand(distribution3, distribution2);

                    circlePoints[indexCounter].controlPoint = true;

                    //store locations that have the highest value
                    if (maxValue < circlePoints[indexCounter].value)
                    {
                        maxValue = circlePoints[indexCounter].value;
                        maxValueLocationCount = 1;
                        maxValueLocations[maxValueLocationCount] = indexCounter;
                    }
                    else if (maxValue == circlePoints[indexCounter].value)
                    {
                        maxValueLocations[++maxValueLocationCount] = indexCounter;
                    }

                    indexCounter += Rand.rand(Common.circleControlPointHigh, Common.circleControlPointLow);

                    //if(indexCounter>Common.circleControlPointHigh || indexCounter<Common.circleControlPointLow)
                    //{
                    //    int a = 1;
                    //}

                    if (indexCounter > Common.circlePointCount && !goneOverTheTop)
                    {
                        goneOverTheTop = true;
                        indexCounter -= Common.circlePointCount;
                    }

                    if (goneOverTheTop && indexCounter >= startIndex)
                    {
                        go = false;
                    }
                }

                //check for fail
                int testIndex = -1;
                for (int i = 1; i <= Common.circlePointCount; i++)
                {
                    if (circlePoints[i] != null)
                    {
                        if (circlePoints[i].controlPoint)
                        {
                            if (testIndex == -1)
                            {
                                testIndex = i;
                            }
                            else
                            {
                                if (i != startIndex)
                                {
                                    if (i - testIndex < Common.circleControlPointLow)
                                    {
                                        MessageBox.Show("Two control points are closer than than the minimum distance in period" + periodNumber + ".",
                                                        "Error",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error);
                                    }
                                }
                                testIndex = i;
                            }
                        }
                    }
                }

                setupCirclePoints2(ref circlePoints, ref maxValue,ref maxValueLocationCount, ref maxValueLocations, periodNumber);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void setupCirclePoints2(ref CirclePoint[] circlePoints,
                                ref double maxValue,
                                ref int maxValueLocationCount,
                                ref int[] maxValueLocations,
                                int periodNumber)
        {
            try
            {
                //interpolate between control points
                bool go = true;
                //indexCounter = 1;
                int startControlPoint = 1;
                int endControlPoint = 0;
                while (go)
                {
                    if (circlePoints[startControlPoint] != null)
                    {

                        endControlPoint = startControlPoint + 1;
                        if (endControlPoint > Common.circlePointCount)
                        {
                            endControlPoint = 1;
                            go = false;
                        }

                        bool go2 = true;

                        while (go2)
                        {
                            if (circlePoints[endControlPoint] != null)
                            {
                                go2 = false;
                            }
                            else
                            {
                                endControlPoint++;
                            }

                            if (endControlPoint > Common.circlePointCount)
                                endControlPoint = 1;
                        }

                        float slope = 0;

                        if (startControlPoint < endControlPoint)
                            slope = (circlePoints[endControlPoint].value - circlePoints[startControlPoint].value) / (endControlPoint - startControlPoint);
                        else
                            slope = (circlePoints[endControlPoint].value - circlePoints[startControlPoint].value) / (endControlPoint + (Common.circlePointCount - startControlPoint));

                        int currentIndex;
                        if (startControlPoint < Common.circlePointCount)
                            currentIndex = startControlPoint + 1;
                        else
                            currentIndex = 1;

                        int lastIndex = startControlPoint;

                        while (currentIndex != endControlPoint)
                        {
                            circlePoints[currentIndex] = new CirclePoint();
                            circlePoints[currentIndex].value = circlePoints[lastIndex].value + slope;
                            circlePoints[currentIndex].controlPoint = false;
                            circlePoints[currentIndex].slope = slope;

                            currentIndex++;
                            lastIndex++;

                            if (currentIndex > Common.circlePointCount)
                            {
                                currentIndex = 1;
                                go = false;
                            }

                            if (lastIndex > Common.circlePointCount)
                                lastIndex = 1;
                        }

                        startControlPoint = endControlPoint;

                    }
                    else
                    {
                        //find first control point
                        startControlPoint++;
                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }   

        public static void loadValueDistributions()
        {
            try
            {
                Common.valueDistributionCount = int.Parse(INI.getINI(Common.sfile, "valueDistribution", "levels"));

                Common.valueDistributions = new ValueDistribution[Common.valueDistributionCount + 1];

                for (int i = 1; i <= Common.valueDistributionCount; i++)
                {
                    Common.valueDistributions[i] = new ValueDistribution();

                    Common.valueDistributions[i].fromString(INI.getINI(Common.sfile, "valueDistribution", i.ToString()));
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
        //public static void setNumberOfPeriodGroups()
        //{
        //    try
        //    {

        //    }
        //     catch (Exception ex)
        //    {
        //        EventLog.appEventLog_Write("error :", ex);
        //        return "0.00";
        //    }
        //}

    }
}
