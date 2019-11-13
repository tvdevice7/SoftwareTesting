using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Program {
        static void Main(string[] args) {
            //try {
                List<Region> regions = new List<Region>();
                regions = DataLoader.Instance.LoadRegions();

                Playoff playoffRound = new Playoff(regions);
                List<Team> teamsGoToQualifier = playoffRound.StartRound();
                Console.WriteLine("Ket qua vong playoff: ");
                foreach (Team t in teamsGoToQualifier) {
                    Console.WriteLine(t.Name);
                }
                Console.WriteLine("--------------------------------------------------------");

                Qualifiers qualifiers = new Qualifiers(teamsGoToQualifier);
                List<Team> teamsGoToRoundOf16 = qualifiers.StartRound();
                Console.WriteLine("Ket qua vong bang: ");
                foreach (Team t in teamsGoToRoundOf16) {
                    Console.WriteLine(t.Name);
                }
                Console.WriteLine("--------------------------------------------------------");

                Round roundOf16 = new Round(teamsGoToRoundOf16);
                List<Team> teamsGoToQuarterFinal = roundOf16.StartRound();
                Console.WriteLine("Ket qua vong 16: ");
                foreach (Team t in teamsGoToQuarterFinal) {
                    Console.WriteLine(t.Name);
                }
                Console.WriteLine("--------------------------------------------------------");

                Round quarterFinal = new Round(teamsGoToQuarterFinal);
                List<Team> teamsGoToSemiFinal = quarterFinal.StartRound();
                Console.WriteLine("Ket qua vong tu ket: ");
                foreach (Team t in teamsGoToSemiFinal) {
                    Console.WriteLine(t.Name);
                }
                Console.WriteLine("--------------------------------------------------------");

                Round semiFinal = new Round(teamsGoToSemiFinal);
                List<Team> teamsGoToFinal = semiFinal.StartRound();
                Console.WriteLine("Ket qua vong ban ket: ");
                foreach (Team t in teamsGoToFinal) {
                    Console.WriteLine(t.Name);
                }
                Console.WriteLine("--------------------------------------------------------");

                Round final = new Round(teamsGoToFinal);
                Team champion = final.StartRound()[0];
                Console.WriteLine("Ket qua vong chung ket: ");
                Console.WriteLine(champion.Name);
                Console.WriteLine("--------------------------------------------------------");

                List<Goal> goals = new List<Goal>();
                goals.AddRange(qualifiers.GetGoals());
                goals.AddRange(roundOf16.GetGoals());
                goals.AddRange(quarterFinal.GetGoals());
                goals.AddRange(semiFinal.GetGoals());
                goals.AddRange(final.GetGoals());
                Player topScorer = goals[0].Scorer;
                foreach(Goal g in goals) {
                    if (g.Scorer.GoalsScored > topScorer.GoalsScored) topScorer = g.Scorer;
                }
                Console.WriteLine("Vua pha luoi: ");
                Console.WriteLine(topScorer.Name);
                Console.WriteLine("--------------------------------------------------------");
            //}
            //catch (Exception err) {
            //    Console.WriteLine(err.Message);
            //}            

            Console.ReadKey();
        }
    }
}
