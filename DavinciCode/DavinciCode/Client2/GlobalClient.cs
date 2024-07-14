using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client2
{
    internal class GlobalClient
    {
        public static TcpClient Client = new TcpClient();
        public static NetworkStream Stream;
    }
}
