using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Player
    {


        public string colorName="";                     //string color name
        public int myID=0;                              //id number 


        public void FromString(ref string[] msgtokens, ref int nextToken,int myID)
        {
            try
            {
                this.myID = myID;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

    }
}
