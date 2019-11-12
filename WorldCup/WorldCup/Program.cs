using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Program {
        static void Main(string[] args) {
            List<Region> regions = new List<Region>();

            Playoff playoffRound = new Playoff(regions);
            List<Team> teamsGoToQualifier = playoffRound.startRound();

            Qualifiers qualifiers = new Qualifiers(teamsGoToQualifier);
            List<Team> teamsGoToRoundOf16 = qualifiers.startRound();

            Round roundOf16 = new Round(teamsGoToRoundOf16);
            List<Team> teamsGoToQuarterFinal = roundOf16.startRound();

            Round quarterFinal = new Round(teamsGoToQuarterFinal);
            List<Team> teamsGoToSemiFinal = quarterFinal.startRound();

            Round semiFinal = new Round(teamsGoToSemiFinal);
            List<Team> teamsGoToFinal = semiFinal.startRound();

            Round final = new Round(teamsGoToFinal);
            Team champion = final.startRound()[0];
        }
    }
}
