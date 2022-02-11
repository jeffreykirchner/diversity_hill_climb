using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Server
{
    public class Turn
    {
        public int index;                 //turn number        
        public TurnMove[] turnMoves;      //moves made this turn
        public int turnMovesCount;        //number of moves this turn
        public int startLocation;
        public int endLocation;
        public int bestLocation;

        public string toString()
        {
            try
            {
                string str = "";

                str = index + ";";
                str += startLocation + ";";
                str += endLocation + ";";
                str += bestLocation + ";";
                str += turnMovesCount + ";";

                for (int i=1;i<= turnMovesCount; i++)
                {
                    str += turnMoves[i].toString();
                }                

                return str;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return "";
            }
        }

        public void doTurn(int startLocation,MoveDirections[] moves,int index)
        {
            try
            {
                Period p = Common.periodList[Common.currentPeriod];

                this.startLocation = startLocation;
                this.index = index;

                turnMoves = new TurnMove[Common.movesPerTurn+1];

                int tempLocation = startLocation;
                bestLocation = startLocation;

                for(int i=1;i<=Common.movesPerTurn;i++)
                {
                    turnMoves[i] = new TurnMove(this);

                    tempLocation = turnMoves[i].doTurnMove(tempLocation, i,index, moves);

                    turnMovesCount = i;

                    if (p.circlePoints[tempLocation].value > p.circlePoints[bestLocation].value)
                    {
                        bestLocation = tempLocation;
                        break;
                    }
                    else
                    {
                        tempLocation = startLocation;
                    }
                }               

                endLocation = bestLocation;
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
                Period p = Common.periodList[Common.FrmServer.displayPeriod];
                for (int i = 1; i <= turnMovesCount; i++)
                {
                    g.DrawLine(new Pen(Common.FrmServer.moveColor), new Point(0, 0), p.circlePoints[turnMoves[i].circlePointEnd].location);

                    
                    //g.DrawString(index + " - " + i + " (" + Math.Round(p.circlePoints[turnMoves[i].circlePointEnd].value, 2) + ")",
                    //                Common.FrmServer.f8,
                    //                Brushes.Black,
                    //                p.circlePoints[turnMoves[i].circlePointEnd].location,
                    //                Common.FrmServer.fmt);
                    
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public JProperty getJson()
        {
            try
            {
                JObject jo = new JObject();

                jo.Add(new JProperty("Index", index));
                jo.Add(new JProperty("Start Location", startLocation));
                jo.Add(new JProperty("End Location", endLocation));
                jo.Add(new JProperty("Best Location", bestLocation));

                jo.Add(new JProperty("Turn Moves Count", turnMovesCount));

                JObject joTurnMoves = new JObject();
                for (int i = 1; i <= turnMovesCount; i++)
                {
                    joTurnMoves.Add(turnMoves[i].getJson());
                }

                jo.Add(new JProperty("Turn Moves", joTurnMoves));

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

                index = (int)jo["Index"];
                startLocation = (int)jo["Start Location"];
                endLocation = (int)jo["End Location"];
                bestLocation = (int)jo["Best Location"];

                //turn moves
                turnMovesCount = (int)jo["Turn Moves Count"];
                turnMoves = new TurnMove[turnMovesCount + 1];
                JObject joTurnMoves = new JObject((JObject)jo["Turn Moves"]);

                for (int i = 1; i <= turnMovesCount; i++)
                {
                    turnMoves[i] = new TurnMove(this);
                    turnMoves[i].fromJSON(joTurnMoves.Property(i.ToString()));
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);

            }
        }
    }
}
