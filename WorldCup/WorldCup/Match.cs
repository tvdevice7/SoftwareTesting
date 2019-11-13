using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    public enum Result {
        FIRST_TEAM_WIN, SECOND_TEAM_WIN, DRAW
    }
    public enum MatchException {
        NONE, NOT_ENOUGH_PLAYERS
    }
    class Match {
        struct PlayerCardCount {
            Player player;
            int yellowCard;
            int redCard;

            public PlayerCardCount(Player p) {
                this.player = p;
                yellowCard = 0;
                redCard = 0;
            }

            public Player GetPlayer() {
                return player;
            }

            public void AddYellowCard() {
                yellowCard++;
            }

            public void AddRedCard() {
                redCard++;
            }

            public int GetYellowCard() {
                return yellowCard;
            }

            public int GetRedCard() {
                return redCard;
            }
        };
        const int goalChance = 5;
        const int yellowCardChance = 3;
        const int redCardChance = 1;
        const int injuryChance = 2;

        int id;
        int time;
        Team firstTeam;
        Team secondTeam;
        List<Card> cards;
        List<Goal> goals;
        List<PlayerCardCount> firstTeamOfficial;
        List<PlayerCardCount> firstTeamReserve;
        List<PlayerCardCount> secondTeamOfficial;
        List<PlayerCardCount> secondTeamReserve;
        Result result;
        MatchException exception;
        List<Match> extraMatch;
        bool isExtraMatch = false;


        public Match(Team firstTeam, Team secondTeam, bool isKnockOut) {
            this.firstTeam = firstTeam;
            this.secondTeam = secondTeam;
            cards = new List<Card>();
            goals = new List<Goal>();
            firstTeam.OfficalPlayers = new List<Player>();
            secondTeam.OfficalPlayers = new List<Player>();
            firstTeam.ReservePlayers = new List<Player>();
            secondTeam.ReservePlayers = new List<Player>();
            chooseOfficialSquad(firstTeam);
            chooseOfficialSquad(secondTeam);

            firstTeamOfficial = new List<PlayerCardCount>();
            foreach (Player p in firstTeam.OfficalPlayers) {
                firstTeamOfficial.Add(new PlayerCardCount(p));
            }
            secondTeamOfficial = new List<PlayerCardCount>();
            foreach (Player p in secondTeam.OfficalPlayers) {
                secondTeamOfficial.Add(new PlayerCardCount(p));
            }
            firstTeamReserve = new List<PlayerCardCount>();
            foreach (Player p in firstTeam.ReservePlayers) {
                firstTeamReserve.Add(new PlayerCardCount(p));
            }
            secondTeamReserve = new List<PlayerCardCount>();
            foreach (Player p in secondTeam.ReservePlayers) {
                secondTeamReserve.Add(new PlayerCardCount(p));
            }
            Compete();
        }

        public void chooseOfficialSquad(Team team) {
            for (int i = 0; i < 11; i++) {
                team.OfficalPlayers.Add(firstTeam.Players[i]);
            }
            for (int i = 11; i < 16; i++) {
                team.ReservePlayers.Add(firstTeam.Players[i]);
            }
        }

        public void Compete() {
            Random rnd = new Random();
            int gameTime;
            if (isExtraMatch) {
                gameTime = 15;
            }
            else {
                gameTime = 90;
            }
            for (int i = 0; i < gameTime; i++) {
                int chance = rnd.Next(100);
                if (chance <= goalChance) {
                    Goal newGoal = new Goal();
                    chance = rnd.Next(100);
                    if (chance < 50) {
                        chance = rnd.Next(firstTeamOfficial.Count() - 1);
                        newGoal.Scorer = firstTeamOfficial[chance].GetPlayer();
                        newGoal.Match = this;
                    }
                    else {
                        chance = rnd.Next(secondTeamOfficial.Count() - 1);
                        newGoal.Scorer = secondTeamOfficial[chance].GetPlayer();
                        newGoal.Match = this;
                    }
                    goals.Add(newGoal);
                }
                chance = rnd.Next(100);
                if (chance <= yellowCardChance) {
                    bool canContinue = true;
                    Card newCard = new Card(); // Cần phân loại red vs yellow card
                    chance = rnd.Next(100);
                    if (chance > 50) {
                        chance = rnd.Next(firstTeamOfficial.Count() - 1);
                        newCard.Player = firstTeamOfficial[chance].GetPlayer();
                        firstTeamOfficial[chance].AddYellowCard();
                        newCard.Match = this;
                        newCard.IsRedCard = false;
                        canContinue = SwapPlayerFirst(chance, false);
                    }
                    else {
                        chance = rnd.Next(secondTeamOfficial.Count() - 1);
                        newCard.Player = secondTeamOfficial[chance].GetPlayer();
                        secondTeamOfficial[chance].AddYellowCard();
                        newCard.Match = this;
                        newCard.IsRedCard = false;
                        canContinue = SwapPlayerSecond(chance, false);
                    }
                    cards.Add(newCard);
                    if (!canContinue) {
                        exception = MatchException.NOT_ENOUGH_PLAYERS;
                    }
                    //Kiểm tra player đã có 2 card vàng chưa, nếu có thì phải thay đổi player
                }
                chance = rnd.Next(100);
                if (chance <= redCardChance) {
                    bool canContinue = true;
                    Card newCard = new Card(); // Cần phân loại red vs yellow card
                    chance = rnd.Next(100);
                    if (chance > 50) {
                        chance = rnd.Next(firstTeamOfficial.Count() - 1);
                        newCard.Player = firstTeamOfficial[chance].GetPlayer();
                        firstTeamOfficial[chance].AddYellowCard();
                        newCard.Match = this;
                        newCard.IsRedCard = true;
                        canContinue = SwapPlayerFirst(chance, false);
                    }
                    else {
                        chance = rnd.Next(secondTeamOfficial.Count() - 1);
                        newCard.Player = secondTeamOfficial[chance].GetPlayer();
                        secondTeamOfficial[chance].AddYellowCard();
                        newCard.Match = this;
                        newCard.IsRedCard = true;
                        canContinue = SwapPlayerSecond(chance, false);
                    }
                    cards.Add(newCard);
                    if (!canContinue) {
                        exception = MatchException.NOT_ENOUGH_PLAYERS;
                    }
                }
                chance = rnd.Next(100);
                if (chance <= injuryChance) {
                    bool canContinue = true;
                    chance = rnd.Next(100);
                    if (chance > 50) {
                        chance = rnd.Next(firstTeamOfficial.Count() - 1);
                        canContinue = SwapPlayerFirst(chance, true);
                    }
                    else {
                        chance = rnd.Next(secondTeamOfficial.Count() - 1);
                        canContinue = SwapPlayerSecond(chance, true);
                    }
                    if (!canContinue) {
                        exception = MatchException.NOT_ENOUGH_PLAYERS;
                    }
                }
            }
            if ((FirstTeamGoal() == SecondTeamGoal()) && (!isExtraMatch)) {
                Match extraMatch1st = new Match(firstTeam, secondTeam, false);
                extraMatch.Add(extraMatch1st);
                extraMatch1st.isExtraMatch = true;
                extraMatch1st.Compete();
                if (extraMatch1st.result == Result.DRAW) {
                    Match extraMatch2nd = new Match(firstTeam, secondTeam, false);
                    extraMatch.Add(extraMatch2nd);
                    extraMatch2nd.isExtraMatch = true;
                    extraMatch2nd.Compete();
                    result = extraMatch2nd.result;
                    return;
                }
                result = extraMatch1st.result;
                return;
            }
            if (FirstTeamGoal() > SecondTeamGoal()) {
                result = Result.FIRST_TEAM_WIN;
            }
            else if (FirstTeamGoal() < SecondTeamGoal()) {
                result = Result.SECOND_TEAM_WIN;
            }
            else {
                result = Result.DRAW;
            }
            exception = MatchException.NONE;
        }

        public Result Result {
            get { return result; }
        }
        public Team Winner {
            get {
                if (result == Result.FIRST_TEAM_WIN) return firstTeam;
                if (result == Result.SECOND_TEAM_WIN) return secondTeam;
                return null;
            }
        }
        public List<Goal> Goals {
            get { return goals; }
        }
        public int FirstTeamGoal() {
            int firstTeamGoal = 0;
            foreach (Goal g in goals) {
                if (firstTeam.Players.Contains(g.Scorer)) {
                    firstTeamGoal++;
                }
            }
            return firstTeamGoal;
        }
        public int FirstTeamCard() {
            int firstTeamCard = 0;
            foreach (Card c in cards) {
                if (firstTeam.Players.Contains(c.Player)) {
                    firstTeamCard++;
                }
            }
            return firstTeamCard;
        }
        public int SecondTeamCard() {
            int secondTeamCard = 0;
            foreach (Card c in cards) {
                if (secondTeam.Players.Contains(c.Player)) {
                    secondTeamCard++;
                }
            }
            return secondTeamCard;
        }
        public int SecondTeamGoal() {
            int secondTeamGoal = 0;
            foreach (Goal g in goals) {
                if (secondTeam.Players.Contains(g.Scorer)) {
                    secondTeamGoal++;
                }
            }
            return secondTeamGoal;
        }

        private bool SwapPlayerFirst(int id, bool isInjury) {
            PlayerCardCount temp = firstTeamOfficial[id];
            if (((temp.GetYellowCard() < 2) || (temp.GetRedCard() < 1)) && (!isInjury)) {
                return true;
            }
            firstTeamOfficial.Remove(temp);
            PlayerCardCount reserve;
            foreach (PlayerCardCount p in firstTeamReserve) {
                if ((p.GetYellowCard() < 2) || (p.GetRedCard() < 1)) {
                    reserve = p;
                    firstTeamOfficial.Add(reserve);
                    firstTeamReserve.Remove(reserve);
                    return true;
                }
            }
            if (firstTeamOfficial.Count() >= 7) {
                return true;
            }
            result = Result.SECOND_TEAM_WIN;
            return false;
        }

        private bool SwapPlayerSecond(int id, bool isInjury) {
            PlayerCardCount temp = secondTeamOfficial[id];
            if (((temp.GetYellowCard() < 2) || (temp.GetRedCard() < 1)) && (!isInjury)) {
                return true;
            }
            secondTeamOfficial.Remove(temp);
            PlayerCardCount reserve;
            foreach (PlayerCardCount p in secondTeamReserve) {
                if ((p.GetYellowCard() < 2) || (p.GetRedCard() < 1)) {
                    reserve = p;
                    secondTeamOfficial.Add(reserve);
                    secondTeamReserve.Remove(reserve);
                    return true;
                }
            }
            if (secondTeamOfficial.Count > 7) {
                return true;
            }
            result = Result.FIRST_TEAM_WIN;
            return false;
        }
    }
}
