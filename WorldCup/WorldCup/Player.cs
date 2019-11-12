using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Player {
        int id;
        Team team;

        public Team Team {
            get { return team; }
            set { team = value; }
        }
    }
}
