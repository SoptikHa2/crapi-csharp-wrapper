using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Class that contains the best clans in Clash Royale
    /// </summary>
    public class TopClans
    {
        /// <summary>
        /// DateTime when was this list last updated (in MS format)
        /// </summary>
        public ulong lastUpdated;
        /// <summary>
        /// Top clans
        /// </summary>
        public Clan[] clans;
    }
}
