using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    public class ClanWarsRecord
    {
        /// <summary>
        /// Participants in this clan war
        /// </summary>
        public Participant[] participants;
        /// <summary>
        /// Clans ordered by their placement in the war. (1st is index 0, 2nd is index 1, ...)
        /// </summary>
        public ClanWarInfoClan[] standings;
        /// <summary>
        /// Season number
        /// </summary>
        public int seasonNumber;
        /// <summary>
        /// Unix timestamp, when the clan war started
        /// </summary>
        public int createdDate;
    }
}
