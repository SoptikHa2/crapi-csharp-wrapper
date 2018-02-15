using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Badge of clan
    /// </summary>
    [Serializable]
    public class ClanBadge
    {
        /// <summary>
        /// URL to clan badge
        /// </summary>
        public string image;
        /// <summary>
        /// Name of the badge
        /// </summary>
        public string name;
        /// <summary>
        /// I don't know what this means. If you do, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public string category;
        /// <summary>
        /// Badge unique ID
        /// </summary>
        public int id;
    }
}
