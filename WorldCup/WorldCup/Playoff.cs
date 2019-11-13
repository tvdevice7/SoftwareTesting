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
        }
        List<Region> regions;
        public override List<Team> StartRound() {
            try {
                if (regions.Count != 7) throw new Exception("Invalid Number of Region");
                Team sixthAFC = regions[0].Teams[5];
                Team fourthCONCACAF = regions[2].Teams[3];
                Team fourthCONMEBOL = regions[3].Teams[3];
                Team firstOFC = regions[4].Teams[0];

                Match m1 = new Match(sixthAFC, fourthCONCACAF, false);
                m1.Compete();
                Match m2 = new Match(fourthCONCACAF, sixthAFC, false);
                m2.Compete();
                Match m3 = new Match(fourthCONMEBOL, firstOFC, false);
                m3.Compete();
                Match m4 = new Match(firstOFC, fourthCONMEBOL, false);
                m4.Compete();
                matches.Add(m1);
                matches.Add(m2);
                matches.Add(m3);
                matches.Add(m4);

                if (m1.Result == Result.FIRST_TEAM_WIN && m2.Result == Result.FIRST_TEAM_WIN) {
                    regions[2].Teams.Remove(fourthCONCACAF);
                }
                else if (m1.Result == Result.SECOND_TEAM_WIN && m2.Result == Result.SECOND_TEAM_WIN) {
                    regions[0].Teams.Remove(sixthAFC);
                }
                else {
                    int sixthAFCGoal = m1.FirstTeamGoal() + m2.SecondTeamGoal();
                    int fourthCONCACAFGoal = m1.SecondTeamGoal() + m2.FirstTeamGoal();
                    if (sixthAFCGoal > fourthCONCACAFGoal) regions[2].Teams.Remove(fourthCONCACAF);
                    else if (sixthAFCGoal < fourthCONCACAFGoal) regions[0].Teams.Remove(sixthAFC);
                    else {
                        int sixthAFCCard = m1.FirstTeamCard() + m2.SecondTeamCard();
                        int fourthCONCACAFCard = m1.SecondTeamCard() + m2.FirstTeamCard();
                        if (sixthAFCCard > fourthCONCACAFCard) regions[2].Teams.Remove(fourthCONCACAF);
                        else if (sixthAFCCard < fourthCONCACAFCard) regions[0].Teams.Remove(sixthAFC);
                        else {
                            Random rd = new Random();
                            int temp = rd.Next(1, 2);
                            if (temp == 1) regions[2].Teams.Remove(fourthCONCACAF);
                            else regions[0].Teams.Remove(sixthAFC);
                        }
                    }
                }

                List<Team> teamsGoOn = new List<Team>();
                foreach(Region r in regions) {
                    foreach (Team t in r.Teams) {
                        teamsGoOn.Add(t);
                    }
                }
                return teamsGoOn;
            }
            catch (Exception err) {
                Console.WriteLine(err);
                return null;
            }
        }
    }
}
