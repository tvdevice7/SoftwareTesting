using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Team {
        public Team(DataRow data) {
            this.ID = (int)data["ID"];
            this.Name = data["TenDoi"].ToString();
        }

        int id;
        string name;
        int numPlayers;
        int numOfficialPlayers;
        int numReservePlayers;
        int numAssistantCoach;

        public int ID {
            get { return id; }
            set { id = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }
    }
}
