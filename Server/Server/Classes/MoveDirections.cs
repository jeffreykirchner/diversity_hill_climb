using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class MoveDirections
    {
        public string direction;             //left or right
        public int distance;                 //distance gone
        

        public string toString()
        {
            try
            {
                string str = "";

                str = direction + ";";
                str += distance + ";";

                return str;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return "";
            }
        }
    }
}
