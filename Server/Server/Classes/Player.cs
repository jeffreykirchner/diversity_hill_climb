using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class player
    {
        public socketPlayer sp = new socketPlayer();
        public int inumber;
        public string name;
        public string studentID;
        public double earnings;
        public double[] periodEarnings;

        public int groupNumberRandom = -1;
        public int groupNumberSorted = -1;
        public double totalInduvidualScore = 0;                 //total earnings from individual periods

        public MoveDirections[,] moveDirectionsList;            //period,moves
        public string[] groupAssignments;                       //Random, ranked or individual groupings

        public int[] groupNumber;

        public bool[] readyToGoOnPressed = new bool[10000];     //ready to go on button pressed 
        public bool[] movesSubmitted = new bool[10000];         //player has submitted moves

        public bool[] autoSubmit = new bool[10000];             //true if player let time expire 

        public void FromString(ref string[] msgtokens,ref int nextToken)
        {
            try
            {
            

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public override string ToString()
        {
            try
            {
                string s="";              

                return s;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return "";
            }
        }

        //send message to the client
        public void sendMessage(string index, string message)
        {
            try
            {
                if (sp == null) return;
                if (Common.closing) return;

                sp.send(index, message);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //send begin message to client
        public void sendBegin(string str)
        {
            try
            {
                for(int i=1;i<=Common.numberOfPeriods;i++)
                {
                    readyToGoOnPressed[i] = false;
                    movesSubmitted[i] = false;
                    autoSubmit[i] = false;
                }

                moveDirectionsList = new MoveDirections[Common.numberOfPeriods+1,Common.movesPerTurn+1];
                periodEarnings = new double[Common.numberOfPeriods + 1];

                string outstr = str;

                outstr += Common.numberOfPlayers + ";";
                outstr += Common.numberOfPeriods + ";";
                outstr += Common.instructionX + ";";
                outstr += Common.instructionY + ";";
                outstr += Common.windowX + ";";
                outstr += Common.windowY + ";";
                outstr += Common.showInstructions + ";";
                outstr += inumber + ";";
                outstr += Common.testMode + ";";
                
                outstr += Common.groupSize + ";";                
                outstr += Common.circlePointCount + ";";
                outstr += Common.circleControlPointLow + ";";
                outstr += Common.circleControlPointHigh + ";";
                outstr += Common.minControlPointValue + ";";
                outstr += Common.maxControlPointValue + ";";
                outstr += Common.movesPerTurn + ";";
                outstr += Common.maxTurnsPerPeriod + ";";               
                outstr += Common.showFullCircle + ";";
                outstr += Common.maxRoundsPerPeriod + ";";

                outstr += Common.periodLength + ";";
                outstr += Common.readyToGoOnLength + ";";

                outstr += Common.periodList[Common.currentPeriod].toString();
                outstr += Common.periodList[Common.currentPeriod].periodGroups[groupNumber[Common.currentPeriod]].getGroupList();

                Common.FrmServer.dgMain[4, inumber - 1].Value = groupNumber[Common.currentPeriod];

                if (Common.showInstructions)
                    Common.FrmServer.dgMain[2, inumber - 1].Value = "Instructions";
                else
                    Common.FrmServer.dgMain[2, inumber - 1].Value = "Playing";

                //for (int i=1;i<=Common.numberOfPeriods;i++)
                //{

                //}

                //players
                //for (int i=1;i<=Common.numberOfPlayers;i++)
                //{
                //    outstr += Common.playerlist[i].ToString();
                //}

                sendMessage("BEGIN", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void loadGroupings()
        {
            try
            {
                groupAssignments = new string[Common.numberOfPeriods+1];


                groupAssignments[0] = "Individual";

                //int nextToken = 0;

                for (int i=1;i<=Common.numberOfPeriods; i++)
                {
                    string[] msgtokens = INI.getINI(Common.sfile, "periods", i.ToString()).Split(';');
                    groupAssignments[i] = msgtokens[inumber-1];
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void setupGroupNumber()
        {
            try
            {
                if(Common.showInstructions)
                {
                    groupNumber[0] = inumber;
                }
                else if(groupAssignments[Common.currentPeriod] == "Individual")
                {
                    groupNumber[Common.currentPeriod] = inumber;
                }
                else if(groupAssignments[Common.currentPeriod] == "Random")
                {
                    groupNumber[Common.currentPeriod] = groupNumberRandom;
                }
                else
                {
                    groupNumber[Common.currentPeriod] = groupNumberSorted;
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void sendInvalidConnection()
        {
            try
            {
                sendMessage("INVALID_CONNECTION", "");
            }
            catch (Exception ex)
            {
               EventLog.appEventLog_Write("error :", ex);
            }
        }
        
        //kill the clients
        public void sendReset()
        {
            try
            {
                string outstr = "";
                sendMessage("RESET", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //kill the clients
        public void sendShowName()
        {
            try
            {
               // if (Common.replayDf == null) return;   //replay

                string outstr = "";

                outstr = String.Format(Common.culture, "{0:C}", earnings / 100) + ";";

                sendMessage("SHOW_NAME", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //all subjects done with instructions, proceed to next phase
        public void sendFinishedInstructions()
        {
            try
            {

                string outstr = "";

                outstr += Common.currentPeriod + ";";
               

                sendMessage("FINISHED_INSTRUCTIONS", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //singal the clients that the current period is now that last period
        public void sendEndEarly()
        {
            try
            {
                string outstr = Common.numberOfPeriods + ";";

                sendMessage("END_EARLY", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void sendStartNextPeriod()
        {
            try
            {
                //if (Common.replayDf == null) return;   //replay, do not record data                              

                string outstr = "";

                outstr += Common.currentPeriod.ToString() + ";";
                outstr += earnings.ToString() + ";";
                outstr += Common.showInstructions.ToString() + ";";

                outstr += Common.periodList[Common.currentPeriod].toString();
                outstr += Common.periodList[Common.currentPeriod].periodGroups[groupNumber[Common.currentPeriod]].getGroupList();

                Common.FrmServer.dgMain[6, inumber - 1].Value = groupAssignments[Common.currentPeriod];
                Common.FrmServer.dgMain[4, inumber - 1].Value = groupNumber[Common.currentPeriod];
                Common.FrmServer.dgMain[2, inumber - 1].Value = "Playing";


                sendMessage("START_NEXT_PERIOD", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void sendPeriodResults()
        {
            try
            {
                //if (Common.replayDf == null) return;   //replay, do not record data              

                int endingCirclePoint = Common.periodList[Common.currentPeriod].periodGroups[groupNumber[Common.currentPeriod]].endingLocation;

                if (!Common.showInstructions)
                {
                    periodEarnings[Common.currentPeriod] = Common.periodList[Common.currentPeriod].circlePoints[endingCirclePoint].value;
                    earnings += periodEarnings[Common.currentPeriod];
                }

                string outstr = "";

                outstr = earnings.ToString() + ";";
                outstr += periodEarnings[Common.currentPeriod].ToString() +";";

                outstr += Common.periodList[Common.currentPeriod].periodGroups[groupNumber[Common.currentPeriod]].toString();

                for (int i = 1; i <= Common.movesPerTurn; i++)
                    outstr += moveDirectionsList[Common.currentPeriod, i].toString();

                if(groupAssignments[Common.currentPeriod] == "Individual" && !Common.showInstructions)
                {
                    calcTotalIndividualScore();
                }

                if (!Common.showInstructions)
                {
                    Common.FrmServer.dgMain[2, inumber - 1].Value = "Reviewing Results";
                    Common.FrmServer.dgMain[3, inumber - 1].Value = string.Format(Common.culture, "{0:C}", Math.Round(earnings / 100, 2));
                }

                sendMessage("01", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void writeSummaryData()
        {
            try
            {
                //if (Common.showInstructions) return;
                //if (Common.replayDf == null) return;   //replay, do not record data

                //string str = "";

                //str = Common.currentPeriod + ",";                
                //str += inumber + ",";               

                //Common.summaryDf.WriteLine(str);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);               
            }
        }


        //public void writeReplayData()
        //{
        //    try
        //    {
        //        //str = "Period,Time,Player,LocationX,LocationY,TargetX,TargetY,Health,HealthTime,CoolingTime,Emote,EmoteTime,";

        //        if (Common.replayDf == null) return;   //replay, do not record data
        //        if (Common.showInstructions) return;

        //        string outstr = "";

        //        outstr = Common.currentPeriod + ",";               
        //        outstr += inumber + ",";

        //        Common.replayDf.WriteLine(outstr);
        //    }
        //     catch (Exception ex)
        //    {
        //        EventLog.appEventLog_Write("error :", ex);                
        //    }
        //}

        //public void readReplayData(string s)
        //{
        //    try
        //    {
        //        string[] msgtokens = s.Split(',');
        //        //int nextToken = 3;
        //    }
        //    catch (Exception ex)
        //    {
        //        EventLog.appEventLog_Write("error :", ex);
        //    }
        //}

        public MoveDirections[] getCurrentPeriodMoves()
        {
            try
            {
                MoveDirections[] md = new MoveDirections[Common.movesPerTurn+1];

                for(int i=1;i<=Common.movesPerTurn;i++)
                {
                    md[i] = new MoveDirections();
                    md[i] = moveDirectionsList[Common.currentPeriod, i];
                }

                return md;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return null;
            }
        }

        public void calcTotalIndividualScore()
        {
            try
            {
                totalInduvidualScore = 0;

                for(int i=1;i<=Common.currentPeriod;i++)
                {
                    if (groupAssignments[i] == "Individual")
                    {
                        totalInduvidualScore += periodEarnings[i];
                    }
                }

                Common.FrmServer.dgMain[5, inumber - 1].Value = totalInduvidualScore;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);                
            }
        }

    }
}
