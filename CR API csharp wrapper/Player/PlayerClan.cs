using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Short clan description (clan tag, name, player's role and badge) from player's profile view
    /// </summary>
    public class PlayerClan
    {
        public string tag;
        public string name;
        public string role;
        public ClanBadge badge;

        public override string ToString()
        {
            return name;
        }
    }
}
