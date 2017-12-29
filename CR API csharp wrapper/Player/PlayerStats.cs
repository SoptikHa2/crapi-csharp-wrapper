using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Player overall stats
    /// </summary>
    public class PlayerStats
    {
        /// <summary>
        /// Cards won in tournaments
        /// </summary>
        public int tournamentCardsWon;
        /// <summary>
        /// Best count of player's tropheis
        /// </summary>
        public int maxTrophies;
        /// <summary>
        /// Count of player's 3 crown wins
        /// </summary>
        public int threeCrownWins;
        /// <summary>
        /// Count of cards in player's inventory
        /// </summary>
        public int cardsFound;
        /// <summary>
        /// Player's favourite card
        /// </summary>
        public Card favoriteCard;
        /// <summary>
        /// Total count of donations to player's clanmates
        /// </summary>
        public int totalDonations;
        /// <summary>
        /// Max wins in challenge
        /// </summary>
        public int challengeMaxWins;
        /// <summary>
        /// Cards won in challenge
        /// </summary>
        public int challengeCardsWon;
        /// <summary>
        /// Player's XP level
        /// </summary>
        public int level;
    }
}
