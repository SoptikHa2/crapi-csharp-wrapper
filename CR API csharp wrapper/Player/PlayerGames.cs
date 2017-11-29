using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Stats of player's games
    /// </summary>
    public class PlayerGames
    {
        /// <summary>
        /// Total number of all player's games
        /// </summary>
        public int total;
        /// <summary>
        /// Total number of all player's tournament games
        /// </summary>
        public int tournamentGames;
        /// <summary>
        /// Player's wins
        /// </summary>
        public int wins;
        /// <summary>
        /// Player's losses
        /// </summary>
        public int losses;
        /// <summary>
        /// Player's draws
        /// </summary>
        public int draws;
        /// <summary>
        /// Player's current win streak
        /// </summary>
        public int currentWinStreak;
    }
}
