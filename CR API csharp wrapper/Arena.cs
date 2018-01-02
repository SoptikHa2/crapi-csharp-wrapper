using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Clash Royale Arena class
    /// </summary>
    [Serializable]
    public class Arena
    {
        /// <summary>
        /// Arena number (like "Arena 10")
        /// </summary>
        public string arena;
        /// <summary>
        /// Arena ID
        /// </summary>
        public int arenaID;
        /// <summary>
        /// Arena name (like "Hog Mountain")
        /// </summary>
        public string name;
        /// <summary>
        /// Arena minimum trophy limit
        /// </summary>
        public int trophyLimit;

        /// <summary>
        /// Returns string reprezentation of arena, return arena's name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }
    }
}
