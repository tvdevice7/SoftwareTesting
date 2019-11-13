using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Playoff : Round {
        public Playoff(List<Region> regions) {
            this.isKnockOut = false;
            this.regions = regions;
            foreach (Region r in this.regions) {
                foreach (Team t in r.Teams) {
                    teams.Add(t);
                }
            }
            if (teams.Count != 34) throw new Exception("Invalid numbers of team before Playoff Round");
        }
        List<Region> regions;

        List<Team> GetTeamPlay() {
            List<Team> teams = new List<Team>();
            teams.Add(regions[0].Teams[5]);
            teams.Add(regions[2].Teams[3]);
            teams.Add(regions[3].Teams[3]);
            teams.Add(regions[4].Teams[0]);

            return teams;
        }
        public Team GetTeamOff(Match m1, Match m2) {
            if (m1.Result == Result.FIRST_TEAM_WIN && m2.Result == Result.SECOND_TEAM_WIN) {
                return m1.SecondTeam;
            }
            else if (m1.Result == Result.SECOND_TEAM_WIN && m2.Result == Result.FIRST_TEAM_WIN) {
                return m1.FirstTeam;
            }
            else {
                int firstGoal = m1.FirstTeamGoal() + m2.SecondTeamGoal();
                int secondGoal = m1.SecondTeamGoal() + m2.FirstTeamGoal();
                if (firstGoal > secondGoal) return m1.SecondTeam;
                else if (firstGoal < secondGoal) return m1.FirstTeam;
                else {
                    int firstCard = m1.FirstTeamCard() + m2.SecondTeamCard();
                    int secondCard = m1.SecondTeamCard() + m2.FirstTeamCard();
                    if (firstCard < secondCard) return m1.SecondTeam;
                    else if (firstCard > secondCard) return m1.FirstTeam;
                    else {
                        Random rd = new Random();
                        int temp = rd.Next(1, 2);
                        if (temp == 1) return m1.SecondTeam;
                        else return m1.FirstTeam;
                    }
                }
            }
        }
        public List<Team> GetTeamGoOn(List<Team> teamOff) {
            List<Team> teamsGoOn = teams;             
            foreach (Team t in teamOff) {
                teamsGoOn.Remove(t);
            }
            return teamsGoOn;
        }

        public override List<Team> StartRound() {
            try {
                if (regions.Count != 7) throw new Exception("Invalid Number of Region");
                List<Team> teamPlay = GetTeamPlay();
                Match m1 = new Match(teamPlay[0], teamPlay[1], false);
                m1.Compete();
                matches.Add(m1);
                Match m2 = new Match(teamPlay[1], teamPlay[0], false);
                m2.Compete();
                matches.Add(m2);
                Match m3 = new Match(teamPlay[2], teamPlay[3], false);
                m3.Compete();
                matches.Add(m3);
                Match m4 = new Match(teamPlay[3], teamPlay[2], false);
                m4.Compete();
                matches.Add(m4);

                List<Team> teamOff = new List<Team>();
                teamOff.Add(GetTeamOff(m1, m2));
                teamOff.Add(GetTeamOff(m3, m4));

                List<Team> teamsGoOn = GetTeamGoOn(teamOff);                
                return teamsGoOn;
            }
            catch (Exception err) {
                Console.WriteLine(err);
                return null;
            }
        }
    }
}
