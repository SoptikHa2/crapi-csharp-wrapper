using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    public class PlayerBattle
    {
        /// <summary>
        /// Type of battle ("2v2")
        /// </summary>
        public string type;
        /// <summary>
        /// Additional info about battle
        /// </summary>
        public Mode mode;
        /// <summary>
        /// Battle timestamp
        /// </summary>
        public int utcTime;
        /// <summary>
        /// TODO
        /// </summary>
        public string deckType;
        /// <summary>
        /// Size of team (1, 2)
        /// </summary>
        public int teamSize;
        /// <summary>
        /// TODO
        /// </summary>
        public int winner;
        /// <summary>
        /// Player's crowns
        /// </summary>
        public int teamCrowns;
        /// <summary>
        /// Opponent's crowns
        /// </summary>
        public int opponentCrowns;
        /// <summary>
        /// Array of players of one team
        /// </summary>
        public Player[] team;
        /// <summary>
        /// Array of players of second team
        /// </summary>
        public Player[] opponent;
        /// <summary>
        /// In which arena was the fight
        /// </summary>
        public Arena arena;
    }

    public struct Mode
    {
        /// <summary>
        /// Name of battle mode ("TeamVsTeamLadder")
        /// </summary>
        public string name;
        /// <summary>
        /// TODO
        /// </summary>
        public string deck;
        /// <summary>
        /// Tournament or normal card levels? ("Ladder")
        /// </summary>
        public string cardLevels;
        /// <summary>
        /// Bonus seconds in overtime
        /// </summary>
        public int overtimeSeconds;
        /// <summary>
        /// TODO
        /// </summary>
        public string players;
        /// <summary>
        /// TODO
        /// </summary>
        public bool sameDeck;
    }
}
