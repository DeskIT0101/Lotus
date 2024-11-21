using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Data.SqlTypes;
using ServerForLotus;

Server server = new Server();
server.StartServer();
Console.ReadLine();

class Server
{
    List<Client> clients = new List<Client>(); 

    private int port = 33445;
    private string myIP = "192.168.1.71";

    public void StartServer() => WaitConnection();

    
    private async void WaitConnection()
    {
        IPEndPoint IPpoint = new IPEndPoint(IPAddress.Any, port);
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        socket.Bind(IPpoint);
        socket.Listen(1000);

        Console.WriteLine("Server was started without problem....");

        while (true)
        {
            Socket clientSocket = await socket.AcceptAsync();
            

            Console.WriteLine($"{DateTime.Now.ToString()}>> The client is connection: {clientSocket.RemoteEndPoint}");

            Client client = new Client(clientSocket, clientSocket.RemoteEndPoint.ToString(), "");
            ListenMessage(client);
            clients.Add(client);

        }
    }

    private async Task ListenMessage(Client client)
    {
        NetworkStream clientThread = new NetworkStream(client.socket);

        while (true)
        {
            byte[] buffer = new byte[1024];
            var bytes = await clientThread.ReadAsync(buffer, 0, buffer.Length);
            string responce = Encoding.UTF8.GetString(buffer);
            Console.WriteLine($"{client.socket.RemoteEndPoint}: " + responce);
        }
    }
}
