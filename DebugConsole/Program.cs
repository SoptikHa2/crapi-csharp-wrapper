using System;
using CRAPI;

namespace DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = Player.GetPlayer("80RGVCV9C");
            Console.WriteLine($"{player.name}'s current deck:\n");

            foreach(Card card in player.currentDeck)
            {
                Console.WriteLine($"{card.name} ({card.elixir})");
            }

            Console.WriteLine("\n\n");
            Console.WriteLine($"{player.name}'s current arena:");
            Console.WriteLine($"{player.arena.name} ({player.trophies}/{player.arena.trophyLimit})");
            
            Console.ReadLine();
        }
    }
}
