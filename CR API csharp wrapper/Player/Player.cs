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
        /// Player's position in global leaderboard. May be NULL.
        /// </summary>
        public int? rank;
        /// <summary>
        /// Player's current arena
        /// </summary>
        public Arena arena;

        /// <summary>
        /// Player's current clan
        /// </summary>
        public SimplifiedClan clan;

        /// <summary>
        /// Additional player's stats
        /// </summary>
        public PlayerStats stats;

        /// <summary>
        /// Stats about player's games
        /// </summary>
        public PlayerGames games;

        /// <summary>
        /// Player's chest cycle
        /// </summary>
        public PlayerChestCycle chestCycle;

        /// <summary>
        /// Player's current deck
        /// </summary>
        public Card[] currentDeck;

        /// <summary>
        /// Cards in player's inventory
        /// </summary>
        public Card[] cards;

        /// <summary>
        /// Player's achievements
        /// </summary>
        public PlayerAchievement[] achievements;

        /// <summary>
        /// Player's recent battles
        /// </summary>
        public PlayerBattle[] battles;

        /// <summary>
        /// Player's league statistics. This may be NULL.
        /// </summary>
        public LeagueStatistics leagueStatistics;

        /// <summary>
        /// URL of player's deck. Open in web browser on device that has CR installed to copy player's deck.
        /// </summary>
        public string deckLink;


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
