using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Player {
        int id;
        string name;
        Team team;

        public Player(DataRow data) {
            this.ID = (int)data["ID"];
            this.Name = data["CauThu"].ToString();
        }

        public int ID {
            get { return id; }
            set { id = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        internal Team Team {
            get { return team; }
            set { team = value; }
        }
    }
}
