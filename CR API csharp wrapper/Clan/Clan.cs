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
        /// Clan tag
        /// </summary>
        public string tag;
        /// <summary>
        /// Clan name
        /// </summary>
        public string name;
        /// <summary>
        /// Clan description
        /// </summary>
        public string description;
        /// <summary>
        /// TODO
        /// </summary>
        public string type;
        /// <summary>
        /// Clan score (trophies)
        /// </summary>
        public int score;
        /// <summary>
        /// Number of members
        /// </summary>
        public int memberCount;
        /// <summary>
        /// Required trophies to join
        /// </summary>
        public int requiredScore;
        /// <summary>
        /// TODO
        /// </summary>
        public int donations;
        /// <summary>
        /// Clan chest status
        /// </summary>
        public ClanChest clanChest;
        /// <summary>
        /// Clan badge (image)
        /// </summary>
        public ClanBadge badge;
        /// <summary>
        /// Clan location
        /// </summary>
        public ClanRegion location;
        /// <summary>
        /// Array of players in clan
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

    public struct ClanChest
    {
        public string status;
    }
}
