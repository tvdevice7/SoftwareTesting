using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Region {
        int id;
        string name;
        List<Team> teams;
        public Region(int id, string name, List<Team> t) {
            this.id = id;
            this.name = name;
            this.Teams = t;
        }

        public List<Team> Teams {
            get { return teams; }
            set { teams = value; }
        }
    }
}
