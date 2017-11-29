using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Clan chest information
    /// </summary>
    public class ClanChest
    {
        /// <summary>
        /// Number of collected crowns
        /// </summary>
        public int clanChestCrowns;
        /// <summary>
        /// Percentage, showing how much % crowns of maximum number of crowns clan collected
        /// </summary>
        public int clanChestCrownsPercent;
        /// <summary>
        /// Required number of crowns to 10/10 chest
        /// </summary>
        public int clanChestCrownsRequired;
    }
}
