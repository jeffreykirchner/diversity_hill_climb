using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Server
{
    
    public static class EventLog
    {
        static StreamWriter eventLog;

        //intialize the the event log
        public static void AppEventLog_Init()
        {
            string filename="";

            string tempTime = DateTime.Now.Month + "-" +
                             DateTime.Now.Day + "-" +
                             DateTime.Now.Year + "_" +
                             DateTime.Now.Hour + "_" +
                             DateTime.Now.Minute + "_" +
                             DateTime.Now.Second;

            filename = "ServerLog_" + tempTime + ".txt";
            filename = Application.StartupPath + @"\Logs\" + filename;

            eventLog = File.CreateText(filename);
            eventLog.AutoFlush = true;
        }

        public static void appEventLog_Write(string text)
        {

        }

        public static void appEventLog_Write(string text, Exception ex)
        {

        }

        public static void AppEventLog_Close()
        {
            if (eventLog != null) eventLog.Close();
        }
    }
}
