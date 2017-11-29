using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Player experience data
    /// </summary>
    public class PlayerExperience
    {
        /// <summary>
        /// Player's level
        /// </summary>
        public int level;
        /// <summary>
        /// Player's XP
        /// </summary>
        public int xp;
        /// <summary>
        /// Player's XP that he needs to level up (may be number or string "Max", if player can no longer level up)
        /// </summary>
        public string xpRequiredForLevelUp;
        /// <summary>
        /// How many additional XP player needs to level up
        /// </summary>
        public int xpToLevelUp;
    }
}
