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
    [Serializable]
    public class SimplifiedPlayer
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
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public int rank;
        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
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
        /// Number of crowns player scored last clan chest [May be null]
        /// </summary>
        public int? clanChestCrowns;
        /// <summary>
        /// Player donations [May be null]
        /// </summary>
        public int? donations;
        /// <summary>
        /// Donations that player received [May be null]
        /// </summary>
        public int? donationsReceived;
        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks! [May be null]
        /// </summary>
        public int? donationsDelta;
        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks! [May be null]
        /// </summary>
        public float? donationsPercent;
        /// <summary>
        /// Player's current arena
        /// </summary>
        public Arena arena;
        /// <summary>
        /// Basic info about player's clan. Is null if this instance comes from clan or player doesn't have clan.
        /// </summary>
        public ClanInfo clan;

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
