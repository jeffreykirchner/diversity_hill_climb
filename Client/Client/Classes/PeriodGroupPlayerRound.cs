using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Client
{
    public class PeriodGroupPlayerRound
    {  
        public int index;                //which round this is
        public PeriodGroupPlayer pgp;    //period group player this is a memeber of

        public Turn[] turns;             //array of turns taken  
        public int turnCount = 1;        //number of turns taken            
        public int startingLocation;     //location where player starts
        public int endingLocation;       //location where player stops    
        public int bestTurn;             //turn that had the highest value
        public int bestTurnMove;         //move within best turn that had highest value 

        public GraphLabel labelBest = new GraphLabel();
        public GraphLabel labelStart = new GraphLabel();
        public GraphLabel labelEnd = new GraphLabel();

        public void fromString(ref string[] msgtokens, ref int nextToken,PeriodGroupPlayer pgp)
        {

            try
            {
                this.pgp = pgp;

                index = int.Parse(msgtokens[nextToken++]);                
                startingLocation = int.Parse(msgtokens[nextToken++]);
                endingLocation = int.Parse(msgtokens[nextToken++]);
                turnCount = int.Parse(msgtokens[nextToken++]);
                bestTurn = int.Parse(msgtokens[nextToken++]);
                bestTurnMove = int.Parse(msgtokens[nextToken++]);

                turns = new Turn[turnCount + 1];

                for (int i = 1; i <= turnCount; i++)
                {
                    turns[i] = new Turn();
                    turns[i].fromString(ref msgtokens, ref nextToken);
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
                for (int i = 1; i <= turnCount; i++)
                {
                    if (!doCheck || Common.Frm1.selectionTurn > i)
                    {
                        turns[i].draw(g, false);
                    }
                    else if (Common.Frm1.selectionTurn == i)
                    {
                        turns[i].draw(g, true);
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
                turns[bestTurn].drawBest(g, bestTurnMove);
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
                Period p = Common.periodList[Common.currentPeriod];
                g.DrawLine(new Pen(Common.Frm1.moveColor), new Point(0, 0), p.circlePoints[startingLocation].location);
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
                Period p = Common.periodList[Common.currentPeriod];
                g.DrawLine(new Pen(Common.Frm1.moveColor), new Point(0, 0), p.circlePoints[endingLocation].location);
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
                for (int i = 1; i <= turnCount; i++)
                {
                    turns[i].fillResultsTable(playerId,round);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
