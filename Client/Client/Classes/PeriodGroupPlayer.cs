using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Client
{
    public class PeriodGroupPlayer
    {
        public int index;                //number in group        
        public int playerNumber;         //id number of player
        public PeriodGroup pg;    //period group player this is a memeber of
        public PeriodGroupPlayerRound[] periodGroupPlayerRounds = new PeriodGroupPlayerRound[1000];

        //public string labelBest;                                    //text of label
        //public PointF locationLabelBest = new PointF(0, 0);         //location of label   
        //public RectangleF boundingBoxBest;                          //bounding box of label

        //public string labelStart;                                    //text of label
        //public PointF locationLabelStart = new PointF(0, 0);         //location of label   
        //public RectangleF boundingBoxStart;                          //bounding box of label

        //public string labelEnd;                                    //text of label
        //public PointF locationLabelEnd = new PointF(0, 0);         //location of label   
        //public RectangleF boundingBoxEnd;                          //bounding box of label

        public void fromString(ref string[] msgtokens, ref int nextToken,PeriodGroup pg)
        {

            try
            {
                index = int.Parse(msgtokens[nextToken++]);
                playerNumber = int.Parse(msgtokens[nextToken++]);

                this.pg = pg;

                for(int i=1;i<=pg.roundCount;i++)
                {
                    periodGroupPlayerRounds[i] = new PeriodGroupPlayerRound();
                    periodGroupPlayerRounds[i].fromString(ref msgtokens,ref nextToken, this);
                }

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
                for(int i=1;i<=pg.roundCount;i++)
                {

                    if(Common.Frm1.selectionRound>i)
                    {
                        periodGroupPlayerRounds[i].draw(g,false);
                    }
                    else if(Common.Frm1.selectionRound == i)
                    {
                        if(Common.Frm1.selectionIndex>index)
                        {
                            periodGroupPlayerRounds[i].draw(g, false);
                        }
                        else if(Common.Frm1.selectionIndex == index)
                        {
                            periodGroupPlayerRounds[i].draw(g, true);
                        }
                    }                    
                }                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void drawBest(Graphics g)
        {
            try
            {
                for (int i = 1; i <= pg.roundCount; i++)
                {
                    periodGroupPlayerRounds[i].drawBest(g);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void drawStart(Graphics g)
        {
            try
            {
                for (int i = 1; i <= pg.roundCount; i++)
                {
                    periodGroupPlayerRounds[i].drawStart(g);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void drawEnd(Graphics g)
        {
            try
            {
                for (int i = 1; i <= pg.roundCount; i++)
                {
                    periodGroupPlayerRounds[i].drawEnd(g);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void fillResultsTable(int playerId,int round)
        {
            try
            {
                //for (int i = 1; i <= pg.roundCount; i++)
                //{
                    periodGroupPlayerRounds[round].fillResultsTable(playerId,round);
                //}
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //public void setupBoundingBox(RectangleF[] r, Graphics g)
        //{
        //    try
        //    {
        //        Period p = Common.periodList[Common.currentPeriod];

        //        int cp = turns[bestTurn].turnMoves[bestTurnMove].circlePointEnd;
        //        float value = p.circlePoints[cp].value;
        //        float degree = p.circlePoints[cp].degree;

        //        if (playerIndex == Common.inumber)
        //        {
        //            labelBest = string.Format("Your Best ({0:0.00})", Math.Round(value, 2));
        //        }
        //        else
        //        {
        //            labelBest = string.Format("P{1}'s Best ({0:0.00})", Math.Round(value, 2),index);
        //        }
                
        //        int counter = 10;

        //        setupBoundingBox2(counter, g, value, degree);

        //        if (r != null)
        //        {

        //            bool go = true;

        //            while (counter > -200 && go)
        //            {
        //                go = false;

        //                for (int i = 1; i <= r.Length - 1; i++)
        //                {
        //                    if (r[i].IntersectsWith(boundingBoxBest))
        //                    {
        //                        setupBoundingBox2(counter, g, value, degree);
        //                        go = true;
        //                    }                            
        //                }

        //                counter--;
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        EventLog.appEventLog_Write("error :", ex);
        //    }
        //}

        //void setupBoundingBox2(int counter, Graphics g, float value, float degree)
        //{
        //    try
        //    {
        //        float tempDistance2 = Common.Frm1.circleRadius + counter + Common.dataUnit * (float)value;

        //        locationLabelBest = new PointF(tempDistance2 * (float)Math.Cos(degree * (Math.PI / 180)),
        //                                   tempDistance2 * (float)Math.Sin(degree * (Math.PI / 180)) - 7);

        //        SizeF sf = g.MeasureString(labelBest, Common.Frm1.f10, Common.Frm1.pnlMain.Width, Common.Frm1.fmt);
        //        sf.Width += 2;
        //        sf.Height += 2;
        //        boundingBoxBest = new RectangleF(new PointF(locationLabelBest.X - 1 - sf.Width / (float)2.0, locationLabelBest.Y - 1), sf);
        //    }
        //    catch (Exception ex)
        //    {
        //        EventLog.appEventLog_Write("error :", ex);
        //    }
        //}

        //public void drawLabel(Graphics g,string label,PointF loca)
        //{
        //    try
        //    {
        //        g.FillRectangle(Brushes.White, boundingBoxBest.X, boundingBoxBest.Y, boundingBoxBest.Width, boundingBoxBest.Height);
        //        g.DrawRectangle(Pens.Black, boundingBoxBest.X, boundingBoxBest.Y, boundingBoxBest.Width, boundingBoxBest.Height);

        //        g.DrawString(labelBest,
        //                    Common.Frm1.f10,
        //                    Brushes.Black,
        //                    locationLabelBest,
        //                    Common.Frm1.fmt);
        //    }
        //    catch (Exception ex)
        //    {
        //        EventLog.appEventLog_Write("error :", ex);
        //    }
        //}
    }
}
