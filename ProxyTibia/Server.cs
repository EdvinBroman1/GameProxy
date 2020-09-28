using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace ProxyTibia
{
    class Server
    {
        Socket clientSocket;

        public string m_IPAddress { get; set; }
        public int m_Port { get; set; }

        public string m_RemoteIP { get; set; }
        public int m_RemotePort { get; set; }

        public Client m_Client { get; set; }


        public Server(string IP, int Port, string RemoteIP, int RemotePort) // IP can be submitted in number or domain form
        {
            this.m_IPAddress = IP;
            this.m_Port = Port;
            this.m_RemoteIP = RemoteIP;
            this.m_RemotePort = RemotePort;
            this.m_Client = new Client(this.m_IPAddress, RemotePort,this);
            
        }

        public void SendToClient(byte[] buffer)
        {
            clientSocket.Send(buffer);
        }

        public void StartUp()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(m_IPAddress);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, m_Port);

            Socket listener = new Socket(ipAddr.AddressFamily,  SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                Console.WriteLine($"Server setup.. awaiting connection on port -> {m_Port}... ");
                clientSocket = listener.Accept(); // next packet...
                while (true)
                {


                    byte[] bytes = new Byte[512]; 
                    int bytecount = clientSocket.Receive(bytes);
                    byte[] btRecvData = new Byte[bytecount];
                    Array.Copy(bytes, 0, btRecvData, 0, bytecount);

                    if (btRecvData != null)
                    {
                        Console.WriteLine("TibiaClient->TibiaServer");
                        Console.WriteLine(Print.HexDump(btRecvData));
                        m_Client.SendToServer(btRecvData);
                    }

                    clientSocket = listener.Accept();

                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}