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
        bool isRedCard;

        public int Id {
            get { return id; }
            set { id = value; }
        }
        public Player Player {
            get { return player; }
            set { player = value; }
        }

        public Match Match {
            get { return match; }
            set { match = value; }
        }

        public bool IsRedCard {
            get { return isRedCard; }
            set { isRedCard = value; }
        }
    }
}
