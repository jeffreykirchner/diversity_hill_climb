using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class frmMain : Form
    {       
        public SocketClient SC ;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
           
            Common.start();
            setupSC();
        }

        //socket setup  
        public void setupSC()
        {
            try
            {
                SC = new SocketClient();
                SC.messageReceived += new EventHandler<ListEventArgs>(setTakeMessage);
                SC.connected += new EventHandler<ListEventArgs>(SC_connected);
                SC.connectionError += new EventHandler<ListEventArgs>(SC_ConnectionError);

                SC.connect();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void SC_connected(object sender, ListEventArgs e)
        {
            try
            {
                bwSocket.RunWorkerAsync();
            }
            catch (Exception ex)
            {
               EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void SC_ConnectionError(object sender, ListEventArgs e)
        {

            try
            {
                if (bwSocket.IsBusy)
                {
                    bwSocket.CancelAsync();
                }
                else
                {
                    showConnectionBox();
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void bwSocket_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                bool go = true;

                //receive messages from server until canceled
                while (go)
                {
                    if (bwSocket.CancellationPending) go = false;
                    if (go) SC.Receive();
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void bwSocket_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!Common.clientClosing)
            {
                showConnectionBox();
            }
            else
            {
                Close();
            }
        }

        public void showConnectionBox()
        {
            try
            {
                if (SC.client.Connected)
                {
                    SC.close();
                }

                SC = new SocketClient();

                Common.FrmConnect.Show();
            }
            catch (Exception ex)
            {
               EventLog.appEventLog_Write("error :", ex);
            }
        }

        //tread safe handling of incoming message from a client
        delegate void StringArgReturningVoidDelegate(object sender, ListEventArgs e);

        public void setTakeMessage(object sender, ListEventArgs e)
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
                Common.takeMessage(e.Data);
            }
        }

        

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { 
                if (e.Alt == true)
                {
                    if (Convert.ToInt32(e.KeyValue) == Convert.ToInt32(Keys.K))
                    {
                        if (MessageBox.Show("Close Program?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;

                        Common.closeClient();
                    }
                    else if (Convert.ToInt32(e.KeyValue) == Convert.ToInt32(Keys.Q))
                    {
                        Common.FrmConnect.Show();
                    }
                }
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

        private void Timer1_Tick(object sender, EventArgs e)
        {
           
            Timer1.Enabled = false;
        }
    }


}
