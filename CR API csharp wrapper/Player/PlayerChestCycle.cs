using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Chest cycle of player
    /// </summary>
    [Serializable]
    public class PlayerChestCycle
    {
        /// <summary>
        /// Array of upcoming chests. ("silver", "silver", "gold", etc)
        /// </summary>
        public string[] upcoming;

        /// <summary>
        /// Number of chests player have to open to get Super Magical chest
        /// </summary>
        public int superMagical;

        /// <summary>
        /// Number of chests player have to open to get Magical chest
        /// </summary>
        public int magical;

        /// <summary>
        /// Number of chests player have to open to get Legendary chest
        /// </summary>
        public int legendary;

        /// <summary>
        /// Number of chests player have to open to get Epic chest
        /// </summary>
        public int epic;

        /// <summary>
        /// Number of chests player have to open to get Giant chest
        /// </summary>
        public int giant;
    }
}
