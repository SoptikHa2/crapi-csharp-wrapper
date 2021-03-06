**Warning**

Unofficial Clash Royale API will change it’s version to v3 in few days, which means complete API overhaul. This will almost certainly break this wrapper. If your application ceases to work, switch to [P.E.K.K.A .Net wrapper](https://github.com/Blind-Striker/clash-royale-client-dotnet) which should work flawlessly.

- Soptik, 2019-05-06

---

**Warning**

I no longer use this wrapper myself and development stalled for several months. This wrapper still works and you can use it, and it'll work in the future - but no new API features will be added and bugs won't be fixed. I suggest you to use [P.E.K.K.A .Net wrapper](https://github.com/Blind-Striker/clash-royale-client-dotnet) which should support both official and unofficial Clash Royale API.

- Soptik, 2019-01-23

---

# C# Wrapper for CR API
This C# Wrapper was created to help developers with accessing unofficial [Clash Royale API](https://royaleapi.com/). You can search for clans, tournaments, see player's deck, card collection, chest cycle, past battles, view clan information, search for open tournaments, gather Clan Wars data and much more. You can even use this wrapper as data miner. Feel free to explore this wrapper.

This wrapper is written using .Net Standart. This means, you can use it with ASP, Xamarin, .Net, .Net Core - anything. See [this table](http://immo.landwerth.net/netstandard-versions/#) and select version `2.0+` to see all avaiable APIs.

> If you find something that isn't in wrapper, visit [API website](https://docs.royaleapi.com/) and see, if the feature you want is there. If not, [create API issue](https://github.com/royaleAPI/cr-api/issues).
If it is there, but it isn't in wrapper, [create wrapper issue](https://github.com/SoptikHa2/crapi-csharp-wrapper/issues) and I'll implement it as soon as possible.

To start, you'll need AUTH key to access API. Go to [ROYALE API website](https://docs.royaleapi.com/#/authentication?id=key-management) and get your own key.

Now just download nuget package [Stastny.CRAPI](https://www.nuget.org/packages/Stastny.CRAPI/) and everything will be ready :)
```
Install-Package Stastny.CRAPI -Version 0.7.3

dotnet add package Stastny.CRAPI --version 0.7.3
```

## How to start
First of all, get API auth key and download nuget package (see above). Now, you have to instantiate wrapper class.
```csharp
Wrapper wr = new Wrapper("YourAPIKey");
```
You can now use `wr` to get everything from API. Let's get player:
```csharp
Player SomeRandomPlayer = wr.GetPlayer("80RGVCV9C"); // 80RGVCV9C is player TAG
```
You can access player's chest cycle, deck, card collection, clan, etc. Just type `SomeRandomPlayer.` and let Visual Studio show you everything you can get!

> Clan received from Player doesn't contain all possible informations. Use `wr.GetClan(SomeRandomPlayer.clan.tag);` to get everything else.

You can use simmilar process to get clan `wr.GetClan("9Y8888RC");`, or tournament.

You can search for clans using `wr.SearchForClans("Clash", 10000, 10, 40);`. This will search for clans with name "Clash", score at least 10000 and member count between 10 and 40. Some of these values may be omitted (by passing `null`), but at least one have to be there. Name must be at least 3 characters long. For example `wr.SearchForClans(null, null, null, 30);` will search for all clans with at most 30 members.

## Use wrapper Async

You can even use this wrapper async - that means, you can order 20 player requests and do something while waiting for wrapper to finish.


> You have to add new using: `using System.Threading.Tasks;`

```csharp
// As in sync version, initialize wrapper with your API key
Wrapper wr = new Wrapper("MyAPIKey");

// Store player tag and clan tag
string tag = "80RGVCV9C";
string ctag = "22Y802";

Console.WriteLine("I'll get one player profile, one clan profile, best players and best clans async!");

// Get one player, one clan, best players and best clans async
Task<Player> a_player = wr.GetPlayerAsync(tag);
Task<Clan> a_clan = wr.GetClanAsync(ctag);
Task<SimplifiedPlayer[]> a_topPlayers = wr.GetTopPlayersAsync(); // wr.GetTopPlayers() and its async version return SimplifiedPlayer -> this is just like Player,
                                                                    // but simplified with less properties. If you want to get complete overview, get the top player:
                                                                    // Player topPlayer = wr.GetPlayer(wr.GetTopPlayers()[0].tag)
Task<SimplifiedClan[]> a_topClans = wr.GetTopClansAsync(); // Here is the same thing as with GetTopPlayers()


// Wait untill everything is prepared. You can for example write dots into console /* THIS IS OPTIONAL */
while (!a_player.IsCompleted || !a_clan.IsCompleted || !a_topPlayers.IsCompleted || !a_topClans.IsCompleted)
{
    Console.Write(".");
    System.Threading.Thread.Sleep(50);
}


// The while cycle above ends when everything loaded
Console.WriteLine("Done!");


// Get variables
// NOTE: If the while loop above wouldn't be here, the application would wait untill everything is prepared here
Player player = await a_player;
Clan clan = await a_clan;
SimplifiedPlayer[] topPlayers = await a_topPlayers;
SimplifiedClan[] topCLans = await a_topClans;

// Write few names
Console.WriteLine(player.name);
Console.WriteLine(clan.name);
Console.WriteLine(topPlayers[0].name);
Console.WriteLine(topCLans[0].name);
```


Output:
```
I'll get one player profile, one clan profile, best players and best clans async!
..........................................................Done!
Soptik
CZ exKnights 2
Nova l Pompeyo
Nova eSports
```



## Using Include and Exclude

All .Get methods have two optional parameters - `string[] include` and `string[] exclude`. At least one of these must be `null`.

If you for example only want to know player's clan, there's no reason to request and download whole player object. Just add `clan` (name of field you want there) into
string array `include` and API will return only `clan` field - everything else will be either `null` or it will have its default value. You can add multiple fields into the array. 
If this parameter is `null`, full response is returned.
```csharp
wr.GetPlayer("80RGVCV9C", new string[] { "clan" }); // Returns only "clan" field, everything else is null or has default value
```

If you want, you can also exclude some fields. Just pass one or more field names in `string[] exclude`. You MUST set `include` as `null`, you cannot specify both
parameters at one time.
```csharp
wr.GetPlayer("80RGVCV9C", null, new string[] { "clan" }); // Returns everything except "clan" field, this will be `null`
```

## Mining data

This wrapper offers unique feature - you can use it to simply mine data from Clash Royale. Simply call `wr.Mine` and wrapper will take care of everything.
The mine method looks like this:
```csharp
public IEnumerable<IEnumerable<O>> Mine<T, O>(IEnumerable<T> input, Func<string, T> getObjectFromTag, IEnumerable<Func<T, IEnumerable<string>>> selectTagsFunction, Func<string, O> resultFunction)
```
`T` is thing, that you work with (`Player`, `Clan`, ...). `O` is output type - if you want to get player tags, this will be `string`. `input` is collection of source objects - 
for example players. You need at least one. The more objects you put here, the more will you wait before mining starts.

Function `getObjectFromTag` defines, what you want - if you want to use players, simply use `tag => wr.GetPlayer(tag)`. `selectTagsFunction` is collection of functions,
where each gets `Player` (or clan, ...) and returns collection of `string` - tags gathered from object (from player's clan, from his recent battles, ...).

`resultFunction` gets string (tag) and returns `O` - thing that you want to get from mining. This may be tag (`tag => tag`) or full player profile (`tag => wr.GetPlayer(tag)`).

Here's example that starts mining tags of players, where first player is the best player in Clash Royale.

```csharp
// Player is the thing I work with (I mine players), string is the thing mining returns (player tags)
var result = wr.Mine<Player, string>(
    new Player[] { wr.GetPlayer(wr.GetTopPlayers().First().tag, false, true) }, // As first data, use best player (here may be more players, even all players in wr.GetTopPlayers())
    playerTag => wr.GetPlayer(playerTag, false, true), // How to get object Player from tag
    new Func<Player, IEnumerable<string>>[]{ // Define array of actions to do with each player object (how to get more tags)
        // Here, do only three actions:
        new Func<Player, IEnumerable<string>>( // Get other players from clan
            player => player.clan == null ? new string[0] : // If player doesn't have clan, return nothing
                        wr.GetClan(player.clan.tag) // Get player's clan
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
```

Mining may look really complex, but if you spend few minutes and learn which parameter does what (I suggest you to take look at [Mine method source code](https://github.com/SoptikHa2/crapi-csharp-wrapper/blob/master/CR%20API%20csharp%20wrapper/Wrapper.cs#L831)
so you'll know how to use it) you'll be able to build data miners in few minutes.

Please note that you should access result by using foreach, because mining uses `yield return` functionality.

## Exceptions

When something goes wrong, an exception is thrown. This can be either `APIException` if something goes wrong at sever side (bad player tag, bad auth key, etc).
This is class inherited from `Exception` and contains message from server what gone wrong.

If something goes wrong at client side (timed out, no internet connection), an `WebException` is thrown.


# Contact Me

If you have any feedback, questions, or suggestions, feel free to contact me! You can add [GitHub issue](https://github.com/SoptikHa2/crapi-csharp-wrapper/issues) or send me a message
via [Discord](https://discordapp.com/) (`SoptikHa2#0976`).
