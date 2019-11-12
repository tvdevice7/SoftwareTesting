using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    public enum Result {
        FIRST_TEAM_WIN, SECOND_TEAM_WIN, DRAW
    }
    class Match {
        int id;
        int time;
        Team firstTeam;
        Team secondTeam;
        List<Card> cards;
        List<Goal> goals;
        Result result;
        Match[] extraMatch;

        public Match(Team firstTeam, Team secondTeam, bool isKnockOut) {
            this.firstTeam = firstTeam;
            this.secondTeam = secondTeam;
        }

        public void Compete() {
            for (int i = 0; i < 90; i++) {

            }
        }

        public Result Result {
            get { return result; }
        }
        public Team Winner {
            get {
                if (result == Result.FIRST_TEAM_WIN) return firstTeam;
                return null;
            }
        }
        public int FirstTeamGoal() {
            int firstTeamGoal = 0;
            foreach (Goal g in goals) {
                if (g.Scorer.Team.ID == firstTeam.ID) {
                    firstTeamGoal++;
                }
            }
            return firstTeamGoal;
        }
        public int FirstTeamCard() {
            int firstTeamCard = 0;
            foreach (Card c in cards) {
                if (c.Player.Team.ID == firstTeam.ID) {
                    firstTeamCard++;
                }
            }
            return firstTeamCard;
        }
        public int SecondTeamCard() {
            int secondTeamCard = 0;
            foreach (Card c in cards) {
                if (c.Player.Team.ID == secondTeam.ID) {
                    secondTeamCard++;
                }
            }
            return secondTeamCard;
        }
        public int SecondTeamGoal() {
            int secondTeamGoal = 0;
            foreach (Goal g in goals) {
                if (g.Scorer.Team.ID == secondTeam.ID) {
                    secondTeamGoal++;
                }
            }
            return secondTeamGoal;
        }
    }
}
