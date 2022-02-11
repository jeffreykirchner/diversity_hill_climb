using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Client
{
    public class PeriodGroup
    {
        public PeriodGroupPlayer[] periodGroupPlayers;
        public string groupAssignment;                 //Random, ranked or individual groupings
        public int periodGroupPlayerCount = 0;               
        public int startingLocation;
        public int endingLocation;
        public int roundCount = 1;                           //number of rounds
        //public bool drawLabels = true;

        public GraphLabel labelStart = new GraphLabel();
        public GraphLabel labelEnd = new GraphLabel();

        //public string labelStart;
        //public string labelEnd;

        //public PointF locationLabelStart = new PointF(0, 0);
        //public PointF locationLabelEnd = new PointF(0, 0);

        //public RectangleF boundingBoxStart;
        //public RectangleF boundingBoxEnd;

        public void fromString(ref string[] msgtokens, ref int nextToken)
        {
            try
            {
                Period p = Common.periodList[Common.currentPeriod];

                groupAssignment = msgtokens[nextToken++];
                periodGroupPlayerCount = int.Parse(msgtokens[nextToken++]);
                startingLocation = int.Parse(msgtokens[nextToken++]);
                endingLocation = int.Parse(msgtokens[nextToken++]);
                roundCount = int.Parse(msgtokens[nextToken++]);

                periodGroupPlayers = new PeriodGroupPlayer[periodGroupPlayerCount + 1];

                for (int i = 1; i <= periodGroupPlayerCount; i++)
                {
                    periodGroupPlayers[i] = new PeriodGroupPlayer();
                    periodGroupPlayers[i].fromString(ref msgtokens, ref nextToken,this);
                }

                //find label positions
                //labelStart = string.Format("Start ({0:0.00})", Math.Round(p.circlePoints[startingLocation].value, 2));
                //labelEnd = string.Format("End ({0:0.00})", Math.Round(p.circlePoints[endingLocation].value, 2));

                //RectangleF[] r = new RectangleF[2 + periodGroupPlayerCount*2+1];
                //int rCounter = 1;

                //Common.setupBoundingBox(r,
                //                 Common.Frm1.mainScreen.GetGraphics(),
                //                 labelStart,
                //                 p.circlePoints[startingLocation].value,
                //                 p.circlePoints[startingLocation].degree,
                //                 ref this.boundingBoxStart,
                //                 ref this.locationLabelStart);

                //labelStart.setupBoundingBox(r,
                //                        Common.Frm1.mainScreen.GetGraphics(),
                //                        string.Format("Start ({0:0.00})", Math.Round(p.circlePoints[startingLocation].value, 2)),
                //                        p.circlePoints[startingLocation].value,
                //                        p.circlePoints[startingLocation].degree);


                //r[rCounter++] = this.labelStart.boundingBox;               

                //for(int i=1;i<=periodGroupPlayerCount;i++)
                //{

                //    string idString;

                //    PeriodGroupPlayer pgp = periodGroupPlayers[i];

                //    if (pgp.playerNumber == Common.inumber)
                //        idString = "Your";
                //    else
                //        idString =  "P" + pgp.playerNumber.ToString() + "'s ";
                                                            
                //    pgp.labelStart.setupBoundingBox(r,
                //                                Common.Frm1.mainScreen.GetGraphics(),
                //                                string.Format("{1} Start ({0:0.00})", Math.Round(p.circlePoints[pgp.startingLocation].value, 2), idString),
                //                                p.circlePoints[pgp.startingLocation].value,
                //                                p.circlePoints[pgp.startingLocation].degree);                                                                        

                //    r[rCounter++] = pgp.labelStart.boundingBox;

                    //pgp.labelEnd = string.Format("{1} End ({0:0.00})", Math.Round(p.circlePoints[pgp.endingLocation].value, 2), idString);

                    //Common.setupBoundingBox(r,
                    //           Common.Frm1.mainScreen.GetGraphics(),
                    //           pgp.labelEnd,
                    //           p.circlePoints[pgp.endingLocation].value,
                    //           p.circlePoints[pgp.endingLocation].degree,
                    //           ref pgp.boundingBoxEnd,
                    //           ref pgp.locationLabelEnd);

                //    pgp.labelEnd.setupBoundingBox(r,
                //                                Common.Frm1.mainScreen.GetGraphics(),
                //                                string.Format("{1} End ({0:0.00})", Math.Round(p.circlePoints[pgp.endingLocation].value, 2), idString),
                //                                p.circlePoints[pgp.endingLocation].value,
                //                                p.circlePoints[pgp.endingLocation].degree);                  

                //    r[rCounter++] = pgp.labelEnd.boundingBox;
                //}

                //Common.setupBoundingBox(r,
                //                Common.Frm1.mainScreen.GetGraphics(),
                //                labelEnd,
                //                p.circlePoints[endingLocation].value,
                //                p.circlePoints[endingLocation].degree,
                //                ref this.boundingBoxEnd,
                //                ref this.locationLabelEnd);

                //labelEnd.setupBoundingBox(r,
                //                        Common.Frm1.mainScreen.GetGraphics(),
                //                        string.Format("End ({0:0.00})", Math.Round(p.circlePoints[endingLocation].value, 2)),
                //                        p.circlePoints[endingLocation].value,
                //                        p.circlePoints[endingLocation].degree);

                //r[rCounter++] = this.labelEnd.boundingBox;
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
                    Period p = Common.periodList[Common.currentPeriod];

                    if (Common.drawProgramResults)
                    {                     
                        for (int i = 1; i <= periodGroupPlayerCount; i++)
                        {
                            periodGroupPlayers[i].draw(g);

                            //if (Common.Frm1.selectionIndex > i)
                            //{
                                
                            //}
                            //else if(Common.Frm1.selectionIndex  == i)
                            //{
                            //    periodGroupPlayers[i].draw(g, true);
                            //}
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= periodGroupPlayerCount; i++)
                        {
                            periodGroupPlayers[i].drawStart(g);
                            periodGroupPlayers[i].drawEnd(g);
                        }
                    }

                    g.DrawLine(Common.Frm1.p3StartColor, new Point(0, 0), p.circlePoints[startingLocation].location);                   

                    g.DrawLine(Common.Frm1.p3EndColor, new Point(0, 0), p.circlePoints[endingLocation].location);

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

        //public void drawLabels(Graphics g)
        //{
        //    try
        //    {
        //        labelStart.draw(g);
               

        //        for (int i = 1; i <= periodGroupPlayerCount; i++)
        //        {
        //            //PeriodGroupPlayer pgp = periodGroupPlayers[i];

        //            periodGroupPlayers[i].labelStart.draw(g);
        //            periodGroupPlayers[i].labelEnd.draw(g);                    
        //        }

        //        labelEnd.draw(g);

        //        //draw mouse over label on top
        //        if(labelStart.mouseIsOver) labelStart.draw(g);

        //        for (int i = 1; i <= periodGroupPlayerCount; i++)
        //        {
        //            //PeriodGroupPlayer pgp = periodGroupPlayers[i];

        //            if(periodGroupPlayers[i].labelStart.mouseIsOver) periodGroupPlayers[i].labelStart.draw(g);
        //            if (periodGroupPlayers[i].labelEnd.mouseIsOver) periodGroupPlayers[i].labelEnd.draw(g);
        //        }

        //        if (labelEnd.mouseIsOver) labelEnd.draw(g);

        //    }
        //    catch (Exception ex)
        //    {
        //        EventLog.appEventLog_Write("error :", ex);
        //    }
        //}


        public PointF getMoveLocation(int playerNumber,int tempTurn,int tempMove,int tempRound)
        {
            try
            {               

                for(int i=1;i<=periodGroupPlayerCount;i++)
                {
                    if(periodGroupPlayers[i].playerNumber==playerNumber)
                    {
                        int tempCirclePointEnd = periodGroupPlayers[i].periodGroupPlayerRounds[tempRound].turns[tempTurn].turnMoves[tempMove].circlePointEnd;
                        return Common.periodList[Common.currentPeriod].circlePoints[tempCirclePointEnd].location;                   
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

        public int getMoveCirclePoint(int playerNumber, int tempTurn, int tempMove,int tempRound)
        {
            try
            {

                for (int i = 1; i <= periodGroupPlayerCount; i++)
                {
                    if (periodGroupPlayers[i].playerNumber == playerNumber)
                    {
                        int tempCirclePointEnd = periodGroupPlayers[i].periodGroupPlayerRounds[tempRound].turns[tempTurn].turnMoves[tempMove].circlePointEnd;
                        return tempCirclePointEnd;
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }
        }

        public int getStartingCirclePoint(int playerNumber, int tempTurn,int tempRound)
        {
            try
            {

                for (int i = 1; i <= periodGroupPlayerCount; i++)
                {
                    if (periodGroupPlayers[i].playerNumber == playerNumber)
                    {
                        return (periodGroupPlayers[i].periodGroupPlayerRounds[tempRound].turns[tempTurn].startLocation);
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }
        }

        public int getPlayerIndex(int playerNumber)
        {
            try
            {
                for (int i = 1; i <= periodGroupPlayerCount; i++)
                {
                    if (periodGroupPlayers[i].playerNumber == playerNumber)
                    {
                        return i;
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }
        }

       
    }
}

