using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Short clan description (clan tag, name, player's role and badge) from player's profile view
    /// </summary>
    [Serializable]
    public class SimplifiedClan
    {
        /// <summary>
        /// Clan TAG
        /// </summary>
        public string tag;
        /// <summary>
        /// Clan name
        /// </summary>
        public string name;
        /// <summary>
        /// If instance of this class comes from player's profile: Player's role (elder, member, etc). If this instance is from clan search, this should be NULL.
        /// </summary>
        public string role;
        /// <summary>
        /// Overall donations in clan
        /// </summary>
        public int? donations;
        /// <summary>
        /// If instance of this class comes from player's profile: Player's received donations. If this instance is from clan search, this should be NULL. May be null if player is not in clan.
        /// </summary>
        public int? donationsReceived;
        /// <summary>
        /// If instance of this class comes from player's profile: Player's delta donations. If this instance is from clan search, this should be NULL. May be null if player is not in clan.
        /// </summary>
        public int? donationsDelta;
        /// <summary>
        /// Clan badge
        /// </summary>
        public ClanBadge badge;

        /// <summary>
        /// Returns string representation of clan summary, returns clan name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }
    }
}
