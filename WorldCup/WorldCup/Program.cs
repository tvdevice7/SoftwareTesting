using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Program {
        static void Main(string[] args) {
            try {
                List<Region> regions = new List<Region>();
                regions = DataLoader.Instance.LoadRegions();

                Playoff playoffRound = new Playoff(regions);
                List<Team> teamsGoToQualifier = playoffRound.StartRound();
                Console.WriteLine("*****************************Ket qua vong playoff: *****************************");
                Console.WriteLine("---->Ket qua cac tran dau:");
                playoffRound.printHappening();
                Console.WriteLine("---->Cac doi di tiep:");
                printInfo(teamsGoToQualifier);
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------");

                Qualifiers qualifiers = new Qualifiers(teamsGoToQualifier);
                List<Team> teamsGoToRoundOf16 = qualifiers.StartRound();
                Console.WriteLine("*****************************Ket qua vong bang: *****************************");
                Console.WriteLine("---->Ket qua cac tran dau:");
                qualifiers.printHappening();
                Console.WriteLine("---->Cac doi di tiep:");
                printInfo(teamsGoToRoundOf16);
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------");

                Round roundOf16 = new Round(teamsGoToRoundOf16);
                List<Team> teamsGoToQuarterFinal = roundOf16.StartRound();
                Console.WriteLine("*****************************Ket qua vong 16: *****************************");
                Console.WriteLine("---->Ket qua cac tran dau:");
                roundOf16.printHappening();
                Console.WriteLine("---->Cac doi di tiep:");
                printInfo(teamsGoToQuarterFinal);
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------");

                Round quarterFinal = new Round(teamsGoToQuarterFinal);
                List<Team> teamsGoToSemiFinal = quarterFinal.StartRound();
                Console.WriteLine("*****************************Ket qua vong tu ket: *****************************");
                Console.WriteLine("---->Ket qua cac tran dau:");
                quarterFinal.printHappening();
                Console.WriteLine("---->Cac doi di tiep:");
                printInfo(teamsGoToSemiFinal);
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------");

                Round semiFinal = new Round(teamsGoToSemiFinal);
                List<Team> teamsGoToFinal = semiFinal.StartRound();
                Console.WriteLine("*****************************Ket qua vong ban ket: *****************************");
                Console.WriteLine("---->Ket qua cac tran dau:");
                semiFinal.printHappening();
                Console.WriteLine("---->Cac doi di tiep:");
                printInfo(teamsGoToFinal);
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------");

                Round final = new Round(teamsGoToFinal);
                Team champion = final.StartRound()[0];
                Console.WriteLine("*****************************Ket qua vong chung ket: *****************************");
                Console.WriteLine("---->Ket qua cac tran dau:");
                final.printHappening();
                Console.WriteLine("---->Doi vo dich:");
                Console.WriteLine(champion.Name);
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------");

                List<Goal> goals = new List<Goal>();
                goals.AddRange(qualifiers.GetGoals());
                goals.AddRange(roundOf16.GetGoals());
                goals.AddRange(quarterFinal.GetGoals());
                goals.AddRange(semiFinal.GetGoals());
                goals.AddRange(final.GetGoals());
                Player topScorer = goals[0].Scorer;
                foreach (Goal g in goals) {
                    if (g.Scorer.GoalsScored > topScorer.GoalsScored) topScorer = g.Scorer;
                }
                Console.WriteLine("Vua pha luoi: ");
                Console.WriteLine(topScorer.Name);
            }
            catch (Exception err) {
                Console.WriteLine(err.Message);
            }

            Console.ReadKey();
        }
        static void printInfo(List<Team> team) {
            for (int i = 0; i < team.Count; i++) {
                if (i != team.Count - 1) Console.Write(team[i].Name + ", ");
                else Console.WriteLine(team[i].Name);
                if (i % 5 == 4) Console.WriteLine("");
            }
        }
    }
}
