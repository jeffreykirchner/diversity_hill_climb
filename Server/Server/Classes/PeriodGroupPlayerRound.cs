using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Server
{
    public class PeriodGroupPlayerRound
    {
        public int index;
        public PeriodGroupPlayer pgp;  //period group player this is a memeber of

        public Turn[] turns;            //round,turn array of turns taken  
        public int turnCount;          //round number of turns taken            
        public int startingLocation;   //location where player starts
        public int endingLocation;     //location where player stops    
        public int bestTurn;           //turn that had the highest value
        public int bestTurnMove;       //move within best turn that had highest value


        public void setup(int index, PeriodGroupPlayer pgp)
        {
            try
            {
                this.index = index;
                this.pgp = pgp;

                this.bestTurn = 1;
                this.bestTurnMove = 1;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public string toString()
        {
            try
            {
                string str = "";

                str = index + ";";               
                str += startingLocation + ";";
                str += endingLocation + ";";
                str += turnCount + ";";
                str += bestTurn + ";";
                str += bestTurnMove + ";";

                for (int i = 1; i <= turnCount; i++)
                {
                    str += turns[i].toString();
                }

                //MoveDirections[] moveDirections = Common.playerlist[playerIndex].getCurrentPeriodMoves();

                //for (int i = 1; i <= Common.movesPerTurn; i++)
                //{
                //    str += moveDirections[i].toString();
                //}

                return str;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return "";
            }
        }

        public int doTurns(int startLocation)
        {
            try
            {
                turns = new Turn[Common.maxTurnsPerPeriod + 1];

                for (int i = 1; i <= Common.maxTurnsPerPeriod; i++)
                {
                    turns[i] = new Turn();
                }

                int counter = 1;
                turnCount = 1;

                bool go = true;
                int location = startLocation;
                startingLocation = startLocation;

                turns[1].bestLocation = startLocation;

                while (go)
                {

                    turns[counter].doTurn(location, Common.playerlist[pgp.playerNumber].getCurrentPeriodMoves(), counter);

                    if (Common.periodList[Common.currentPeriod].circlePoints[turns[counter].startLocation].value >=
                        Common.periodList[Common.currentPeriod].circlePoints[turns[counter].bestLocation].value)
                    {
                        go = false;
                    }
                    else
                    {
                        location = turns[counter].bestLocation;
                        counter++;
                        if (counter > Common.maxTurnsPerPeriod)
                        {
                            go = false;
                        }
                        else
                        {
                            //turns[counter].bestLocation = location;
                            turnCount = counter;
                        }
                    }
                }

                endingLocation = location;

                //find best turn and move
                bestTurn = 1;
                bestTurnMove = 1;
                for (int i = 1; i <= turnCount; i++)
                {
                    for (int j = 1; j <= turns[i].turnMovesCount; j++)
                    {
                        int cp1 = turns[bestTurn].turnMoves[bestTurnMove].circlePointEnd;
                        int cp2 = turns[i].turnMoves[j].circlePointEnd;

                        if (Common.periodList[Common.currentPeriod].circlePoints[cp2].value >
                            Common.periodList[Common.currentPeriod].circlePoints[cp1].value)
                        {
                            bestTurn = i;
                            bestTurnMove = j;
                        }
                    }
                }

                return endingLocation;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }
        }

        public void draw(Graphics g)
        {
            try
            {
                for (int i = 1; i <= turnCount; i++)
                {
                    turns[i].draw(g);
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

                jo.Add(new JProperty("Starting Location", startingLocation));
                jo.Add(new JProperty("Ending Location", endingLocation));
                jo.Add(new JProperty("Best Turn", bestTurn));
                jo.Add(new JProperty("Best Turn Move", bestTurnMove));

                jo.Add(new JProperty("Turn Count", turnCount));

                JObject joTurns = new JObject();
                for (int i = 1; i <= turnCount; i++)
                {
                    joTurns.Add(turns[i].getJson());
                }

                jo.Add(new JProperty("Turns", joTurns));

                return new JProperty(index.ToString(), jo);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return null;
            }
        }

        public void fromJSON(JProperty jp,PeriodGroupPlayer pgp)
        {
            try
            {
                JObject jo = (JObject)jp.Value;
                this.pgp = pgp;

                startingLocation = (int)jo["Starting Location"];
                endingLocation = (int)jo["Ending Location"];
                bestTurn = (int)jo["Best Turn"];
                bestTurnMove = (int)jo["Best Turn Move"];

                //turns
                turnCount = (int)jo["Turn Count"];
                turns = new Turn[turnCount + 1];
                JObject joTurns = new JObject((JObject)jo["Turns"]);

                for (int i = 1; i <= turnCount; i++)
                {
                    turns[i] = new Turn();
                    turns[i].fromJSON(joTurns.Property(i.ToString()));
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);

            }
        }
    }
}
