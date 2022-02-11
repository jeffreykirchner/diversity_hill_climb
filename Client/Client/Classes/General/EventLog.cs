using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Client
{
    public static class EventLog
    {
        static StreamWriter eventLog;

        //intialize the the event log
        public static void AppEventLog_Init()
        {
            string filename = "";

            string tempTime = DateTime.Now.Month + "-" +
                             DateTime.Now.Day + "-" +
                             DateTime.Now.Year + "_" +
                             DateTime.Now.Hour + "_" +
                             DateTime.Now.Minute + "_" +
                             DateTime.Now.Second + "." +
                             DateTime.Now.Millisecond;

            filename = "ClientLog_" + tempTime + ".txt";
            filename = Application.StartupPath + @"\Logs\" + filename;

            eventLog = File.CreateText(filename);
            eventLog.AutoFlush = true;
        }

        static string oldText = "";
        public static void appEventLog_Write(string text)
        {
            try
            {
                if (eventLog == null) AppEventLog_Init();

                //prevent run away logging
                if (oldText == text)
                    return;

                oldText = text;

                eventLog.WriteLine(text + " (" + DateTime.Now + ")");                
            }
            catch 
            {

            }
        }
        static string lastError = "";
        public static void appEventLog_Write(string text, Exception err)
        {
            try
            {
                if (eventLog == null) AppEventLog_Init();

                string outstr = null;

                //check for run away logs
                if (err.Message == lastError)
                    return;
                lastError = err.Message;

                eventLog.WriteLine(text + " (" + DateTime.Now + ")");

                outstr = err.Message + Environment.NewLine;
                outstr += err.StackTrace + Environment.NewLine;

                eventLog.WriteLine(outstr);
                Common.FrmClient.SC.sendMessage("CLIENT_ERROR", outstr + ";");
            }
            catch
            {
            }
        }

        public static void AppEventLog_Close()
        {
            if (eventLog != null) eventLog.Close();
        }
    }
}
