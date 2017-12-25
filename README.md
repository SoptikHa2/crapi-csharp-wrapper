# C# wrapper for CR API
This C# wrapper was made to help people with using [CR API](https://cr-api.com/). 
Wrapper is written in C# and can be used in any C# application.

> You need developer key to use API. You can get the key by following instructions listed on [CR API website](http://docs.cr-api.com/#/authentication)

#### NUGET package

`Stastny.CRAPI`

[nuget.org package](https://www.nuget.org/packages/Stastny.CRAPI/)

```
Install-Package Stastny.CRAPI -Version 0.2.1

dotnet add package Stastny.CRAPI --version 0.2.1
```

## How to use this wrapper

> You have to get your own API key. Get your API KEY [here](http://docs.cr-api.com/#/authentication)

Classes are divided into 3 groups:
- Main classes, like `Player`, `Clan` and `ClashRoyale`. These classes have methods, that can be used to get some output (example: `Player.GetPlayer(string tag)` method returns instance of `Player`). Instances
of these classes have some basic information about object (like `Player.name`)
- Sub classes, like `PlayerClan` or `ClanBadge`. These classes are related to one of main classes - `PlayerClan` is related to class `Player`, or `ClanBadge` is related to class `Clan`.
These classes contains additional information. For example `Player.clan` returns `PlayClan` class, that contains summary of player's clan (information that you can get from ingame profile window, 
detailed information are in `Clan` class)
- Independent classes, like `Card`. These classes are something between Main classes and Sub classes. They are independent, but you cannot get them directly. They are returned alongside with
some Main Class. For example, `Player` class contains `deck` property, that is array of `Card`.


> Note: All classes are in namespace `CRAPI`

To start, initialize `Wrapper` class. `Wrapper wr = new Wrapper(your_dev_key);`. You can get clans and players by calling methods on Wrapper class. See example usage.

### Example usage

> You have to add using: `using CRAPI;`

```csharp
// Initialize wrapper with your dev key
Wrapper wr = new Wrapper("your dev key");

// Get player with ID 80RGVCV9C
Player player = wr.GetPlayer("80RGVCV9C");
// Get clan with ID 22Y802
Clan clan = wr.GetClan("22Y802");

Console.WriteLine(player.name);
Console.WriteLine(clan.name);

// Write cards in player's deck
foreach (Card card in player.currentDeck)
    Console.WriteLine(card);

Console.WriteLine("\n");

// Write best player's name and best clan's name
Console.WriteLine(wr.GetTopPlayers()[0]);
Console.WriteLine(wr.GetTopClans()[0]);
```

## Documentation

Documentation is still in TODO phase. Everything you need is commented inside classes. Open `.cs` files to see code.

### Exceptions

When something goes wrong, this wrapper throws an exception. This exception is thrown ONLY in `.Get` methods, that access API. The exception thrown is `WebException`. This exception means,
that either user don't have connection to the Internet, or something inside API gone wrong (their servers may be down).

## How to add this to my project

You can use nuget :) It's `Stastny.CRAPI`

[nuget.org package](https://www.nuget.org/packages/Stastny.CRAPI/)

```
Install-Package Stastny.CRAPI -Version 0.2.1

dotnet add package Stastny.CRAPI --version 0.2.1
```