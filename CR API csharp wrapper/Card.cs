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
        /// Name of card
        /// </summary>
        public string name;
        /// <summary>
        /// Rarity of caard
        /// </summary>
        public string rarity;
        /// <summary>
        /// Level of card
        /// </summary>
        public int level;
        /// <summary>
        /// Number of cards, that player have
        /// </summary>
        public int count;
        /// <summary>
        /// Number of cards that player needs to upgrade this card (can be "Maxed" instead of number when the card is on maximum level)
        /// </summary>
        public string requiredForUpgrade;
        /// <summary>
        /// Card ID
        /// </summary>
        public int card_id;
        /// <summary>
        /// Card key (name of card)
        /// </summary>
        public string key;
        /// <summary>
        /// Card key (name of card)
        /// </summary>
        public string card_key;
        /// <summary>
        /// Card elixir cost
        /// </summary>
        public int elixir;
        /// <summary>
        /// Card type (like "Troop")
        /// </summary>
        public string type;
        /// <summary>
        /// Card arena
        /// </summary>
        public int arena;
        /// <summary>
        /// Card description
        /// </summary>
        public string description;
        /// <summary>
        /// TODO
        /// </summary>
        public string decklink;
        /// <summary>
        /// TODO
        /// </summary>
        public int? leftToUpgrade;

        /// <summary>
        /// Returns string reprezentation of card, return card's name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }
    }
}
