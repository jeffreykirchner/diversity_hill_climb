using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class player
    {
        public socketPlayer sp = new socketPlayer();
        public int inumber;
        public string name;
        public string studentID;

        //send message to the client
        public void sendMessage(string index, string message)
        {
            try
            {
                if (sp == null) return;
                sp.send(index, message);
            }
            catch (Exception ex)
            {

            }
        }

        //send begin message to client
        public void sendBegin(string str)
        {
            try
            {
                string outstr = str;

                outstr += Main.numberOfPlayers + ";";
                outstr += Main.numberOfPeriods + ";";
                outstr += Main.instructionX + ";";
                outstr += Main.instructionY + ";";
                outstr += Main.windowX + ";";
                outstr += Main.windowY + ";";
                outstr += Main.showInstructions + ";";
                outstr += inumber + ";";
                outstr += Main.testMode + ";";

                sendMessage("BEGIN", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error endEarly:", ex);
            }
        }

        public void sendInvalidConnection()
        {
            try
            {
                sendMessage("INVALID_CONNECTION", "");
            }
            catch (Exception ex)
            {
               EventLog.appEventLog_Write("error SendInvalidConnection:", ex);
            }
        }
        
        //kill the clients
        public void sendreset()
        {
            try
            {
                string outstr = "";
                sendMessage("RESET", outstr);
            }
            catch (Exception ex)
            {
            }
        }

        public void sendTextMessage(string str)
        {
            try
            {
                string outstr = "";

                outstr += str + ";";

                sendMessage("01", outstr);
            }
            catch (Exception ex)
            {
            }
        }


    }
}
