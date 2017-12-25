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

        public string tag;

        public int rank;

        public int previousRank;

        public string role;

        public int expLevel;

        public int trophies;

        public int clanChestCrowns;

        public int donations;

        public int donationsReceived;

        public int donationsDelta;

        public float donationsPercent;

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
