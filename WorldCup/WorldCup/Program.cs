using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Program {
        static void Main(string[] args) {
            List<Region> regions = new List<Region>();
            regions = DataLoader.Instance.LoadRegions();

            Playoff playoffRound = new Playoff(regions);
            List<Team> teamsGoToQualifier = playoffRound.startRound();
            Console.WriteLine("Ket qua vong playoff: ");
            foreach (Team t in teamsGoToQualifier) {
                Console.WriteLine(t.Name);
            }
            Console.WriteLine("--------------------------------------------------------");

            Qualifiers qualifiers = new Qualifiers(teamsGoToQualifier);
            List<Team> teamsGoToRoundOf16 = qualifiers.startRound();
            Console.WriteLine("Ket qua vong bang: ");
            foreach (Team t in teamsGoToRoundOf16) {
                Console.WriteLine(t.Name);
            }
            Console.WriteLine("--------------------------------------------------------");

            Round roundOf16 = new Round(teamsGoToRoundOf16);
            List<Team> teamsGoToQuarterFinal = roundOf16.startRound();
            Console.WriteLine("Ket qua vong 16: ");
            foreach (Team t in teamsGoToQuarterFinal) {
                Console.WriteLine(t.Name);
            }
            Console.WriteLine("--------------------------------------------------------");

            Round quarterFinal = new Round(teamsGoToQuarterFinal);
            List<Team> teamsGoToSemiFinal = quarterFinal.startRound();
            Console.WriteLine("Ket qua vong tu ket: ");
            foreach (Team t in teamsGoToSemiFinal) {
                Console.WriteLine(t.Name);
            }
            Console.WriteLine("--------------------------------------------------------");

            Round semiFinal = new Round(teamsGoToSemiFinal);
            List<Team> teamsGoToFinal = semiFinal.startRound();
            Console.WriteLine("Ket qua vong ban ket: ");
            foreach (Team t in teamsGoToFinal) {
                Console.WriteLine(t.Name);
            }
            Console.WriteLine("--------------------------------------------------------");

            Round final = new Round(teamsGoToFinal);
            Team champion = final.startRound()[0];
            Console.WriteLine("Ket qua vong chung ket: ");
            Console.WriteLine(champion.Name);
            Console.WriteLine("--------------------------------------------------------");

            Console.ReadKey();
        }
    }
}
