using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public partial class frmMain : Form
    {
        //network variables
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = default(IPAddress);
        IPEndPoint localEndPoint = default(IPEndPoint);

        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        bool resetPressed = false;

        public frmMain()
        {
            InitializeComponent();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            Main.start();

            //find the IPv4 address
            for (int i = 1; i <= ipHostInfo.AddressList.Length; i++)
            {
                if (ipHostInfo.AddressList[i - 1].ToString().IndexOf(".") > -1)
                {
                    ipAddress = ipHostInfo.AddressList[i - 1];
                    break; 
                }
            }

            //start network listener
            localEndPoint = new IPEndPoint(IPAddress.Any,Main.portNumber);

            listener.Bind(localEndPoint);
            listener.Listen(10);

            bwTakeSocketConnections.RunWorkerAsync();

            //display network info on form
            lblIpAddress.Text = ipAddress.ToString();
            lblLocalHost.Text = SystemInformation.ComputerName;
            lblConnectionCount.Text = "0";           
        }

        private void cmdBegin_Click(object sender, EventArgs e)
        {
            try
            {           
                //send signal to begin to clients
                string outstr = "";

                for (int i = 1; i <= Main.numberOfPlayers; i++)
                {
                    Main.playerlist[i].sendBegin(outstr);
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }

        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            try
            {

                for (int i = 1; i <= Main.clientCount; i++)
                {
                    Main.playerlist[i].sendreset();
                }

                bwTakeSocketConnections.CancelAsync();
                listener.Close();

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSetup1_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSetup2_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSetup3_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSetup4_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSetup5_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdEndEarly_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdExchange_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
            
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            EventLog.AppEventLog_Close();
        }

        private void bwTakeSocketConnections_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                bool go = true;

                while (go)
                {
                    Socket tempSocket = listener.Accept();

                    Main.clientCount += 1;
                    Main.playerlist[Main.clientCount] = new player();

                    Main.playerlist[Main.clientCount].sp.socketHandler = tempSocket;

                    Main.playerlist[Main.clientCount].sp.startReceive();

                    Main.playerlist[Main.clientCount].sp.messageReceived += new EventHandler<ListEventArgs>(setTakeMessage);

                    Main.playerlist[Main.clientCount].inumber = Main.clientCount;
                    Main.playerlist[Main.clientCount].sp.inumber = Main.clientCount;

                    refreshConnectionsLabel();

                    if (cmdBegin.Enabled == false)
                    {
                        Main.playerlist[Main.clientCount].sendInvalidConnection();
                        Main.clientCount -= 1;
                    }

                    if (resetPressed)
                        go = false;
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //tread safe handling of incoming message from a client
        delegate void StringArgReturningVoidDelegate(object sender, ListEventArgs e);

        private void setTakeMessage(object sender, ListEventArgs e)
        {
            // InvokeRequired required compares the thread ID of the  
            // calling thread to the thread ID of the creating thread.  
            // If these threads are different, it returns true.  
            if (this.txtMain.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(setTakeMessage);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                Main.takeMessage(e.Data);
            }
        }              

        //thread safe update to number of clients
        delegate void StringArgReturningVoidDelegate2(string text);

        private void setConnectionsLabel(string text)
        {
            // InvokeRequired required compares the thread ID of the  
            // calling thread to the thread ID of the creating thread.  
            // If these threads are different, it returns true.  
            if (this.lblConnectionCount.InvokeRequired)
            {
                StringArgReturningVoidDelegate2 d = new StringArgReturningVoidDelegate2(setConnectionsLabel);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lblConnectionCount.Text = text;
            }
        }

        //upate the number of connected clients
        public void refreshConnectionsLabel()
        {
            try
            {
                setConnectionsLabel(Main.clientCount.ToString());
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void bwTakeSocketConnections_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                txtMain.Text = "";
                txtError.Text = "";

                resetPressed = false;

                for (int i = 1; i <= Main.clientCount; i++)
                {
                    Main.playerlist[i].sp.stopping = true;
                    Main.playerlist[i].sp = null;
                }

                Main.clientCount = 0;

                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                listener.Bind(localEndPoint);
                listener.Listen(10);

                bwTakeSocketConnections.RunWorkerAsync();
                refreshConnectionsLabel();
            }
            catch (Exception ex)
            {
               EventLog.appEventLog_Write("error :", ex);
            }
        }
    }




}
