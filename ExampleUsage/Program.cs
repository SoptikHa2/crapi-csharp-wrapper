using System;
using CRAPI;

namespace ExampleUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize wrapper with your dev key
            Wrapper wr = new Wrapper("your secret key");

            string tag = Console.ReadLine();

            Player player = wr.GetPlayer(tag);

            Console.WriteLine($"{player.name} (XP level {player.stats.level}) ({player.arena.name})");
            Console.WriteLine($"{player.clan.name} ({player.clan.role})");

            Card[] cards = player.currentDeck;

            foreach(Card c in cards)
            {
                Console.WriteLine($"{c.name} {c.elixir} ({c.rarity}) --- level: {c.level}");
            }

            Console.ReadKey();
        }
    }
}
