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
        /// Player's wins percentage
        /// </summary>
        public float winsPercent;
        /// <summary>
        /// Player's losses
        /// </summary>
        public int losses;
        /// <summary>
        /// Player's losses percentage
        /// </summary>
        public float lossesPercent;
        /// <summary>
        /// Player's draws
        /// </summary>
        public int draws;
        /// <summary>
        /// Player's draws percentage
        /// </summary>
        public float drawsPercent;
        /// <summary>
        /// Player's current win streak
        /// </summary>
        public int currentWinStreak;
    }
}
