using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorldCup;
using System.Collections.Generic;

namespace WorldCupTest {
    [TestClass]
    public class MatchTest {
        Team A;
        Team B;
        public MatchTest() {
            this.A = this.createTeam(0, "A");
            this.B = this.createTeam(1, "B");
        }

        //[TestMethod]
        //public void MatchCreationTest() {
        //    Match test = new Match(A, B, false);
        //    bool result = false;
        //    if (test.Result == Result.NONE) {
        //        result = true;
        //    }

        //    Assert.IsTrue(result);
        //}

        [TestMethod]
        public void OfficialTeamPicker_Team1() {
            Match test = new Match(A, B, false);
            bool result = false;
            if (A.OfficalPlayers.Count == 11) {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OfficialTeamPicker_Team2() {
            Match test = new Match(A, B, false);
            bool result = false;
            if (B.OfficalPlayers.Count == 11) {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReserveTeamPicker_Team1() {
            Match test = new Match(A, B, false);
            bool result = false;
            if (A.ReservePlayers.Count == 5) {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReserveTeamPicker_Team2() {
            Match test = new Match(A, B, false);
            bool result = false;
            if (B.ReservePlayers.Count == 5) {
                result = true;
            }

            Assert.IsTrue(result);
        }

        //[TestMethod]
        //public void MatchExecutionTest() {
        //    Match test = new Match(A, B, false);
        //    bool result = false;
        //    test.Compete();
        //    if (test.Result != Result.NONE) {
        //        result = true;
        //    }

        //    Assert.IsTrue(result);
        //}

        [TestMethod]
        public void GoalCheckerTest_Team1() {
            Match test = new Match(A, B, false);
            bool result = false;
            test.Compete();
            if (test.FirstTeamGoal() == (test.Goals.Count - test.SecondTeamGoal())) {
                result = true;
            }
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GoalCheckerTest_Team2() {
            Match test = new Match(A, B, false);
            bool result = false;
            test.Compete();
            if (test.SecondTeamGoal() == (test.Goals.Count - test.FirstTeamGoal())) {
                result = true;
            }
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OutOfPlayer_Team1() {
            Match test = new Match(A, B, false);
            bool result = false;
            for (int i = 0; i < 9; i++) {
                test.SwapPlayerFirstTeam(0, true);
            }
            Assert.IsFalse(test.SwapPlayerFirstTeam(0, true));
        }
        [TestMethod]
        public void OutOfPlayer_Team2() {
            Match test = new Match(A, B, false);
            bool result = false;
            for (int i = 0; i < 9; i++) {
                test.SwapPlayerSecondTeam(0, true);
            }
            Assert.IsFalse(test.SwapPlayerSecondTeam(0, true));
        }

        [TestMethod]
        public void PCCCheck_Team1() {
            Match test = new Match(A, B, false);
            bool result = false;
            if ((test.FirstTeam.OfficalPlayers.Count == 11) && (test.FirstTeam.ReservePlayers.Count == 5)) {
                result = true;
            }
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PCCCheck_Team2() {
            Match test = new Match(A, B, false);
            bool result = false;
            if ((test.SecondTeam.OfficalPlayers.Count == 11) && (test.SecondTeam.ReservePlayers.Count == 5)) {
                result = true;
            }
            Assert.IsTrue(result);
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
            Match match = new Match(A, B, false);
            match.firstTeamScore();

            Assert.AreEqual(1, match.FirstTeamGoal());
            Assert.AreEqual(0, match.SecondTeamGoal());
        }

        [TestMethod]
        public void Match_SecondTeamHaveGoal_UpdateScore() {
            Match match = new Match(A, B, false);
            match.secondTeamScore();
            Assert.AreEqual(0, match.FirstTeamGoal());
            Assert.AreEqual(1, match.SecondTeamGoal());
        }

        [TestMethod]
        public void Match_TestYellowCard() {
            Match match = new Match(A, B, false);
            int chance = RandomGenerator.rnd.Next(1, 1);
            match.YellowCardHandler(chance);
            Assert.AreEqual(1, match.Cards.Count);            
        }

        [TestMethod]
        public void Match_TestRedCard() {
            Match match = new Match(A, B, false);
            int chance = RandomGenerator.rnd.Next(1, 1);
            match.RedCardHandler(chance);
            Assert.AreEqual(1, match.Cards.Count);
        }
    }
}
