using System;

namespace ProxyTibia
{
    class Program
    {
        static void Main(string[] args)
        {
            Server s = new Server("127.0.0.1", 7170, 7171);
            s.StartUp();
            Console.ReadLine();

            Console.WriteLine("Hello World!");
        }
    }
}
