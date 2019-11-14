using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    public class Team {
        public Team(int id, string name) {
            this.id = id;
            this.name = name;
        }
        public Team(DataRow data) {
            this.ID = (int)data["ID"];
            this.Name = data["TenDoi"].ToString();
        }

        int id;
        string name;
        List<Player> players;
        List<Player> officalPlayers;
        List<Player> reservePlayers;
        Coach coach;
        List<AssistantCoach> assistantCoach;
        Doctor doctor;

        public int ID {
            get { return id; }
            set { id = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public List<Player> Players {
            get { return players; }
            set { players = value; }
        }

        public List<Player> OfficalPlayers {
            get { return officalPlayers; }
            set { officalPlayers = value; }
        }

        public List<Player> ReservePlayers {
            get { return reservePlayers; }
            set { reservePlayers = value; }
        }

        public Coach Coach {
            get { return coach; }
            set { coach = value; }
        }
        public Doctor Doctor {
            get { return doctor; }
            set { doctor = value; }
        }
        public List<AssistantCoach> AssistantCoach {
            get { return assistantCoach; }
            set { assistantCoach = value; }
        }
        public void checkTeam() {
            if (assistantCoach.Count > 3) throw new Exception("Invalid number of assistant coach");
            if (players.Count > 22) throw new Exception("Invalid number of player");
        }
    }
}
