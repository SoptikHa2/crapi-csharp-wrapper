using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Clash Royale Card
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Name of card ("Rocket")
        /// </summary>
        public string name;
        /// <summary>
        /// Current card level
        /// </summary>
        public int level;
        /// <summary>
        /// Maximum level of card
        /// </summary>
        public int maxLevel;
        /// <summary>
        /// Number of cards in player's inventory
        /// </summary>
        public int count;
        /// <summary>
        /// Url to card image
        /// </summary>
        public IconUrls iconUrls;
        /// <summary>
        /// Key of card ("rocket")
        /// </summary>
        public string key;
        /// <summary>
        /// Card cost
        /// </summary>
        public int elixir;
        /// <summary>
        /// Type of card ("Spell")
        /// </summary>
        public string type;
        /// <summary>
        /// Number of arena, where is card unlocked (3)
        /// </summary>
        public int arena;
        /// <summary>
        /// Card description
        /// </summary>
        public string description;
        /// <summary>
        /// Card rarity ("Epic")
        /// </summary>
        public string rarity;

        /// <summary>
        /// Returns string reprezentation of card, return card's name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }
    }

    public struct IconUrls
    {
        /// <summary>
        /// TODO
        /// </summary>
        public string medium;
    }
}
