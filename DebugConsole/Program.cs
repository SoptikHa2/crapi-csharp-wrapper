using System;
using CRAPI;

namespace DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get player with ID 80RGVCV9C
            Player player = Player.GetPlayer("80RGVCV9C");
            // Get clan with ID 22Y802
            Clan clan = Clan.GetClan("22Y802");

            // Write player's name by using player.name
            Console.WriteLine($"{player.name}'s current deck:\n");

            // Go thru all cards in player's deck
            foreach(Card card in player.currentDeck)
            {
                // Write card name and card elixir cost
                Console.WriteLine($"{card.name} ({card.elixir})");
            }

            Console.WriteLine("\n\n");
            // Again write player's name
            Console.WriteLine($"{player.name}'s current arena:");
            // Write player's arena name, player's trophies and player's arena's trophy limit
            Console.WriteLine($"{player.arena.name} ({player.trophies}/{player.arena.trophyLimit})");


            Console.WriteLine("\n\n");
            // Get top players
            TopPlayers topPlayers = ClashRoyale.GetTopPlayers();
            // Get top clans
            TopClans topClans = ClashRoyale.GetTopClans();
            Console.WriteLine("Top players:");
            // List top players
            foreach (Player p in topPlayers.players)
                Console.WriteLine(p); // Use Player.ToString(), that returns player's name

            Console.WriteLine("\n\nTop clans:");
            // List top clans
            foreach (Clan c in topClans.clans)
                Console.WriteLine(c); // Use Clan.ToString(), that returns clan's name
            
            Console.ReadLine();
        }
    }
}
