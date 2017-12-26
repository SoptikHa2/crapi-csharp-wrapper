# C# wrapper for CR API
This C# wrapper was made to help people with using [CR API](https://cr-api.com/). 
Wrapper is written in C# and can be used in any C# application.

> You need developer key to use API. You can get the key by following instructions listed on [CR API website](http://docs.cr-api.com/#/authentication)

## How to use this wrapper

First of all, you should obtain your own developer key. Please see [CR API website](http://docs.cr-api.com/#/authentication) and get your developer key.
You should receive something like this: `3012e5ab523243q2a86w2bqa58bdf9bce96071843029447631924cf99w5a9kfc` (example key, this one is not valid).


When you get your own key, you can include this wrapper in your project. The easiest way is to use my [NUGET package](https://www.nuget.org/packages/Stastny.CRAPI/).
You can either import the package via visual studio or use one of these commands:
```
Install-Package Stastny.CRAPI -Version 0.2.1

dotnet add package Stastny.CRAPI --version 0.2.1
```


After you get your developer key and include the wrapper, you can start using it. Start by adding new using.
```csharp
// Start of file
using System;
using CRAPI;
```


Then you need to create new Wrapper object.

```csharp
Wrapper wr = new Wrapper("your dev key");
```


Now, you can get Player or Clan objects.

```csharp
Player somerandomplayer = wr.GetPlayer("80RGVCV9C"); // Get player with tag 80RGVCV9C

Clan somerandomclan = wr.GetClan("22Y802"); // Get clan with tag 22Y802

Player[] bestPlayersInClashRoyale = wr.GetTopPlayers(); // Returns array which contains the best players in CR
Clan[] bestClansInClashRoyale = wr.GetTopClans(); // Returns array which contains the best clans in CR
```



You can get information about player/clan directly from these objects.

```csharp
somerandomplayer.name; // Get player's name
Card[] deck = somerandomplayer.currentDeck; // Get array of card, this array represents player's deck

// Write each card's name into console
foreach(Card card in deck){
	Console.WriteLine(card.name);
}

// ...
```


You can get much more info from player or clan objects - for example player's recent battles or clan badge. Use IntelliSense or browse my code to see what you can achieve with
my wrapper!

## Example usage

> You have to add using: `using CRAPI;`

```csharp
 // Initialize wrapper with your dev key
Wrapper wr = new Wrapper("3012e5ab523243q2a86w2bqa58bdf9bce96071843029447631924cf99w5a9kfc");

// Read player TAG from default input
string tag = Console.ReadLine();

// Get player from API
Player player = wr.GetPlayer(tag);

// Write player's name, level and arena'
Console.WriteLine($"{player.name} (XP level {player.stats.level}) ({player.arena.name})");
// Write player's clan name and player's role in clan
Console.WriteLine($"{player.clan.name} ({player.clan.role})");

Card[] cards = player.currentDeck;

// For each card in player's deck
foreach(Card c in cards)
{
	// Write card's name, elixir cost, rarity and level
    Console.WriteLine($"{c.name} {c.elixir} ({c.rarity}) --- level: {c.level}");
}
```

Output:

```
Soptik (XP level 10) (Legendary Arena)
CZ exKnights 2 (elder)
Mega Knight 7 (Legendary) --- level: 1
Skeleton Army 3 (Epic) --- level: 5
Inferno Tower 5 (Rare) --- level: 8
The Log 2 (Legendary) --- level: 2
Minions 3 (Common) --- level: 12
Miner 3 (Legendary) --- level: 2
Executioner 5 (Epic) --- level: 5
Archers 3 (Common) --- level: 11
```

## NUGET package

`Stastny.CRAPI`

[nuget.org package](https://www.nuget.org/packages/Stastny.CRAPI/)

```
Install-Package Stastny.CRAPI -Version 0.2.1

dotnet add package Stastny.CRAPI --version 0.2.1
```