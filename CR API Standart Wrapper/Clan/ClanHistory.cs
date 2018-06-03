using System;
using System.Collections.Generic;
using System.Text;

namespace CRAPI
{
    /// <summary>
    /// Clan history record
    /// </summary>
    [Serializable]
    public class ClanHistoryRecord
    {
        /// <summary>
        /// Total donations
        /// </summary>
        public int donations;
        /// <summary>
        /// Member count
        /// </summary>
        public int memberCount;
        /// <summary>
        /// Array of all clan members
        /// </summary>
        public ClanHistoryPlayer[] members;
    }

    [Serializable]
    public struct ClanHistoryPlayer
    {
        /// <summary>
        /// Position within rank
        /// </summary>
        public int clanRank;
        /// <summary>
        /// Total player donations
        /// </summary>
        public int donations;
        /// <summary>
        /// Player name
        /// </summary>
        public string name;
        /// <summary>
        /// Player tag
        /// </summary>
        public string tag;
        /// <summary>
        /// Player trophies
        /// </summary>
        public int trophies;
    }
}
