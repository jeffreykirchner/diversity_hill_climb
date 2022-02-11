using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class MoveDirections
    {
        public string direction;             //left or right
        public int distance;                 //distance gone

        public void fromString(ref string[] msgtokens, ref int nextToken)
        {
            try
            {
                direction = msgtokens[nextToken++];
                distance = int.Parse(msgtokens[nextToken++]);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);               
            }
        }
    }
}
