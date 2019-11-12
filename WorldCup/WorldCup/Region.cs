using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Region {
        int id;
        string name;
        List<Team> teams;

        public Region(DataRow data) {
            this.ID = (int)data["ID"];
            this.Name = data["KhuVuc"].ToString();

        }
        public Region(int id, string name, List<Team> t) {
            this.ID = id;
            this.Name = name;
            this.Teams = t;
        }

        public int ID {
            get { return id; }
            set { id = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public List<Team> Teams {
            get { return teams; }
            set { teams = value; }
        }
    }
}
