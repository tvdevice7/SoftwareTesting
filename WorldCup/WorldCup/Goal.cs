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

        internal Player Scorer {
            get { return scorer; }
            set { scorer = value; }
        }
    }
}
