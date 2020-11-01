using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace asynctcpserver
{
    public class AsyncTcpServer : TcpServer
    {
        private delegate void TransmissionDataDelegate(NetworkStream stream);
        private delegate TcpClient RunServerDelegate(TcpClient client);

        public AsyncTcpServer(IPAddress ipAddres, int port, int bufferSize) : base(ipAddres, port, bufferSize) { }

        public override void RunServer()
        {
            Listener.Start();
            TcpClient tcpClient = new TcpClient();
            RunServerDelegate serverDelegate = new RunServerDelegate(BeginRunServer);
            serverDelegate.BeginInvoke(tcpClient, RunServerCallback, new object[] { serverDelegate });
            while(true)
            {}

        }

        private void RunServerCallback(IAsyncResult ar)
        {
            
            var args = (object[])ar.AsyncState;
            RunServerDelegate del = (RunServerDelegate)args[0];
            TcpClient client = del.EndInvoke(ar);
            del.BeginInvoke(client, RunServerCallback, new object[] { del });
            
        }

        private TcpClient BeginRunServer(TcpClient tcpClient)
        {
            if (tcpClient is null) return null;
            else if (!tcpClient.Connected)
            {
                tcpClient = Listener.AcceptTcpClient();
                RunServerDelegate serverDelegate = new RunServerDelegate(BeginRunServer);
                serverDelegate.BeginInvoke(new TcpClient(), RunServerCallback, new object[] { serverDelegate });
                this.EndPointHistory.Add(new Tuple<IPAddress, DateTime, bool>(((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address, DateTime.Now, true));
                Console.WriteLine("Połączenie: \t" + ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString() + " : " + DateTime.Now.ToString());
            }
            NetworkStream str = tcpClient.GetStream();
            byte[] buffer = new byte[BufferSize];
            string message = String.Empty;
            try
            {
                str.Read(buffer, 0, BufferSize);
                message = Encoding.ASCII.GetString(buffer).Trim().Replace("\0", String.Empty);
                int number = int.Parse(message);
                string silnia = this.Silnia(number).ToString();
                str.Write(Encoding.ASCII.GetBytes(silnia), 0, silnia.Length);
            }
            catch (IOException)
            { }
            catch (FormatException formatEx)
            {
                if (message.Trim() == "q")
                {
                    this.EndPointHistory.Add(new Tuple<IPAddress, DateTime, bool>(((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address, DateTime.Now, true));
                    Console.WriteLine("Rozłączenie: \t" + ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString() + " : " + DateTime.Now.ToString());
                    tcpClient.Close();
                }
            }
            
            return tcpClient;
        }

    }
}
