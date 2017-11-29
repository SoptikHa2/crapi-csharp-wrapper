using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Short summary of player (from clan view)
    /// </summary>
    public class ClanPlayer
    {
        /// <summary>
        /// Name of player
        /// </summary>
        public string name;
        /// <summary>
        /// Arena of player
        /// </summary>
        public Arena arena;
        /// <summary>
        /// Role of player
        /// </summary>
        public int role;
        /// <summary>
        /// Level of player
        /// </summary>
        public int expLevel;
        /// <summary>
        /// Trophies of player
        /// </summary>
        public int trophies;
        /// <summary>
        /// Donations from player
        /// </summary>
        public int donations;
        /// <summary>
        /// TODO
        /// </summary>
        public int currentRank;
        /// <summary>
        /// TODO
        /// </summary>
        public int previousRank;
        /// <summary>
        /// Number of crowns, player collected last clan chest
        /// </summary>
        public int clanChestCrowns;
        /// <summary>
        /// TAG of player
        /// </summary>
        public string tag;
        /// <summary>
        /// Name of role of player (Elder, Leader, ...)
        /// </summary>
        public string roleName;
        /// <summary>
        /// Score of player
        /// </summary>
        public int score;

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
