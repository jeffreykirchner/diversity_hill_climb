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
    public class PeriodGroupPlayer
    {
        public int index;                //number in group
        public PeriodGroup pg;           //period group that this is a memeber of 
        public int playerNumber;         //id number of player       

        public PeriodGroupPlayerRound[] periodGroupPlayerRounds = new PeriodGroupPlayerRound[1000];        

        public GraphLabel labelStart = new GraphLabel();
        public GraphLabel labelEnd = new GraphLabel();

        public void setup(int index, int playerNumber,PeriodGroup pg)
        {
            try
            {
                
                this.index = index;
                this.playerNumber = playerNumber;
                this.pg = pg;               
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
                str += playerNumber + ";";

                for(int i=1;i<=pg.roundCount;i++)
                {
                    str += periodGroupPlayerRounds[i].toString();
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
                periodGroupPlayerRounds[pg.roundCount] = new PeriodGroupPlayerRound();
                periodGroupPlayerRounds[pg.roundCount].setup(pg.roundCount, this);

                return periodGroupPlayerRounds[pg.roundCount].doTurns(startLocation);                
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
                for (int i = 1; i <= pg.roundCount; i++)
                {
                    if(periodGroupPlayerRounds[i] !=null)
                        periodGroupPlayerRounds[i].draw(g);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void writeSummaryData(int round)
        {
            try
            {
                //"Period,Grouping,Round,Group,GroupID,Player,StartLocation,StartValue,EndLocation,EndValue,TurnCount,"

                if (Common.showInstructions) return;
                //if (Common.replayDf == null) return;   //replay, do not record data

                PeriodGroupPlayerRound pgpr = periodGroupPlayerRounds[round];
                Period p = Common.periodList[Common.currentPeriod];
                player plr = Common.playerlist[playerNumber];

                string str = "";

                str = Common.currentPeriod + ",";
                str += pg.groupAssignment + ",";
                str += pg.groupNumber + ",";
                str += round + ",";                
                str += index + ",";
                str += playerNumber + ",";
                str += pgpr.startingLocation + ",";
                str += p.circlePoints[pgpr.startingLocation].value + ",";
                str += pgpr.endingLocation + ",";
                str += p.circlePoints[pgpr.endingLocation].value + ",";
                str += pgpr.turnCount + ",";
                str += p.maxValue + ",";
                str += plr.autoSubmit[Common.currentPeriod] + ",";

                for (int i=1;i<=Common.movesPerTurn;i++)
                {
                    str += plr.moveDirectionsList[Common.currentPeriod, i].direction + ",";
                    str += plr.moveDirectionsList[Common.currentPeriod, i].distance + ",";
                }

                Common.roundDf.WriteLine(str);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void writeReplayData(int round)
        {
            try
            {

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
                jo.Add(new JProperty("Player Number", playerNumber));

                JObject joPeriodGroupPlayerRounds = new JObject();
                for (int i = 1; i <= pg.roundCount; i++)
                {
                    joPeriodGroupPlayerRounds.Add(periodGroupPlayerRounds[i].getJson());
                }

                jo.Add(new JProperty("Period Group Player Rounds", joPeriodGroupPlayerRounds));

                return new JProperty(index.ToString(), jo);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return null;
            }
        }

        public void fromJSON(JProperty jp,PeriodGroup pg)
        {
            try
            {
                JObject jo = (JObject)jp.Value;
                this.pg = pg;

                index = (int)jo["Index"];
                playerNumber = (int)jo["Player Number"];

                //period group players

                periodGroupPlayerRounds = new PeriodGroupPlayerRound[pg.roundCount + 1];
                JObject joPeriodGroupPlayers = new JObject((JObject)jo["Period Group Player Rounds"]);

                for (int i = 1; i <= pg.roundCount; i++)
                {
                    periodGroupPlayerRounds[i] = new PeriodGroupPlayerRound();
                    periodGroupPlayerRounds[i].fromJSON(joPeriodGroupPlayers.Property(i.ToString()),this);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);

            }
        }
    }
}