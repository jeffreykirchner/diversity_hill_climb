﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Server
{
    public class PeriodGroup
    {
        public PeriodGroupPlayer[] periodGroupPlayers;
        public string groupAssignment;                 //Random, ranked or individual groupings
        public int periodGroupPlayerCount = 0;
        public int groupNumber = 0;
        public int currentLocation = 0;
        public int startingLocation;
        public int endingLocation;
        public int roundCount = 1;                           //number of rounds

        public GraphLabel labelStart = new GraphLabel();
        public GraphLabel labelEnd = new GraphLabel();

        /// <param name="groupNumber">Group number Index</param>
        public void setup(int groupNumber)
        {
            try
            {
                periodGroupPlayers = new PeriodGroupPlayer[Common.numberOfPlayers + 1];

                this.groupNumber = groupNumber;

                periodGroupPlayerCount = 0;
                roundCount = 1;

                //calc group size
                for (int i = 1; i <= Common.numberOfPlayers; i++)
                {
                    if (Common.playerlist[i].groupNumber[Common.currentPeriod] == this.groupNumber)
                    {
                        periodGroupPlayerCount++;
                        groupAssignment = Common.playerlist[i].groupAssignments[Common.currentPeriod];
                    }
                }

                //randomly add players to group
                int counter = 0;
                while (counter < periodGroupPlayerCount)
                {
                    int r = Rand.rand(Common.numberOfPlayers, 1);

                    if (Common.playerlist[r].groupNumber[Common.currentPeriod] == this.groupNumber)
                    {
                        bool found = false;
                        for (int i = 1; i <= periodGroupPlayerCount; i++)
                        {
                            if (periodGroupPlayers[i] != null)
                            {
                                if (periodGroupPlayers[i].playerNumber == r)
                                {
                                    found = true;
                                }
                            }
                        }

                        if (!found)
                        {
                            counter++;
                            periodGroupPlayers[counter] = new PeriodGroupPlayer();
                            periodGroupPlayers[counter].setup(counter, r,this);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public string getGroupList()
        {
            try
            {
                string str = "";

                str += groupAssignment + ";";
                str += periodGroupPlayerCount + ";";

                for (int i = 1; i <= periodGroupPlayerCount; i++)
                {
                    str += periodGroupPlayers[i].playerNumber + ";";
                }

                return str;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return "";
            }
        }

        public string toString()
        {
            try
            {
                string str = "";

                str += groupAssignment + ";";
                str += periodGroupPlayerCount + ";";
                str += startingLocation + ";";
                str += endingLocation + ";";
                str += roundCount + ";";

                for (int i = 1; i <= periodGroupPlayerCount; i++)
                {
                    str += periodGroupPlayers[i].toString();
                }

                return str;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return "";
            }
        }

        public void doPeriodGroup(int startLocation)
        {
            try
            {
                roundCount = 0;

                currentLocation = startLocation;
                startingLocation = startLocation;
                
                bool go = true;

                do
                {
                    int roundStartLocation = currentLocation;
                    go = false;

                    roundCount++;

                    for (int i = 1; i <= periodGroupPlayerCount; i++)
                    {
                        currentLocation = periodGroupPlayers[i].doTurns(currentLocation);
                    }

                    if (currentLocation != roundStartLocation) go = true;

                } while (roundCount < Common.maxRoundsPerPeriod &&
                         groupAssignment !="Individual" &&
                         go);

                endingLocation = currentLocation;

                //setupLabels();

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void draw(Graphics g)
        {
            try
            {
                if (periodGroupPlayerCount > 0)
                {
                    Period p = Common.periodList[Common.FrmServer.displayPeriod];

                    //if (Common.showProgramResults)
                    //{
                    for (int i = 1; i <= periodGroupPlayerCount; i++)
                    {
                        periodGroupPlayers[i].draw(g);
                    }
                    //}
                    //else
                    //{
                    //    for (int i = 1; i <= periodGroupPlayerCount; i++)
                    //    {
                    //        periodGroupPlayers[i].drawStart(g);
                    //        periodGroupPlayers[i].drawEnd(g);
                    //    }
                    //}

                    if (startingLocation == 0) return;

                    g.DrawLine(Common.FrmServer.p3StartColor, new Point(0, 0), p.circlePoints[startingLocation].location);

                    g.DrawLine(Common.FrmServer.p3EndColor, new Point(0, 0), p.circlePoints[endingLocation].location);

                    //if (drawLabels)
                    //{

                    //    //g.DrawString("Start (" + Math.Round(p.circlePoints[startingLocation].value, 2) + ")",
                    //    //             Common.Frm1.f8,
                    //    //             Brushes.Black,
                    //    //             p.circlePoints[startingLocation].locationLabel,
                    //    //             Common.Frm1.fmt);

                    //    //g.DrawString("End (" + Math.Round(p.circlePoints[endingLocation].value, 2) + ")",
                    //    //             Common.Frm1.f8,
                    //    //             Brushes.Black,
                    //    //             p.circlePoints[endingLocation].locationLabel,
                    //    //             Common.Frm1.fmt);
                    //}
                }


            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void drawLabels(Graphics g)
        {
            try
            {
                labelStart.draw(g);


                for (int i = 1; i <= periodGroupPlayerCount; i++)
                {
                    //PeriodGroupPlayer pgp = periodGroupPlayers[i];

                    periodGroupPlayers[i].labelStart.draw(g);
                    periodGroupPlayers[i].labelEnd.draw(g);
                }

                labelEnd.draw(g);

                //draw mouse over label on top
                if (labelStart.mouseIsOver) labelStart.draw(g);

                for (int i = 1; i <= periodGroupPlayerCount; i++)
                {
                    //PeriodGroupPlayer pgp = periodGroupPlayers[i];

                    if (periodGroupPlayers[i].labelStart.mouseIsOver) periodGroupPlayers[i].labelStart.draw(g);
                    if (periodGroupPlayers[i].labelEnd.mouseIsOver) periodGroupPlayers[i].labelEnd.draw(g);
                }

                if (labelEnd.mouseIsOver) labelEnd.draw(g);

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
                int totalTurnCount = 0;
                Period p = Common.periodList[Common.currentPeriod];

                //write round data
                for (int j = 1; j <= roundCount; j++)
                {
                    for (int i = 1; i <= periodGroupPlayerCount; i++)
                    {
                        periodGroupPlayers[i].writeSummaryData(j);

                        totalTurnCount += periodGroupPlayers[i].periodGroupPlayerRounds[j].turnCount;                     
                    }
                }

                //write group data
                //str = "Period,Grouping,GroupNumber,StartLocation,StartValue,EndLocation,EndValue,MaxValue,TotalTurns,TotalRounds,";
                string str = "";

                str += Common.currentPeriod + ",";
                str += groupAssignment + ",";
                str += groupNumber + ",";
                str += startingLocation + ",";
                str += p.circlePoints[startingLocation].value + ",";
                str += endingLocation + ",";
                str += p.circlePoints[endingLocation].value + ",";
                str += p.maxValue + ",";
                str += totalTurnCount + ",";
                str += roundCount + ",";

                Common.groupDf.WriteLine(str);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void writeReplayData()
        {
            try
            {
                for (int j = 1; j <= roundCount; j++)
                {
                    for (int i = 1; i <= periodGroupPlayerCount; i++)
                    {
                        periodGroupPlayers[i].writeReplayData(j);
                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public JProperty getJson(int index)
        {
            try
            {
                JObject jo = new JObject();

                jo.Add(new JProperty("Group Assignment", groupAssignment));
                jo.Add(new JProperty("Group Number", groupNumber));
                jo.Add(new JProperty("Starting Location", startingLocation));
                jo.Add(new JProperty("Ending Location", endingLocation));
                jo.Add(new JProperty("Period Group Player Count", periodGroupPlayerCount));

                JObject joPeriodGroupPlayers = new JObject();
                for (int i = 1; i <= periodGroupPlayerCount; i++)
                {
                    joPeriodGroupPlayers.Add(periodGroupPlayers[i].getJson());
                }

                jo.Add(new JProperty("Period Group Players", joPeriodGroupPlayers));

                return new JProperty(index.ToString(), jo);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return null;
            }
        }

        public void fromJSON(JProperty jp)
        {
            try
            {
                JObject jo = (JObject)jp.Value;

                groupAssignment = (string)jo["Group Assignment"];
                groupNumber = (int)jo["Group Number"];
                startingLocation = (int)jo["Starting Location"];
                endingLocation = (int)jo["Ending Location"];

                //period group players
                periodGroupPlayerCount = (int)jo["Period Group Player Count"];
                periodGroupPlayers = new PeriodGroupPlayer[periodGroupPlayerCount + 1];
                JObject joPeriodGroupPlayers = new JObject((JObject)jo["Period Group Players"]);

                for (int i = 1; i <= periodGroupPlayerCount; i++)
                {
                    periodGroupPlayers[i] = new PeriodGroupPlayer();
                    periodGroupPlayers[i].fromJSON(joPeriodGroupPlayers.Property(i.ToString()),this);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);

            }
        }

        public PointF getMoveLocation(int playerNumber, int tempTurn, int tempMove, int tempRound,int periodIndex)
        {
            try
            {                                             

                for (int i = 1; i <= periodGroupPlayerCount; i++)
                {
                    if (periodGroupPlayers[i].playerNumber == playerNumber)
                    {

                        if(periodGroupPlayers[i].periodGroupPlayerRounds[tempRound].turns[tempTurn].turnMoves == null) return new PointF(0, 0);

                        int tempCirclePointEnd = periodGroupPlayers[i].periodGroupPlayerRounds[tempRound].turns[tempTurn].turnMoves[tempMove].circlePointEnd;
                        return Common.periodList[periodIndex].circlePoints[tempCirclePointEnd].location;
                    }
                }

                return new PointF(0, 0);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return new PointF(0, 0);
            }
        }

        //public void setupLabels()
        //{
        //    try
        //    {
        //        Period p = Common.periodList[Common.currentPeriod];

        //        RectangleF[] r = new RectangleF[2 + periodGroupPlayerCount * 2 + 1];
        //        int rCounter = 1;

        //        labelStart.setupBoundingBox(r,
        //                                Common.FrmServer.mainScreen.GetGraphics(),
        //                                string.Format("Start ({0:0.00})", Math.Round(p.circlePoints[startingLocation].value, 2)),
        //                                p.circlePoints[startingLocation].value,
        //                                p.circlePoints[startingLocation].degree);


        //        r[rCounter++] = this.labelStart.boundingBox;

        //        for (int i = 1; i <= periodGroupPlayerCount; i++)
        //        {

        //            string idString;

        //            PeriodGroupPlayer pgp = periodGroupPlayers[i];

        //            idString = "P" + pgp.playerNumber.ToString() + "'s ";

        //            pgp.labelStart.setupBoundingBox(r,
        //                                        Common.FrmServer.mainScreen.GetGraphics(),
        //                                        string.Format("{1} Start ({0:0.00})", Math.Round(p.circlePoints[pgp.startingLocation].value, 2), idString),
        //                                        p.circlePoints[pgp.startingLocation].value,
        //                                        p.circlePoints[pgp.startingLocation].degree);

        //            r[rCounter++] = pgp.labelStart.boundingBox;

        //            //pgp.labelEnd = string.Format("{1} End ({0:0.00})", Math.Round(p.circlePoints[pgp.endingLocation].value, 2), idString);

        //            //Common.setupBoundingBox(r,
        //            //           Common.Frm1.mainScreen.GetGraphics(),
        //            //           pgp.labelEnd,
        //            //           p.circlePoints[pgp.endingLocation].value,
        //            //           p.circlePoints[pgp.endingLocation].degree,
        //            //           ref pgp.boundingBoxEnd,
        //            //           ref pgp.locationLabelEnd);

        //            pgp.labelEnd.setupBoundingBox(r,
        //                                        Common.FrmServer.mainScreen.GetGraphics(),
        //                                        string.Format("{1} End ({0:0.00})", Math.Round(p.circlePoints[pgp.endingLocation].value, 2), idString),
        //                                        p.circlePoints[pgp.endingLocation].value,
        //                                        p.circlePoints[pgp.endingLocation].degree);

        //            r[rCounter++] = pgp.labelEnd.boundingBox;
        //        }

        //        //Common.setupBoundingBox(r,
        //        //                Common.Frm1.mainScreen.GetGraphics(),
        //        //                labelEnd,
        //        //                p.circlePoints[endingLocation].value,
        //        //                p.circlePoints[endingLocation].degree,
        //        //                ref this.boundingBoxEnd,
        //        //                ref this.locationLabelEnd);

        //        labelEnd.setupBoundingBox(r,
        //                                Common.FrmServer.mainScreen.GetGraphics(),
        //                                string.Format("End ({0:0.00})", Math.Round(p.circlePoints[endingLocation].value, 2)),
        //                                p.circlePoints[endingLocation].value,
        //                                p.circlePoints[endingLocation].degree);

        //        r[rCounter++] = this.labelEnd.boundingBox;
        //    }
        //    catch (Exception ex)
        //    {
        //        EventLog.appEventLog_Write("error :", ex);
        //    }
        //}
    }
}