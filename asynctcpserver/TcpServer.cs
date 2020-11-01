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
        private TcpListener listener;
        private int bufferSize;
        private NetworkStream stream;
        private List<Tuple<IPAddress, DateTime, bool>> endPointHistory;
        protected NetworkStream Stream { get => stream; set => stream = value; }
        protected List<Tuple<IPAddress, DateTime, bool>> EndPointHistory { get => endPointHistory; set => endPointHistory = value; }
        protected int BufferSize { get => bufferSize; set => bufferSize = value; }
        protected TcpListener Listener { get => listener; set => listener = value; }

        protected TcpServer(IPAddress iPAddress, int port, int bufferSize)
        {
            Listener = new TcpListener(iPAddress, port);
            this.BufferSize = bufferSize;
            EndPointHistory = new List<Tuple<IPAddress,DateTime, bool>>();
        }

        public abstract void RunServer();

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
