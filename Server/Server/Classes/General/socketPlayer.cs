using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    //custom event arguments for raising receive events
    public class ListEventArgs : EventArgs
    {
        public List<string> Data { get; set; }

        public ListEventArgs(List<string> data)
        {
            Data = data;
        }
    }

    public class socketPlayer
    {

        public event EventHandler<ListEventArgs> messageReceived;              //throw event when new message is received

        public Socket socketHandler;                                           //socket for each player
        public int inumber;                                                    //id of player this belongs to
        public bool stopping = false;                                          //if true stop listening     
        public string remoteComputerName = "";                                 //remote computer's name

        byte[] bytes = new byte[1024];                                  //byte array to hold incoming data
        Thread receiveThread;                                           //thread the reciver is running on          
        //int messageCouter=0;                                            //keeps count of incoming messages, should line up with the number clients thinks it sent
        
        string data=string.Empty;                                       //incoming data received, could be split across messages 

        //called when a new client connects to start the receive loop on new thread
        public void startReceive()
        {
            try
            {
                receiveThread = new Thread(receive);
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //message receiving loop
        void receive()
        {
            List<string> list = new List<string>();
            int bytesRec;

            string[] tempa = new string[] { "<EOF>" };
            string[] msgtokens;

            while (!stopping)
            {
                bytes = new byte[1024];
                bytesRec = socketHandler.Receive(bytes,bytes.Length,SocketFlags.None);

                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                if (data.IndexOf("<EOF>") > -1)
                {                   
                   msgtokens = data.Split(tempa, StringSplitOptions.None);

                    for(int i = 1; i <= msgtokens.Length - 1; i++)
                    {
                        //EventArgs e = null;

                        list = new List<string>();

                        list.Add(inumber.ToString());
                        list.Add(data);

                        messageReceived?.Invoke(this,new ListEventArgs(list));
                        
                    }

                    data = msgtokens[msgtokens.Length - 1];
                }

            }
        }

        public void send(string id, string str)
        {
            byte[] msg = Encoding.ASCII.GetBytes(id + "<SEP>" + str + "<EOF>");

            socketHandler.Send(msg);
        }
    }
}
