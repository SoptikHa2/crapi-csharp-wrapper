using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Player season data
    /// </summary>
    public class PlayerSeason
    {
        /// <summary>
        /// Number of current season
        /// </summary>
        public int seasonNumber;
        /// <summary>
        /// Highest player's trophies in this season
        /// </summary>
        public int seasonHighest;
        /// <summary>
        /// Player's total trophies at end of this season
        /// </summary>
        public int seasonEnding;
        /// <summary>
        /// Player's global rank at end of this season (may be null)
        /// </summary>
        public int? seasonEndGlobalRank;
    }
}
