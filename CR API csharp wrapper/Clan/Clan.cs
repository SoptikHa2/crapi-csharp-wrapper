using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Clash Royale Clan class
    /// </summary>
    public class Clan
    {
        public string name;
        public ClanBadge badge;
        public int type;
        public int memberCount;
        public int score;
        public int requiredScore;
        public int donations;
        public int currentRank;
        public string description;
        public string tag;
        public string typeName;
        public ClanRegion region;
        public ClanChest clanChest;
        public ClanPlayer[] members;

        public static Clan GetClan(string tag)
        {
            return Inside.APIworker.Parse<Clan>(Inside.APIworker.Get(Inside.APIworker.Endpoints.Clan, tag));
        }

        public override string ToString()
        {
            return name;
        }
    }
}
