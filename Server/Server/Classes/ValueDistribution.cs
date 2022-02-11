using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ValueDistribution
    {
        public int valueRangeStart;
        public int valueRangeEnd;
        public int drawStart;
        public int drawEnd;

        public void fromString(string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                valueRangeStart = int.Parse(msgtokens[nextToken++]);
                valueRangeEnd = int.Parse(msgtokens[nextToken++]);
                drawStart = int.Parse(msgtokens[nextToken++]);
                drawEnd = int.Parse(msgtokens[nextToken++]);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
