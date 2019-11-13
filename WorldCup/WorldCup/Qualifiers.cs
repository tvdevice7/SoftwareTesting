using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Qualifiers : Round {
        public Qualifiers(List<Team> teams) : base(teams) {
            this.isKnockOut = false;
            groups = initializeGroups();
            scores = new int[8, 4];
            cards = new int[8, 4];
            goals = new int[8, 4];
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 4; j++) {
                    goals[i, j] = cards[i, j] = scores[i, j] = 0;
                }
            }
        }

        Team[,] groups;
        int[,] scores;
        int[,] cards;
        int[,] goals;

        public override List<Team> StartRound() {
            if (teams.Count != 32) throw new Exception("Invalid number of teams");
            List<Team> teamsGoOn = new List<Team>();
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 3; j++) {
                    for (int k = j + 1; k < 4; k++) {
                        Match m = new Match(groups[i, j], groups[i, k], false);
                        m.Compete();
                        matches.Add(m);
                        getMatchData(m, i, j, k);
                    }
                }
                Team[] rowDataTeam = { groups[i, 0], groups[i, 1], groups[i, 2], groups[i, 3] };
                List<Team> TeamsGoOnInGroup = chooseTeamsInGroup(rowDataTeam, scores.GetRow(i), cards.GetRow(i), goals.GetRow(i));
                foreach (Team t in TeamsGoOnInGroup) {
                    teamsGoOn.Add(t);
                }
            }
            return teamsGoOn;
        }


        Team[,] initializeGroups() {
            Team[,] groups = new Team[8, 4];
            for (int i = 0, k = 0; i < 8; i++) {
                for (int j = 0; j < 4; j++) {
                    groups[i, j] = teams[k];
                    k++;
                }
            };
            return groups;
        }

        void getMatchData(Match m, int groupIndex, int firstIndex, int secondIndex) {
            switch (m.Result) {
                case Result.FIRST_TEAM_WIN:
                    scores[groupIndex, firstIndex] += 3;
                    break;
                case Result.SECOND_TEAM_WIN:
                    scores[groupIndex, secondIndex] += 3;
                    break;
                case Result.DRAW:
                    scores[groupIndex, firstIndex] += 1;
                    scores[groupIndex, secondIndex] += 1;
                    break;
            }
            cards[groupIndex, firstIndex] += m.FirstTeamCard();
            cards[groupIndex, secondIndex] += m.SecondTeamCard();
            goals[groupIndex, firstIndex] += m.FirstTeamGoal() - m.SecondTeamGoal();
            goals[groupIndex, secondIndex] += m.SecondTeamGoal() - m.FirstTeamGoal();
        }

        List<Team> chooseTeamsInGroup(Team[] group, int[] scores, int[] cards, int[] goals) {
            List<Team> teams = new List<Team>();
            int highestScores = scores.Max();
            int idxHighestScores = Array.IndexOf(scores, highestScores);

            int idx2ndScores;
            if (idxHighestScores == 0) idx2ndScores = 1;
            else idx2ndScores = 0;
            int secondScores = scores[idx2ndScores];


            for (int i = idx2ndScores + 1; i < 4; i++) {
                if (i == idxHighestScores) continue;
                if (scores[i] > secondScores) {
                    secondScores = scores[i];
                    idx2ndScores = i;
                }
            }

            for (int i = idx2ndScores + 1; i < 4; i++) {
                if (i == idxHighestScores) continue;
                if (secondScores == scores[i]) {
                    if (goals[i] > goals[idx2ndScores]) idx2ndScores = i;
                    else if (goals[i] == goals[idx2ndScores]) {
                        if (cards[i] > cards[idx2ndScores]) idx2ndScores = i;
                        else if (cards[i] == cards[idx2ndScores]) {
                            Random rd = new Random();
                            int temp = rd.Next(1, 2);
                            if (temp == 2) idx2ndScores = i;
                        }
                    }
                }
            }

            teams.Add(group[idxHighestScores]);
            teams.Add(group[idx2ndScores]);

            return teams;
        }
    }
}
