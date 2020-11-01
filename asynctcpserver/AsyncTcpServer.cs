using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace asynctcpserver
{
    public class AsyncTcpServer : TcpServer
    {
        public AsyncTcpServer(IPAddress ipAddres, int port, int bufferSize) : base(ipAddres, port, bufferSize) { }

        public override void RunServer()
        {
            listener.Start();
            client = listener.AcceptTcpClient();
        }
    }
}
