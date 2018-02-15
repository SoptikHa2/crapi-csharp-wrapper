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
    [Serializable]
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
        /// <summary>
        /// Player's last played battle
        /// </summary>
        public BattleInfo lastBattle;
    }

    [Serializable]
    public struct BattleInfo
    {
        /// <summary>
        /// Type of game, "2v2"
        /// </summary>
        public string type;
        /// <summary>
        /// Time (linux time, example: 1518707789)
        /// </summary>
        public int time;
    }
}
