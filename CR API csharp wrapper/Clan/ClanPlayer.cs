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
        /// Player's tag
        /// </summary>
        public string tag;
        /// <summary>
        /// TODO
        /// </summary>
        public int rank;
        /// <summary>
        /// TODO
        /// </summary>
        public int previousRank;
        /// <summary>
        /// Player's role in clan
        /// </summary>
        public string role;
        /// <summary>
        /// Player's level
        /// </summary>
        public int expLevel;
        /// <summary>
        /// Player's trophies
        /// </summary>
        public int trophies;
        /// <summary>
        /// Number of crowns player scored last clan chest
        /// </summary>
        public int clanChestCrowns;
        /// <summary>
        /// Player donations
        /// </summary>
        public int donations;
        /// <summary>
        /// Donations that player received
        /// </summary>
        public int donationsReceived;
        /// <summary>
        /// TODO [May be null!]
        /// </summary>
        public int? donationsDelta;
        /// <summary>
        /// TODO
        /// </summary>
        public float donationsPercent;
        /// <summary>
        /// Player's current arena
        /// </summary>
        public Arena arena;

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
