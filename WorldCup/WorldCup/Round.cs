using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Round {
        protected int id;
        protected List<Team> teams;
        protected List<Match> matches;        
        protected bool isKnockOut;

        public Round() {
            teams = new List<Team>();
            matches = new List<Match>();
            isKnockOut = true;
        }
        public Round(List<Team> t) {
            teams = t;
            matches = new List<Match>();
            isKnockOut = true;
        }

        public virtual List<Team> StartRound() {
            try {
                if (teams.Count % 2 != 0) throw new Exception("Invalid number of teams");
                List<Team> teamsGoOn = new List<Team>();
                for (int i = 0; i < teams.Count; i += 2) {
                    Match m = new Match(teams[i], teams[i + 1], isKnockOut);
                    m.Compete();
                    matches.Add(m);
                    if (m.Winner == null) throw new Exception("Invalid match");
                    else teamsGoOn.Add(m.Winner);
                }
                return teamsGoOn;
            }
            catch (Exception err) {
                Console.WriteLine(err);
                return null;
            }
        }

        public List<Goal> GetGoals() {
            List<Goal> goals = new List<Goal>();
            foreach (Match m in matches) {
                goals.AddRange(m.Goals);
            }
            return goals;
        }
    }
}
