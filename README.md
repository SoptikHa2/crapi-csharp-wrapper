# C# wrapper for CR API
This C# wrapper was made to help people with using [CR API](https://cr-api.com/). 
Wrapper is written in C# and can be used in any C# application.

#### NUGET package

`Stastny.CRAPI`

[nuget.org](https://www.nuget.org/packages/Stastny.CRAPI/)

```
Install-Package Stastny.CRAPI -Version 0.1.0

dotnet add package Stastny.CRAPI --version 0.1.0
```

## How to use this wrapper

You can start using wrapper right now, there's no API usage limit nor need to auth to use API.

Classes are divided into 3 groups:
- Main classes, like `Player`, `Clan` and `ClashRoyale`. These classes have methods, that can be used to get some output (example: `Player.GetPlayer(string tag)` method returns instance of `Player`). Instances
of these classes have some basic information about object (like `Player.name`)
- Sub classes, like `PlayerClan` or `ClanBadge`. These classes are related to one of main classes - `PlayerClan` is related to class `Player`, or `ClanBadge` is related to class `Clan`.
These classes contains additional information. For example `Player.clan` returns `PlayClan` class, that contains summary of player's clan (information that you can get from ingame profile window, 
detailed information are in `Clan` class)
- Independent classes, like `Card`. These classes are something between Main classes and Sub classes. They are independent, but you cannot get them directly. They are returned alongside with
some Main Class. For example, `Player` class contains `deck` property, that is array of `Card`.


> Note: All classes are in namespace `CRAPI`

### Example usage

```csharp
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
```

## Documentation

Documentation is still in TODO phase. Everything you need is commented inside classes. Open `.cs` files to see code.

## How to add this to my project

You can use nuget :) It's `Stastny.CRAPI`

[nuget.org](https://www.nuget.org/packages/Stastny.CRAPI/)

```
Install-Package Stastny.CRAPI -Version 0.1.0

dotnet add package Stastny.CRAPI --version 0.1.0
```