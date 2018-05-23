using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    [Serializable]
    public class ClanWarInfo
    {
        /// <summary>
        /// "collectionDay" or "warDay" - represents current clan war status
        /// </summary>
        public string state;
        /// <summary>
        /// Clan info, contains basic info about this clan war
        /// </summary>
        public ClanWarInfoClan clan;
        /// <summary>
        /// Participants in this clan war
        /// </summary>
        public Participant[] participants;
    }

    [Serializable]
    public struct ClanWarInfoClan
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
        /// Number of players active in this war
        /// </summary>
        public int participants;
        /// <summary>
        /// Total number of played battles in this war state
        /// </summary>
        public int battlesPlayed;
        /// <summary>
        /// Total number of wins in this war state
        /// </summary>
        public int wins;
        /// <summary>
        /// Total number of crowns in this war state
        /// </summary>
        public int crowns;
        /// <summary>
        /// Clan total war trophies
        /// </summary>
        public int warTrophies;
        /// <summary>
        /// Clan trophies outcome from this war. !! This is null when the war is still running !!
        /// </summary>
        public int? warTrophiesChange;
        /// <summary>
        /// Clan badge
        /// </summary>
        public ClanBadge badge;
    }

    [Serializable]
    public struct Participant
    {
        /// <summary>
        /// Players tag
        /// </summary>
        public string tag;
        /// <summary>
        /// Players name
        /// </summary>
        public string name;
        /// <summary>
        /// Cards earned by player
        /// </summary>
        public int cardsEarned;
        /// <summary>
        /// Battles played by player in this war state
        /// </summary>
        public int battlesPlayed;
        /// <summary>
        /// Player wins in this war state
        /// </summary>
        public int wins;
    }
}
