using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    public class ClanPlayer
    {
        public string name;
        public Arena arena;
        public int role;
        public int expLevel;
        public int trophies;
        public int donations;
        public int currentRank;
        public int previousRank;
        public int clanChestCrowns;
        public string tag;
        public string roleName;
        public int score;

        public override string ToString()
        {
            return name;
        }
    }
}
