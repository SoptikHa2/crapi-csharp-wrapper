using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Add using CRAPI. Make sure to download package Stastny.CRAPI from NuGet.
using CRAPI;

namespace ExampleUsage
{
    class Program
    {
        // Define wrapper instance
        private static Wrapper clashRoyale;

        static void Main(string[] args)
        {
            // There are async versions of every method here. You can call them with async/await.


            // Create wrapper instance. First parameter is your unique API key. Make sure to not share it with anyone!
            //   Request one at API discord. See https://docs.royaleapi.com/#/authentication?id=key-management
            // Second parameter is if you want wrapper to automatically throttle your requests to satisfy API limit. This is set to True by default.
            //   default. This is here because if you try to exceed API limit, it'll temporarily (10 seconds or something like that) disable your account.
            // Thrid parameter is wrapper cache duration, set to 0 by default. If it is set to 0 or negative number, it is disabled.
            //   Caching is good to use if you process a lot of same requests. Wrapper will save responses to these requests and store it in TEMP folder.
            //   Data will not be always up-to-date, even if you disable this cache, as API refreshes data only once every few minutes.
            clashRoyale = new Wrapper("your secret key", true, 0);

            // Now we can get some data! Let's print name of best Clash Royale player!
            // You can even get top players just from some region! First parameter is enum of regions, by default set to worldwide.
            SimplifiedPlayer[] bestPlayers = clashRoyale.GetTopPlayers();
            // SimplifiedPlayer is basically player profile that contains only some information about player, not all of them.
            Console.WriteLine(bestPlayers[0].name);

            // What about printing his deck? But wait, SimplifiedPlayer doesn't contain player's deck!
            // SimplifiedPlayer contains only few information, but it luckily contains player tag. So let's get full player profile.
            // This method accepts player's tag and two `bool` values. Second parameter is, if we want to include chest cycle in player's profile. We don't need it.
            // Third parameter is, if we want to include player battles in player's profile. We don't need it.
            // You have to specify it because these requests are costly (on server side), so they are separated into different endpoints.
            Player bestPlayer = clashRoyale.GetPlayer(bestPlayers[0].tag, false, false);

            // Maybe you noticed more parameters - include and exclude. At least one of them have to be Null!
            // If you use exclude = [ "name", "clan" ], name and clan will not be sent back by server and these fields will have default value.
            // If you use include = [ "tag" ], only tag (and nothing else) will be present in player's profile. You can use this (include and exclude) in any request.


            // Do you have a clan? No? Let's find one! I live in Czech Republic, so let's find all Czech clans that I might like.
            // This returns profiles of clans with any name, at least 20,000 score, between 40-48 members and in Czech Republic. The last parameter
            // is set by default to worldwide.
            SimplifiedClan[] avaiableClans = clashRoyale.SearchForClans(null, 20000, 40, 48, Wrapper.Locations.CZ);

            // You can even get clan wars data!
            ClanWarInfo ongoingWar = clashRoyale.GetClanWar(avaiableClans[0].tag);
            Console.WriteLine(ongoingWar.state); // Print current clan war state. (Collection day, ...)
            ClanWarsRecord[] pastWars = clashRoyale.GetPastClanWars(avaiableClans[0].tag);
            // Lets print how many crowns had winner clan in past clan war!
            Console.WriteLine(pastWars[0].standings[0].crowns);

            // What about open tournaments?
            SimplifiedTournament[] openTournaments = clashRoyale.GetOpenTournaments();
            Console.WriteLine(openTournaments[0].name);

            // This is unique feature of this wrapper - data mining!
            // This example starts mining and printing player tags.
            // It may look messy at first, but take a look at it, or try to do it on your own. It's just collection of functions, what to do.
            // First parameter is initial collection of players (or clans, tournaments, ...) that I'll mine data from
            // Second one is function that receives tag and returns the object. So for example clashRoyale.GetPlayer(tag).
            // Third one is collection functions that get more players from tag (for example: receive tag and return players in clan, players in past games, ...)
            // Fourth one receives tag and returns the thing that you want to mine

            // Player is the thing I work with (I mine players), string is the thing mining returns (player tags)
            var result = clashRoyale.Mine<Player, string>(
                new Player[] { clashRoyale.GetPlayer(clashRoyale.GetTopPlayers().First().tag, false, true) }, // As first data, use best player (here may be more players, even all players in wr.GetTopPlayers())
                playerTag => clashRoyale.GetPlayer(playerTag, false, true), // How to get object Player from tag
                new Func<Player, IEnumerable<string>>[]{ // Define array of actions to do with each player object (how to get more tags)
                    // Here, do only three actions:
                    new Func<Player, IEnumerable<string>>( // Get other players from clan
                        player => player.clan == null ? new string[0] : // If player doesn't have clan, return nothing
                                    clashRoyale.GetClan(player.clan.tag) // Get player's clan
                                    .members.Select(member => member.tag) // For each clan member, get his tag (this is used to mine additional tags from known players)
                        ),
                    new Func<Player, IEnumerable<string>>( // Get players from player's battles (from opponents)
                        player => player.battles.SelectMany(battle => battle.opponent.Select(plinfo => plinfo.tag))
                        ),
                    new Func<Player, IEnumerable<string>>(
                        player => player.battles.SelectMany(battle => battle.team.Where(plinfo => plinfo.tag != player.tag).Select(plinfo => plinfo.tag))
                        )
                },
                new Func<string, string>( // This functions gets tag (string) and returns required object - in this case, string. If you want to get for example
                                          // full player profile, use something like tag => wr.GetPlayer(tag)
                        resultTag => resultTag
                    )
                );

            // Do not use this in real app, as this code will never end (add some condition)
            // This foreach iterates through all players mined.
            foreach (IEnumerable<string> tags in result)
            {
                Console.WriteLine(String.Join("\n", tags));
            }






            // Example output:

            //Nova l Karnage
            //collectionDay
            //46
            //GAME OVER
            //9CG0QCQ0
            //P988G880
            //228GV8PG
            //PRUUG8P9
            //2GQQ8PJU2
            //L02GQQ8
            //2PJVLL0V
            //UU8CRJJL
            //282V9YQU
            //P92L20Y9
            //9PJL2CYY
            //92CRG92C
            //GJPRJYGP
            //QL2PGVP8
            //2RG8VCRP
            //GQLP9QY
            //CQ28URY0
            //LQ9RYVJC
            //9QCC9JV2
            //VQP2QU2J
            //YJVQP2C9
            //282QQJ2PY
            //8Y0PQRJJQ
            //Q2C0J2R
            //8LCJQLU8
            //2909G2L8C
            //2VLGGPUU
            //892RCQCR
            //8Q98J928
            //PGYL2J0L
            //2RGR0YP20
            //GGYLQRLL
            //2LCCCLRVR
            //9LYGP0YU
            //LJRYGCPJ
            //8RGU928J
            //YCCLQ8PJ
            //2UVVRPV89
            //82UVGPUC
            //P0G9CLQG
            //8908Q0P0
            //Q8288RV
            //PJPPJGR0
            //90RPU8YP
            //GGYG2PC
            //99PC9YCL
            //909C0JU8
            //9YLPQPVJ
            //V8VGJ2PL
            //8Y2UQ2Q
            //2CRULLG0J
            //R0LR9RUQ
            //28CLCP0G
            //28CLCP0G
            //2PLJGLL0
            //R9JJR9U0
            //RU2CC2LG
            //2U0228YP
            //8RJUJGG09
            //2VY8V2VL
            //22UYP9Y0
            // ... (the tags are mined forever)
        }
    }
}
