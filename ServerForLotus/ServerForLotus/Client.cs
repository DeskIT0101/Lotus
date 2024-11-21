using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerForLotus
{
    public class Client
    { 
        public Socket socket;
        public string ClientIP;
        public string ClientName;

        public Client(Socket sckt, string IP, string Name)
        {
            socket = sckt;
            ClientIP = IP;
            ClientName = Name;
        }
    }
}
