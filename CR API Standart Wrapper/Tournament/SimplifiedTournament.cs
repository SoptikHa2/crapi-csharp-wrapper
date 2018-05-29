using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Tournament as is returned from Open Tournaments endpoint
    /// </summary>
    public class SimplifiedTournament
    {
        /// <summary>
        /// Tournament tag, request it to get full tournament response
        /// </summary>
        public string tag;
        /// <summary>
        /// Tournament type, eg open
        /// </summary>
        public string type;
        /// <summary>
        /// Tournament status, eg inProgress, inPreparation, etc
        /// </summary>
        public string status;
        /// <summary>
        /// Tournament name
        /// </summary>
        public string name;
        /// <summary>
        /// This seems to be always equal to <see cref="playerCount"/>. If you know what this stands for, please go to github (/SoptikHa2/crapi-csharp-wrapper/) and submit new issue. Thanks!
        /// </summary>
        public int capacity;
        /// <summary>
        /// Current count of players
        /// </summary>
        public int playerCount;
        /// <summary>
        /// Maximum count of players
        /// </summary>
        public int maxCapacity;
        /// <summary>
        /// Duration of preparation state in seconds
        /// </summary>
        public int preparationDuration;
        /// <summary>
        /// Duration of tournament in seconds
        /// </summary>
        public int duration;
        /// <summary>
        /// Time when tournament ended in normal format. Example: 1514346417. May be null.
        /// </summary>
        public int? endTime;
        /// <summary>
        /// Time when tournament started in normal format. Example: 1514342817. May be null.
        /// </summary>
        public int? startTime;
        /// <summary>
        /// Time when was tournament created in normal format. Example: 1514338911. May be null.
        /// </summary>
        public int? createTime;
    }
}
