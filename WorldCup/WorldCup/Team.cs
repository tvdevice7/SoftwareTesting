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
        List<Player> players;
        List<Player> officalPlayers;
        List<Player> reservePlayers;
        int numAssistantCoach;

        public int ID {
            get { return id; }
            set { id = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public List<Player> Players {
            get {return players;}
            set {players = value;}
        }

        public List<Player> OfficalPlayers {
            get {return officalPlayers;}
            set {officalPlayers = value;}
        }

        public List<Player> ReservePlayers {
            get {return reservePlayers;}
            set {reservePlayers = value;}
        }

        public int NumberOfPlayers()
        {
            return Players.Count();
        }

        public int NumberOfOfficial()
        {
            return OfficalPlayers.Count();
        }

        public int NumberOfReserve()
        {
            return ReservePlayers.Count();
        }
    }
}
