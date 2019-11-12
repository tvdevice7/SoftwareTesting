using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Card {
        int id;
        Player player;
        Match match;

        public Player Player {
            get { return player; }
            set { player = value; }
        }
    }
}
