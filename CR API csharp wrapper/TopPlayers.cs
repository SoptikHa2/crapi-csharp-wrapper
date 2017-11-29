using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Class that contains array of the best player in Clash Royale
    /// </summary>
    public class TopPlayers
    {
        /// <summary>
        /// DateTime when was this list last updated (in MS format)
        /// </summary>
        public ulong lastUpdated;
        /// <summary>
        /// Top players
        /// </summary>
        public Player[] players;
    }
}
