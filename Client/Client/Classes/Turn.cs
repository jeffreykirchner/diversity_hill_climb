using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Client
{
    public class Turn
    {
        public int index;                 //turn number        
        public TurnMove[] turnMoves;      //moves made this turn
        public int turnMovesCount;        //number of moves this turn
        public int startLocation;
        public int endLocation;
        public int bestLocation;
        public bool drawLabels = false;

        public void fromString(ref string[] msgtokens, ref int nextToken)
        {
            try
            {
                index = int.Parse(msgtokens[nextToken++]);
                startLocation = int.Parse(msgtokens[nextToken++]);
                endLocation = int.Parse(msgtokens[nextToken++]);
                bestLocation = int.Parse(msgtokens[nextToken++]);
                turnMovesCount = int.Parse(msgtokens[nextToken++]);

                turnMoves = new TurnMove[Common.movesPerTurn+1];

                for (int i = 1; i <= turnMovesCount; i++)
                {
                    turnMoves[i] = new TurnMove();
                    turnMoves[i].fromString(ref msgtokens, ref nextToken);
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void draw(Graphics g, bool doCheck)
        {
            try
            {
                Period p = Common.periodList[Common.currentPeriod];
                for (int i = 1; i <= turnMovesCount; i++)
                {
                    if (!doCheck ||Common.Frm1.selectionMove >= i)            
                    {
                        g.DrawLine(new Pen(Common.Frm1.moveColor), new Point(0, 0), p.circlePoints[turnMoves[i].circlePointEnd].location);

                        if (drawLabels)
                        {
                            g.DrawString(index + " - " + i + " (" + Math.Round(p.circlePoints[turnMoves[i].circlePointEnd].value, 2) + ")",
                                         Common.Frm1.f8,
                                         Brushes.Black,
                                         p.circlePoints[turnMoves[i].circlePointEnd].location,
                                         Common.Frm1.fmt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void drawBest(Graphics g,int bestTurnMove)
        {
            try
            {
                Period p = Common.periodList[Common.currentPeriod];
                
                g.DrawLine(new Pen(Common.Frm1.moveColor), new Point(0, 0), p.circlePoints[turnMoves[bestTurnMove].circlePointEnd].location);

                if (drawLabels)
                {
                    g.DrawString(index + " - " + bestTurnMove + " (" + Math.Round(p.circlePoints[turnMoves[bestTurnMove].circlePointEnd].value, 2) + ")",
                                    Common.Frm1.f8,
                                    Brushes.Black,
                                    p.circlePoints[turnMoves[bestTurnMove].circlePointEnd].location,
                                    Common.Frm1.fmt);
                }
                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void fillResultsTable(int playerID,int round)
        {
            try
            {
                DataGridView dg = Common.Frm1.dgResults;

                for (int i = 1; i <= turnMovesCount; i++)
                {
                    dg.RowCount++;

                    dg[0, dg.RowCount - 1].Value = round;

                    if (playerID==Common.inumber)
                    {
                        dg[1, dg.RowCount - 1].Value = "You";
                    }
                    else
                    {
                        dg[1, dg.RowCount - 1].Value = playerID;
                    }

                    
                    dg[2, dg.RowCount - 1].Value = index;
                    dg[3, dg.RowCount - 1].Value = i;
                    dg[4, dg.RowCount - 1].Value = turnMoves[i].direction;
                    dg[5, dg.RowCount - 1].Value = turnMoves[i].distance;
                    dg[6, dg.RowCount - 1].Value = Math.Round(Common.periodList[Common.currentPeriod].circlePoints[turnMoves[i].circlePointEnd].value,2);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
