using System;
using CRAPI;

namespace DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Player.GetPlayer("80RGVCV9C").name);
            Console.ReadKey();
        }
    }
}
