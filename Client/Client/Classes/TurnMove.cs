using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class TurnMove
    {
        public int circlePointStart;         //where the move started
        public int circlePointEnd;           //where the move ended
        public int index;                    //from 1 to number of moves this turn        
        public int turnNumber;                     //turn number from 1 to N

        public int distance;                 //distance setting for this move 
        public string direction;             //direction   ""

        public void fromString(ref string[] msgtokens, ref int nextToken)
        {
            try
            {
                circlePointStart = int.Parse(msgtokens[nextToken++]);
                circlePointEnd = int.Parse(msgtokens[nextToken++]);
                index = int.Parse(msgtokens[nextToken++]);
                turnNumber = int.Parse(msgtokens[nextToken++]);
                distance = int.Parse(msgtokens[nextToken++]);
                direction = msgtokens[nextToken++];
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}

