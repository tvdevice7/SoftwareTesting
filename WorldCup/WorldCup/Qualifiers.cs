using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Qualifiers : Round {
        public Qualifiers(List<Team> teams) : base(teams) {
            this.isKnockOut = false;
        }
        public override List<Team> startRound() {
            try {
                return null;
            }
            catch (Exception err) {
                Console.WriteLine(err);
                return null;
            }
        }
    }
}
