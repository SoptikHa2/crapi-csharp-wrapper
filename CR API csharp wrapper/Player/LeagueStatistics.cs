using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    [Serializable]
    public class LeagueStatistics
    {
        /// <summary>
        /// Current season data. May be NULL. In this season, [id] and [bestTrohpies] are always NULL.
        /// </summary>
        public Season? currentSeason;
        /// <summary>
        /// Previous season data. May be NULL.
        /// </summary>
        public Season? previousSeason;
        /// <summary>
        /// Best season data. May be NULL. In this season, [bestTrophies] is always NULL.
        /// </summary>
        public Season? bestSeason;
    }

    [Serializable]
    public struct Season
    {
        /// <summary>
        /// Season rank
        /// </summary>
        public int rank;
        /// <summary>
        /// Season trophies
        /// </summary>
        public int trophies;
        /// <summary>
        /// Season ID, something like "2017-07". In [currentSeason] is this NULL!
        /// </summary>
        public string id;
        /// <summary>
        /// Best trophies of season. Is NULL everywhere except [previousSeason]
        /// </summary>
        public int? bestTrophies;
    }
}
