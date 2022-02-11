using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
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

    public class SocketClient
    {
        //events
        public event EventHandler<ListEventArgs> connected;
        public event EventHandler<ListEventArgs> connectionError;
        public event EventHandler<ListEventArgs> messageReceived;

        //socket connection
        public Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //connection information
        //IPHostEntry ipHostInfo = default(IPHostEntry);
        IPAddress ipAddress = default(IPAddress);
        // IPEndPoint remoteEP = default(IPEndPoint);

        string data = "";                              //incoming data
        List<string> list = new List<string>();        //incoming data list

        public void connect()
        {

            try
            {
                IPHostEntry resolved = Dns.GetHostEntry(Common.myIPAddress);

                for (int i = 1; i <= resolved.AddressList.Length; i++)
                {
                    if (resolved.AddressList[i - 1].ToString().IndexOf(".") > -1)
                    {
                        ipAddress = resolved.AddressList[i - 1];

                        break; // TODO: might not be correct. Was : Exit For
                    }
                }

                IPEndPoint remoteEP = new IPEndPoint(ipAddress, Common.myPortNumber);

                // Connect to the remote endpoint.
                client.Connect(remoteEP);

                List<string> list = new List<string>();
                connected?.Invoke(this, new ListEventArgs(list));
            }
            catch
            {
                List<string> list = new List<string>();
                connectionError?.Invoke(this, new ListEventArgs(list));
            }
        }

        public void close()
        {
            // Release the socket.
            //If Not client.Connected Then Exit Sub

            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        public void sendMessage(string id, string message)
        {
            // Send  data to the remote device.
            Send(id, message);
        }

        private void Send(string id, string str)
        {
            try
            {
                // Convert the string data to byte data using ASCII encoding.
                byte[] msg = Encoding.ASCII.GetBytes(id + "<SEP>" + str + "<EOF>");

                // Begin sending the data to the remote device.
                client.Send(msg,msg.Length,SocketFlags.None);
            }
            catch
            {
                List<string> list = new List<string>();
                connectionError?.Invoke(this, new ListEventArgs(list));
            }

        }

        public void Receive()
        {
            try
            {
                // Create the state object.
                byte[] bytes = new byte[1025];

                // Begin receiving the data from the remote device.
                int bytesRec = client.Receive(bytes);

                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                //if end of message found, extract it
                if (data.IndexOf("<EOF>") > -1)
                {
                    string[] tempa = { "<EOF>" };
                    string[] msgtokens = data.Split(tempa, StringSplitOptions.None);

                    for (int i = 1; i <= msgtokens.Length - 1; i++)
                    {
                        //raise message received event
                        list = new List<string>();
                        list.Add(msgtokens[i - 1]);

                        messageReceived?.Invoke(this, new ListEventArgs(list));
                    }

                    data = msgtokens[msgtokens.Length - 1];
                }
                else
                {
                    //take next message
                    Receive();
                }
            }
            catch
            {
                List<string> list = new List<string>();
                connectionError?.Invoke(this, new ListEventArgs(list));
            }
        }


    }
}
