using System;
using CRAPI;

namespace DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize wrapper with your dev key
            Wrapper wr = new Wrapper("your_secret_key");

            // Get player with ID 80RGVCV9C
            Player player = wr.GetPlayer("80RGVCV9C");
            // Get clan with ID 22Y802
            Clan clan = wr.GetClan("22Y802");

            Console.WriteLine(player.name);
            Console.WriteLine(clan.name);

            // Write cards in player's deck
            foreach (Card card in player.currentDeck)
                Console.WriteLine(card);

            Console.ReadKey();
        }
    }
}
