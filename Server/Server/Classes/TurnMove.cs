using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Server
{
    public class TurnMove
    {
        public int circlePointStart;         //where the move started
        public int circlePointEnd;           //where the move ended
        public int index;                    //from 1 to number of moves this turn        
        public int turnNumber;               //turn number from 1 to N

        public int distance;                 //distance setting for this move 
        public string direction;             //direction   ""

        public Turn turn;                    //parent turn

        public TurnMove(Turn turn)
        {
            try
            {
                this.turn = turn;
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
                               
                str = circlePointStart + ";";
                str += circlePointEnd + ";";
                str += index + ";";                
                str += turnNumber + ";";
                str += distance + ";";
                str += direction + ";";

                return str;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return "";
            }
        }

        public int doTurnMove(int circlePointStart,int index,int turn,MoveDirections[] moves)
        {
            try
            {
                this.circlePointStart = circlePointStart;
                this.index = index;
                this.turnNumber = turn;

                direction = moves[index].direction;
                distance = moves[index].distance;

                if(direction == "Counter Clockwise")
                    circlePointEnd = circlePointStart - distance;
                else
                    circlePointEnd = circlePointStart + distance;

                if (circlePointEnd<=0)
                {
                    circlePointEnd += Common.circlePointCount;
                }
                else if(circlePointEnd>Common.circlePointCount)
                {
                    circlePointEnd -= Common.circlePointCount;
                }

                return circlePointEnd;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return -1;
            }
        }

        public JProperty getJson()
        {
            try
            {
                JObject jo = new JObject();

                jo.Add(new JProperty("Circle Point Start", circlePointStart));
                jo.Add(new JProperty("Circle Point End", circlePointEnd));
                jo.Add(new JProperty("Index", index));
                jo.Add(new JProperty("Turn Number", turnNumber));
                jo.Add(new JProperty("Distance", distance));
                jo.Add(new JProperty("Direction", direction));

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

                circlePointStart = (int)jo["Circle Point Start"];
                circlePointEnd = (int)jo["Circle Point End"];
                index = (int)jo["Index"];
                turnNumber = (int)jo["Turn Number"];
                distance = (int)jo["Distance"];
                direction = (string)jo["Direction"];
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);

            }
        }
    }
}
