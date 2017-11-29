using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Clash Royale Arena class
    /// </summary>
    public class Arena
    {
        public string imageUrl;
        public string arena;
        public int arenaID;
        public string name;
        public int trophyLimit;

        public override string ToString()
        {
            return name;
        }
    }
}
