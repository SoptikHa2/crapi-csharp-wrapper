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
    [Serializable]
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
        /// Invite policy of clan (example: "invite only")
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
        /// Total donations in clan
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
        public SimplifiedPlayer[] members;
        /// <summary>
        /// Keeps basic information about clan tracking by API
        /// </summary>
        public TrackingStatus tracking;

        /// <summary>
        /// Get string representation of this clan, returns clan name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }
    }

    [Serializable]
    public struct ClanChest
    {
        /// <summary>
        /// Current clan chest status (example: "inactive")
        /// </summary>
        public string status;
        /// <summary>
        /// Current clan chest level
        /// </summary>
        public int level;
        /// <summary>
        /// Maximum clan chest level (10)
        /// </summary>
        public int maxLevel;
        /// <summary>
        /// Current number of crowns commited to clan chest
        /// </summary>
        public int crowns;
    }

    [Serializable]
    public struct TrackingStatus
    {
        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public bool active;
        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public bool available;
        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public int snapshotCount;
    }
}
