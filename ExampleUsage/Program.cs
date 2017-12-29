using System;
using System.Threading.Tasks;
using CRAPI;

namespace ExampleUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Executing async requests. Press any key to end async testing and start sync testing.");
            DoSomethingAsync();
            Console.ReadKey();
            Console.Clear();


            // Initialize wrapper with your dev key
            Wrapper wr = new Wrapper("your secret key");

            string tag = "80RGVCV9C";

            Player player = wr.GetPlayer(tag);

            Console.WriteLine($"{player.name} (XP level {player.stats.level}) ({player.arena.name})");
            Console.WriteLine($"{player.clan.name} ({player.clan.role})");

            Card[] cards = player.currentDeck;

            foreach(Card c in cards)
            {
                Console.WriteLine($"{c.name} {c.elixir} ({c.rarity}) --- level: {c.level}");
            }

            Console.WriteLine("Searching for clan with name abc and at least 10 members.");

            // Parameters: name, min score, min members, max members. When is some parameter null, wrapper will not search using the parameter. At least
            // one parameter must not be null.
            SimplifiedClan[] searchResult = wr.SearchForClans("abc", null, 10, null);

            Console.WriteLine($"First clan is \"{searchResult[0]}\" with {wr.GetClan(searchResult[0].tag).memberCount} members.");

            Console.ReadKey();
        }

        
        static async void DoSomethingAsync()
        {
            Wrapper wr = new Wrapper("your secret key");

            string tag = "80RGVCV9C";
            string ctag = "22Y802";

            Console.WriteLine("I'll get one player profile, one clan profile, best players and best clans async!");
            
            Task<Player> a_player = wr.GetPlayerAsync(tag);
            Task<Clan> a_clan = wr.GetClanAsync(ctag);
            Task<SimplifiedPlayer[]> a_topPlayers = wr.GetTopPlayersAsync(); // wr.GetTopPlayers() and its async version return SimplifiedPlayer -> this is just like Player,
                                                                             // but simplified with less properties. If you want to get complete overview, get the top player:
                                                                             // Player topPlayer = wr.GetPlayer(wr.GetTopPlayers()[0].tag)
            Task<SimplifiedClan[]> a_topClans = wr.GetTopClansAsync(); // Here is the same thing as with GetTopPlayers()


            // Wait untill everything is prepared. You can for example write dots into console
            while (!a_player.IsCompleted || !a_clan.IsCompleted || !a_topPlayers.IsCompleted || !a_topClans.IsCompleted)
            {
                Console.Write(".");
                System.Threading.Thread.Sleep(50);
            }



            Console.WriteLine("Done!");

            Player player = await a_player;
            Clan clan = await a_clan;
            SimplifiedPlayer[] topPlayers = await a_topPlayers;
            SimplifiedClan[] topCLans = await a_topClans;

            Console.WriteLine(player.name);
            Console.WriteLine(clan.name);
            Console.WriteLine(topPlayers[0].name);
            Console.WriteLine(topCLans[0].name);
        }
    }
}
