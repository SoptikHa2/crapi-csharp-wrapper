﻿using System;
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
        /// <summary>
        /// Clan TAG
        /// </summary>
        public string tag;
        /// <summary>
        /// Clan name
        /// </summary>
        public string name;
        /// <summary>
        /// TODO
        /// </summary>
        public string role;
        /// <summary>
        /// Clan badge
        /// </summary>
        public ClanBadge badge;

        /// <summary>
        /// Returns string representation of clan summary, returns clan name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }
    }
}
