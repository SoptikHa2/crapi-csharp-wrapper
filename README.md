# C# Wrapper for CR API
This C# Wrapper was created to help developers with accessing unofficial [Clash Royale API](https://cr-api.com/). You can search for clans, tournaments, see player's deck, card collection, chest cycle, past battles, view clan information, search for open tournaments and much more. Feel free to explore this wrapper.

> Because the API changes very often, there may be few things that are in API but are not yet in wrapper. If you find them, please report it via github issues.

For first, you'll need AUTH key to access API. Go to [CR API website](http://docs.cr-api.com/#/authentication?id=key-management) and get your own key.

Now just download nuget package [Stastny.CRAPI](https://www.nuget.org/packages/Stastny.CRAPI/) and you start :)
```
Install-Package Stastny.CRAPI -Version 0.5.5

dotnet add package Stastny.CRAPI --version 0.5.5
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

> After every request, API response in JSON format is stored in `wr.ServerResponse`. If you miss some feature, take a look if it isn't in server response. If it is, create github issue and let me know. If it isn't, create [API issue](https://github.com/cr-api/cr-api/issues) so API developers can add this feature.

## Use wrapper Async

You can even use this wrapper async - that means, you can order 20 player requests and do something while waiting for wrapper to finish.

> You have to write this code to special method -> do not throw this into Main! Make method like `async void DoSomething()` (async keyword!)


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
// NOTE: If the while cycle above wouldn't be here, the application would wait untill everything is prepared here
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

## Exceptions

When something goes wrong, an exception is thrown. This can be either `APIException` if something goes wrong at sever side (bad player tag, bad auth key, etc).
This is class inherited from `Exception` and contains message from server what gone wrong.

If something goes wrong at client side (timed out, no internet connection), an `WebException` is thrown.


# Contact Me

If you have any feedback, questions, or suggestions, feel free to contact me! You can add [GitHub issue](https://github.com/SoptikHa2/crapi-csharp-wrapper/issues) or send me a message
via [Discord](https://discordapp.com/) (SoptikHa2#0976).