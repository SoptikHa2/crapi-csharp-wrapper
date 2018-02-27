using System;

namespace CRAPI
{
    [Serializable]
    public class Tournament
    {
        /// <summary>
        /// Tournament TAG
        /// </summary>
        public string tag;

        /// <summary>
        /// Tournament tag, for example "passwordProtected";
        /// </summary>
        public string type;

        /// <summary>
        /// Tournament current status, for example "ended";
        /// </summary>
        public string status;

        /// <summary>
        /// Tournament name
        /// </summary>
        public string name;

        /// <summary>
        /// Tournament description
        /// </summary>
        public string description;

        /// <summary>
        /// Current number of players in tournament
        /// </summary>
        public int playerCount;

        /// <summary>
        /// Maximum number of players in tournament
        /// </summary>
        public int maxCapacity;

        /// <summary>
        /// Length of duration in seconds
        /// </summary>
        public int preparationDuration;

        /// <summary>
        /// Length of tournament in seconds
        /// </summary>
        public int duration;

        /// <summary>
        /// Time when tournament ended in normal format. Example: 1514346417. May be null
        /// </summary>
        public int? endTime;

        /// <summary>
        /// Time when tournament started in normal format. Example: 1514342817. May be null
        /// </summary>
        public int? startTime;

        /// <summary>
        /// Time when was tournament created in normal format. Example: 1514338911. May be null
        /// </summary>
        public int? createTime;

        /// <summary>
        /// Creator of this tournament
        /// </summary>
        public TournamentPlayer creator;

        /// <summary>
        /// Players inside this tournament
        /// </summary>
        public TournamentPlayer[] members;
    }

    [Serializable]
    public struct TournamentPlayer
    {
        /// <summary>
        /// Player's TAG. Use this to gather full player info
        /// </summary>
        public string tag;

        /// <summary>
        /// Player's name
        /// </summary>
        public string name;

        /// <summary>
        /// Player's inside-tournament trophies
        /// </summary>
        public int score;

        /// <summary>
        /// Player's rank inside tournament
        /// </summary>
        public int rank;

        /// <summary>
        /// Short info about player's clan
        /// </summary>
        public ClanInfo clan;

        public override string ToString()
        {
            return name;
        }
    }
}
