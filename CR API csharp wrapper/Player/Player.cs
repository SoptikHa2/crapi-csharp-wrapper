using System;

namespace CRAPI
{
    /// <summary>
    /// Clash Royale player class
    /// </summary>
    public class Player
    {
        public string tag;
        public string name;
        public int trophies;
        public int legendaryThropies;
        public bool nameChanged;
        public int? globalRank;
        public Card[] currentDeck;
        public PlayerSeason[] previousSeasons;
        public PlayerExperience experience;
        public PlayerStats stats;
        public PlayerGames games;
        public PlayerChestCycle chestCycle;
        public PlayerShopOffers shopOffers;
        // TODO: Clan
        public Arena arena;

        /// <summary>
        /// Get player with TAG (in format like "Y99YRPYG") 
        /// </summary>
        /// <param name="tag">Player's ingame tag (format like "Y99YRPYG")</param>
        /// <returns></returns>
        public static Player GetPlayer(string tag)
        {
            return Inside.APIworker.Parse<Player>(Inside.APIworker.Get(Inside.APIworker.Endpoints.Profile, tag));
        }

        public override string ToString()
        {
            return name;
        }
    }
}
