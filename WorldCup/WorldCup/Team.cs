using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Team {
        int id;
        string name;
        int numPlayers;
        int numOfficialPlayers;
        int numReservePlayers;
        int numAssistantCoach;

        public int ID {
            get { return id; }
            set { id = value;}
        }
    }
}
