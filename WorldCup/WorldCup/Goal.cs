using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Goal {
        int id;
        Player scorer;
        Match match;        

        public int Id {
            get { return id; }
            set { id = value; }
        }

        public Player Scorer {
            get { return scorer; }
            set { scorer = value; }
        }

        public Match Match {
            get { return match; }
            set { match = value; }
        }
    }
}
