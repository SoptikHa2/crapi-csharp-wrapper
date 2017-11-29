using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Clan region
    /// </summary>
    public class ClanRegion
    {
        /// <summary>
        /// ID of region
        /// </summary>
        public int id;
        /// <summary>
        /// Is this region country? (Europe X Germany)
        /// </summary>
        public bool isCountry;
        /// <summary>
        /// Country key
        /// </summary>
        public string key;
        /// <summary>
        /// Country name
        /// </summary>
        public string name;
    }
}
