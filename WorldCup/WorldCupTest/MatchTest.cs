using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorldCup;
using System.Collections.Generic;

namespace WorldCupTest {
    [TestClass]
    public class MatchTest {
        Team team1;
        Team team2;
        public MatchTest() {
            this.team1 = this.createTeam(1, "Viet Nam");
            this.team2 = this.createTeam(2, "Thailand");
        }

        private Team createTeam(int id, string name) {
            Team team = new Team(id, name);
            List<Player> players = new List<Player>();
            int numPlayer = (new Random()).Next(16, 21);
            for (var i = 0; i < numPlayer; i++) {
                Player p = new Player(i + 1, Gen.genString((new Random()).Next(16, 21)), team);
                players.Add(p);
            }

            team.Players = players;
            return team;
        }

        [TestMethod]
        public void Match_FirstTeamHaveGoal_UpdateScore() {

            Match match = new Match(team1, team2, false);
            match.firstTeamScore();

            Assert.AreEqual(1, match.FirstTeamGoal());
            Assert.AreEqual(0, match.SecondTeamGoal());
        }

        [TestMethod]
        public void Match_SecondTeamHaveGoal_UpdateScore() {

            Match match = new Match(team1, team2, false);
            match.secondTeamScore();
            Assert.AreEqual(0, match.FirstTeamGoal());
            Assert.AreEqual(1, match.SecondTeamGoal());
        }
    }
}
