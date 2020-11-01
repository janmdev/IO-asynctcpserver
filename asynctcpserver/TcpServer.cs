using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace asynctcpserver
{
    public abstract class TcpServer
    {
        protected TcpListener listener;
        protected TcpClient client;
        protected byte[] buffer;

        protected TcpServer(IPAddress iPAddress, int port, int bufferSize)
        {
            listener = new TcpListener(iPAddress, port);
            buffer = new byte[bufferSize];
        }

        public abstract void RunServer();
        public void StopServer()
        {
            try
            {
                client.Close();
            }
            catch(NullReferenceException )
            {}
        }

        protected int Silnia(int n)
        {
            int suma = 1;
            for (int i = 1; i <= n; i++)
            {
                suma *= i;
            }
            return suma;
        }
    }
}
