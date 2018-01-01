using System;

namespace CRAPI
{
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
        public int capacity;

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
        /// Time when tournament ended in normal format. Example: 1514346417
        /// </summary>
        public int endTime;

        /// <summary>
        /// Time when tournament started in normal format. Example: 1514342817
        /// </summary>
        public int startTime;

        /// <summary>
        /// Time when was tournament created in normal format. Example: 1514338911
        /// </summary>
        public int createTime;

        /// <summary>
        /// Creator of this tournament
        /// </summary>
        public TournamentPlayer creator;

        /// <summary>
        /// Players inside this tournament
        /// </summary>
        public TournamentPlayer[] members;
    }

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

        public override string ToString()
        {
            return name;
        }
    }
}
