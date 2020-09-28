using System;
using System.Threading;

namespace ProxyTibia
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread LoginThread = new Thread(SetupLoginServer);
            LoginThread.Start();

            Thread GameThread = new Thread(SetupGameServer);
            GameThread.Start();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }



        static void SetupLoginServer()
        {
            Server LoginServer = new Server("127.0.0.1", 7170, "127.0.0.1", 7171);
            LoginServer.StartUp();
        }

        static void SetupGameServer()
        {
            Server GameServer = new Server("127.0.0.1", 7172, "localhost", 7172);
            GameServer.StartUp();
        }

    }
}
