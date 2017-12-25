using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Clash Royale Clan class
    /// </summary>
    public class Clan
    {
        /// <summary>
        /// Name of clan
        /// </summary>
        public string name;
        /// <summary>
        /// Clan badge
        /// </summary>
        public ClanBadge badge;
        /// <summary>
        /// TODO
        /// </summary>
        public int type;
        /// <summary>
        /// Number of members
        /// </summary>
        public int memberCount;
        /// <summary>
        /// Clan score
        /// </summary>
        public int score;
        /// <summary>
        /// Required trophies to join
        /// </summary>
        public int requiredScore;
        /// <summary>
        /// Donations in clan
        /// </summary>
        public int donations;
        /// <summary>
        /// TODO
        /// </summary>
        public int currentRank;
        /// <summary>
        /// Clan description
        /// </summary>
        public string description;
        /// <summary>
        /// Clan tag
        /// </summary>
        public string tag;
        /// <summary>
        /// TODO
        /// </summary>
        public string typeName;
        /// <summary>
        /// Clan region (country)
        /// </summary>
        public ClanRegion region;
        /// <summary>
        /// Clan's last clanchest info
        /// </summary>
        public ClanChest clanChest;
        /// <summary>
        /// Clan's members (short summary of each player)
        /// </summary>
        public ClanPlayer[] members;

        /// <summary>
        /// Get string representation of this clan, returns clan name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }
    }
}
