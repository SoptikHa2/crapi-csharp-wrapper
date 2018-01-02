using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    [Serializable]
    public class PlayerAchievement
    {
        /// <summary>
        /// Name of achievement
        /// </summary>
        public string name;
        /// <summary>
        /// Progress of achievement. There are achievements,
        /// you can achieve multiple times. (Donate 100, 500, ... cards, for example).
        /// The more times you complete the achievement, the more stars you get.
        /// </summary>
        public int stars;
        /// <summary>
        /// TODO
        /// </summary>
        public int value;
        /// <summary>
        /// TODO
        /// </summary>
        public int target;
        /// <summary>
        /// Achievement description
        /// </summary>
        public string info;

        public override string ToString()
        {
            return name;
        }
    }
}
