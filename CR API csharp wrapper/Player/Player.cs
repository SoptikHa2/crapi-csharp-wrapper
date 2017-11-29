using System;

namespace CRAPI
{
    /// <summary>
    /// Clash Royale player class
    /// </summary>
    public class Player
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
        /// Player's current trophies
        /// </summary>
        public int trophies;
        /// <summary>
        /// Player's legendary trophies
        /// </summary>
        public int legendaryTrophies;
        /// <summary>
        /// Changed player his name already?
        /// </summary>
        public bool nameChanged;
        /// <summary>
        /// Global rank of player (may be null)
        /// </summary>
        public int? globalRank;
        /// <summary>
        /// Current deck of player
        /// </summary>
        public Card[] currentDeck;
        /// <summary>
        /// Previous seasons data of player
        /// </summary>
        public PlayerSeason[] previousSeasons;
        /// <summary>
        /// Player's experience
        /// </summary>
        public PlayerExperience experience;
        /// <summary>
        /// Player's stats
        /// </summary>
        public PlayerStats stats;
        /// <summary>
        /// Player's game stats
        /// </summary>
        public PlayerGames games;
        /// <summary>
        /// Player's chest cycle data
        /// </summary>
        public PlayerChestCycle chestCycle;
        /// <summary>
        /// Player's shop offers
        /// </summary>
        public PlayerShopOffers shopOffers;
        /// <summary>
        /// Player's clan summary
        /// </summary>
        public PlayerClan clan;
        /// <summary>
        /// Player's arena
        /// </summary>
        public Arena arena;

        /// <summary>
        /// Get player with TAG (something like "Y99YRPYG") 
        /// </summary>
        /// <param name="tag">Player's ingame tag (something like "Y99YRPYG")</param>
        /// <returns></returns>
        public static Player GetPlayer(string tag)
        {
            return Inside.APIworker.Parse<Player>(Inside.APIworker.Get(Inside.APIworker.Endpoints.Profile, tag));
        }

        /// <summary>
        /// Return string representation of player, returns player's name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }
    }
}
