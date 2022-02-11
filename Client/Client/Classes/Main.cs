using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public static class Main
    {
        public static string sfile;
        public static string myIPAddress = "localhost";
        public static int myPortNumber = 12345;
        public static int inumber;
        public static bool clientClosing = false;

        //forms
        public static frmConnect FrmConnect=new frmConnect();
        public static frm1 Frm1;

        //keeps track of message count
        public static int messageCounter = 0;
        public static int[] playerMessageCounter = new int[101];

        //global parameters
        public static int numberOfPlayers;                     //number of players needed
        public static int numberOfPeriods;                     //number of periods in the experiment         
        public static int instructionX;                        //x location of intstruction window
        public static int instructionY;                        //y location of intstruction window
        public static int windowX;                             //x location of main window
        public static int windowY;                             //y location of main window
        public static bool showInstructions;                   //show instructions before experiment starts
        public static bool testMode;                           //turn on auto test system

        public static void start()
        {
            sfile = Application.StartupPath + @"\client.ini";

            myIPAddress = INI.getINI(sfile, "Settings", "ip").ToString();
            myPortNumber = int.Parse(INI.getINI(sfile, "Settings", "port"));
            
            EventLog.AppEventLog_Init();
        }

        public static void takeMessage(List<string> sinstr)
        {

            try
            {
                string str = sinstr[0];

                string[] tempa = { "<SEP>" };
                string[] msgtokens = str.Split(tempa, StringSplitOptions.None);

                string id = msgtokens[0];
                string message = msgtokens[1];

                switch (id)
                {
                    case "SHOW_NAME":
                        // takeShowName(message);
                        break;
                    case "RESET":
                        takeReset(message);
                        break;
                    case "END_EARLY":
                        //   takeEndEarly(msgtokens(1));
                        break;
                    case "FINISHED_INSTRUCTIONS":
                        //  takeFinishedInstructions(message);
                        break;
                    case "INVALID_CONNECTION":
                        takeInvalidConnection(message);
                        break;
                    case "BEGIN":
                        takeBegin(message);
                        break;
                    case "01":
                        takeClientMessage(message);
                        break;
                    case "02":

                        break;
                    case "03":

                        break;
                    case "04":

                        break;
                    case "05":

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

        static void takeBegin(string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                numberOfPlayers = int.Parse(msgtokens[nextToken]);
                nextToken += 1;

                numberOfPeriods = int.Parse(msgtokens[nextToken]);
                nextToken += 1;

                instructionX = int.Parse(msgtokens[nextToken]);
                nextToken += 1;

                instructionY = int.Parse(msgtokens[nextToken]);
                nextToken += 1;

                windowX = int.Parse(msgtokens[nextToken]);
                nextToken += 1;

                windowY = int.Parse(msgtokens[nextToken]);
                nextToken += 1;

                showInstructions = bool.Parse(msgtokens[nextToken]);
                nextToken += 1;

                inumber = int.Parse(msgtokens[nextToken]);
                nextToken += 1;

                testMode = bool.Parse(msgtokens[nextToken]);
                nextToken += 1;

                Program.frmClient.Hide();

                Frm1 = new frm1();
                Frm1.Show();
                Frm1.timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        static void takeReset(string str)
        {
            try
            {
                //string[] msgtokens = str.Split(';');
                //int nextToken = 0;

                closeClient();
                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }


        public static void closeClient()
        {
            try
            {
                var fc = Program.frmClient;

                fc.Timer1.Enabled = false;
                clientClosing = true;

                fc.bwSocket.CancelAsync();
                fc.SC.close();

                //.Close()
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                Program.frmClient.Close();
            }
        }

        public static void takeInvalidConnection(string str)
        {
            try
            {
               List<string> list = new List<string>();

               Program.frmClient.SC_ConnectionError(null, new ListEventArgs(list));
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        static void takeClientMessage(string message)
        {
            try
            {
                string[] msgtokens = message.Split(';');
                int nextToken = 0;

                string tempId = msgtokens[nextToken++];
                string tempMessage = msgtokens[nextToken++];

                Frm1.textBox1.Text = "Client " + tempId + ": " + tempMessage + Environment.NewLine +
                                                 Frm1.textBox1.Text;

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
        
    }
}
