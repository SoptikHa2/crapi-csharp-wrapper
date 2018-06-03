using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    [Serializable]
    public class ClanInfo
    {
        /// <summary>
        /// Clan tag
        /// </summary>
        public string tag;
        /// <summary>
        /// Clan name
        /// </summary>
        public string name;
        /// <summary>
        /// Clan badge
        /// </summary>
        public ClanBadge badge;
    }
}
