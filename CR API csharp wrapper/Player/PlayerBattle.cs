using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    [Serializable]
    public class PlayerBattle
    {
        /// <summary>
        /// Type of battle ("2v2")
        /// </summary>
        public string type;
        /// <summary>
        /// Additional info about battle
        /// </summary>
        public Mode mode;
        /// <summary>
        /// Battle timestamp
        /// </summary>
        public int utcTime;
        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public string deckType;
        /// <summary>
        /// Size of team (1, 2)
        /// </summary>
        public int teamSize;
        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public int winner;
        /// <summary>
        /// Player's crowns
        /// </summary>
        public int teamCrowns;
        /// <summary>
        /// Opponent's crowns
        /// </summary>
        public int opponentCrowns;
        /// <summary>
        /// Array of players of one team
        /// </summary>
        public PlayerInfo[] team;
        /// <summary>
        /// Array of players of second team
        /// </summary>
        public PlayerInfo[] opponent;
        /// <summary>
        /// In which arena was the fight
        /// </summary>
        public Arena arena;
        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public int? winCountBefore;
    }

    [Serializable]
    public struct Mode
    {
        /// <summary>
        /// Name of battle mode ("TeamVsTeamLadder")
        /// </summary>
        public string name;
        /// <summary>
        /// Type of deck (Collection, Draft, ...)
        /// </summary>
        public string deck;
        /// <summary>
        /// Tournament or normal card levels? ("Ladder")
        /// </summary>
        public string cardLevels;
        /// <summary>
        /// Bonus seconds in overtime
        /// </summary>
        public int overtimeSeconds;
        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public string players;
        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public bool sameDeck;
    }

    [Serializable]
    public struct PlayerInfo
    {
        /// <summary>
        /// Player's tag
        /// </summary>
        public string tag;
        /// <summary>
        /// Player's name
        /// </summary>
        public string name;
        /// <summary>
        /// Crowns earned by this player in the battle
        /// </summary>
        public int crownsEarned;
        /// <summary>
        /// Player's clan
        /// </summary>
        public SimplifiedClan clan;
        /// <summary>
        /// Player's deck in this battle
        /// </summary>
        public Card[] deck;
        /// <summary>
        /// Link to user deck. Follow this link on device with ClashRoyale installed to copy deck
        /// </summary>
        public string deckLink;
        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public int startTrophies;
    }
}
