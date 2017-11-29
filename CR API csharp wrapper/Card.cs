using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    /// <summary>
    /// Clash Royale Card class
    /// </summary>
    public class Card
    {
        public string name;
        public string rarity;
        public int level;
        public int count;
        public string requiredForUpgrade;
        public int card_id;
        public string key;
        public string card_key;
        public int elixir;
        public string type;
        public int arena;
        public string description;
        public string decklink;
        public int? leftToUpgrade;

        public override string ToString()
        {
            return name;
        }
    }
}
