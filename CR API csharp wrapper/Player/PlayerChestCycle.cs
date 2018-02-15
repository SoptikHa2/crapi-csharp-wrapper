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
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public int superMagical;

        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public int magical;

        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public int legendary;

        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public int epic;

        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public int giant;
    }
}
