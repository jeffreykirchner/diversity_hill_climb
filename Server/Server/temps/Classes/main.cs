using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public static class Main
    {
        public static string sfile;                               //location of server.ini       
        public static int clientCount = 0;                        //number of connected clients
        public static player[] playerlist = new player[1000];     //list of connected clients

        //global parameters
        public static int numberOfPlayers;                     //number of players needed
        public static int numberOfPeriods;                     //number of periods in the experiment  
        public static int portNumber;                          //port for socket communication 
        public static int instructionX;                        //x location of intstruction window
        public static int instructionY;                        //y location of intstruction window
        public static int windowX;                             //x location of main window
        public static int windowY;                             //y location of main window
        public static bool showInstructions;                   //show instructions before experiment starts
        public static bool testMode;                           //turn on auto test system

        public static void start()
        {
            sfile = Application.StartupPath + @"\server.ini";
            EventLog.AppEventLog_Init();

            loadParameters();
        }

        public static void loadParameters()
        {
            numberOfPlayers =int.Parse(INI.getINI(sfile, "gameSettings", "numberOfPlayers"));
            numberOfPeriods = int.Parse(INI.getINI(sfile, "gameSettings", "numberOfPeriods"));
            portNumber = int.Parse(INI.getINI(sfile, "gameSettings", "port"));
            instructionX = int.Parse(INI.getINI(sfile, "gameSettings", "instructionX"));
            instructionY = int.Parse(INI.getINI(sfile, "gameSettings", "instructionY"));
            windowX = int.Parse(INI.getINI(sfile, "gameSettings", "windowX"));
            windowY = int.Parse(INI.getINI(sfile, "gameSettings", "windowY"));
            showInstructions =bool.Parse(INI.getINI(sfile, "gameSettings", "showInstructions"));
            testMode = bool.Parse(INI.getINI(sfile, "gameSettings", "testMode"));
        }

        //process incoming message from a client
        public static void takeMessage(List<string> sinstr)
        {
            try
            {

                int index = int.Parse(sinstr[0]);
                string str = sinstr[1];

                string[] tempa = new string[4];
                tempa[1] = "<SEP>";
                tempa[2] = "<EOF>";
                string[] msgtokens = str.Split(tempa, 3, StringSplitOptions.None);

                string id = msgtokens[0];
                string message = msgtokens[1];

                switch (id)
                {
                    case "COMPUTER_NAME":
                        //takeRemoteComputerName(index, message);
                        break;
                    case "SUBJECT_NAME":
                        //takeName(index, message);
                        break;
                    case "FINSHED_INSTRUCTIONS":
                        //takeFinishedInstructions(index, message);
                        break;
                    case "INSTRUCTION_PAGE":
                        //takeInstructionPage(index, message);
                        break;
                    case "01":
                        takeTextMessage(index,message);
                        break;
                    case "02":

                        break;
                    case "03":

                        break;
                    case "04":

                        break;
                    case "05":

                        break;
                    case "06":
                        break;
                    case "07":
                        break;
                    case "08":
                        break;
                    case "09":
                        break;
                    case "10":

                        break;
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        static void takeTextMessage(int index, string message)
        {
            try
            {
                string[] msgtokens = message.Split(';');
                int nextToken = 0;

                //int tempId = int.Parse(msgtokens[nextToken++]);
                string tempMessage = msgtokens[nextToken++];

                string outstr = "";

                outstr += index + ";";
                outstr += tempMessage + ";";

                for (int i = 1; i <= Main.numberOfPlayers; i++)
                {
                    Main.playerlist[i].sendTextMessage(outstr);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }       

    }
}
