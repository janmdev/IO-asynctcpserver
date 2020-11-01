using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using asynctcpserver;

namespace serverapp
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncTcpServer asyncTcpServer = new AsyncTcpServer(IPAddress.Any, 2048, 1024);
            asyncTcpServer.RunServer();
        }
    }
}
