using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ProxyTibia
{
    class Client
    {
        Socket sender; 

        public string m_IPAddress { get; set; }
        public int m_Port { get; set; }
        public Server m_Server { get; set; }

        public Client(string IP, int Port, Server server) 
        {
            this.m_IPAddress = IP;
            this.m_Port = Port;
            this.m_Server = server;
          //  this.ConnectToTibiaServer();
        }

        public void ConnectToTibiaServer()
        {
            IPHostEntry IpAddr = Dns.GetHostEntry(m_IPAddress);
            IPAddress ip = IpAddr.AddressList[0];
            IPEndPoint EndPoint = new IPEndPoint(ip, m_Port);

            sender = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sender.Connect(EndPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



        public void SendToServer(byte[] buffer)
        {
            this.ConnectToTibiaServer();
            sender.Send(buffer);

            byte[] messageReceived = new byte[512];
            int byteRecv = sender.Receive(messageReceived);
            // resizing, client crashes if packets are too large.
            byte[] btRecvData = new byte[byteRecv];
            Array.Copy(messageReceived, 0, btRecvData, 0, byteRecv);

            Console.WriteLine("TibiaServer->TibiaClient");
             Console.WriteLine(Print.HexDump(btRecvData));

            m_Server.SendToClient(btRecvData);


        }

    }
}
