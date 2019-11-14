﻿using System;
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
            public Player player;
            public int yellowCard;
            public int redCard;
            public void addYellowCard() {
                yellowCard++;
            }
            public void addRedCard() {
                redCard++;
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
        bool isKnockOut;
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
            this.isKnockOut = isKnockOut;
            cards = new List<Card>();
            goals = new List<Goal>();
            firstTeam.OfficalPlayers = new List<Player>();
            secondTeam.OfficalPlayers = new List<Player>();
            firstTeam.ReservePlayers = new List<Player>();
            secondTeam.ReservePlayers = new List<Player>();
            extraMatch = new List<Match>();
            chooseOfficialSquad(firstTeam);
            chooseOfficialSquad(secondTeam);

            firstTeamOfficial = initPlayerCardCount(firstTeam.OfficalPlayers);
            secondTeamOfficial = initPlayerCardCount(secondTeam.OfficalPlayers);
            firstTeamReserve = initPlayerCardCount(firstTeam.ReservePlayers);            
            secondTeamReserve = initPlayerCardCount(secondTeam.ReservePlayers);
        }

        public void chooseOfficialSquad(Team team) {
            List<Player> temp = new List<Player>(team.Players);
            for (int i = 0; i < 11; i++) {
                int id = RandomGenerator.rnd.Next(temp.Count);
                Player p = temp[id];
                team.OfficalPlayers.Add(p);
                temp.Remove(p);
            }
            foreach (Player p in temp) {
                team.ReservePlayers.Add(p);
            }
        }

        public void Compete() {
            if (isExtraMatch) time = 15;
            else time = 90;
            for (int i = 0; i < time; i++) {
                int chance = RandomGenerator.rnd.Next(100);
                if (chance <= goalChance) {
                    Goal newGoal = new Goal();
                    chance = RandomGenerator.rnd.Next(100);
                    if (chance < 50) {
                        chance = RandomGenerator.rnd.Next(firstTeamOfficial.Count - 1);
                        newGoal.Scorer = firstTeamOfficial[chance].player;
                        newGoal.Match = this;
                    }
                    else {
                        chance = RandomGenerator.rnd.Next(secondTeamOfficial.Count - 1);
                        newGoal.Scorer = secondTeamOfficial[chance].player;
                        newGoal.Match = this;
                    }
                    goals.Add(newGoal);
                }
                chance = RandomGenerator.rnd.Next(100);
                if (chance <= yellowCardChance) {
                    bool canContinue = true;
                    Card newCard = new Card(); // Cần phân loại red vs yellow card
                    chance = RandomGenerator.rnd.Next(100);
                    if (chance > 50) {
                        chance = RandomGenerator.rnd.Next(firstTeamOfficial.Count - 1);
                        newCard.Player = firstTeamOfficial[chance].player;
                        firstTeamOfficial[chance].addYellowCard();
                        newCard.Match = this;
                        newCard.IsRedCard = false;
                        canContinue = SwapPlayerFirstTeam(chance, false);
                    }
                    else {
                        chance = RandomGenerator.rnd.Next(secondTeamOfficial.Count - 1);
                        newCard.Player = secondTeamOfficial[chance].player;
                        secondTeamOfficial[chance].addYellowCard();
                        newCard.Match = this;
                        newCard.IsRedCard = false;
                        canContinue = SwapPlayerSecondTeam(chance, false);
                    }
                    cards.Add(newCard);
                    if (!canContinue) {
                        exception = MatchException.NOT_ENOUGH_PLAYERS;
                    }
                    //Kiểm tra player đã có 2 card vàng chưa, nếu có thì phải thay đổi player
                }
                chance = RandomGenerator.rnd.Next(100);
                if (chance <= redCardChance) {
                    bool canContinue = true;
                    Card newCard = new Card(); // Cần phân loại red vs yellow card
                    chance = RandomGenerator.rnd.Next(100);
                    if (chance > 50) {
                        chance = RandomGenerator.rnd.Next(firstTeamOfficial.Count - 1);
                        newCard.Player = firstTeamOfficial[chance].player;
                        firstTeamOfficial[chance].addYellowCard();
                        newCard.Match = this;
                        newCard.IsRedCard = true;
                        canContinue = SwapPlayerFirstTeam(chance, false);
                    }
                    else {
                        chance = RandomGenerator.rnd.Next(secondTeamOfficial.Count - 1);
                        newCard.Player = secondTeamOfficial[chance].player;
                        secondTeamOfficial[chance].addRedCard();
                        newCard.Match = this;
                        newCard.IsRedCard = true;
                        canContinue = SwapPlayerSecondTeam(chance, false);
                    }
                    cards.Add(newCard);
                    if (!canContinue) {
                        exception = MatchException.NOT_ENOUGH_PLAYERS;
                    }
                }
                chance = RandomGenerator.rnd.Next(100);
                if (chance <= injuryChance) {
                    bool canContinue = true;
                    chance = RandomGenerator.rnd.Next(100);
                    if (chance > 50) {
                        chance = RandomGenerator.rnd.Next(firstTeamOfficial.Count - 1);
                        canContinue = SwapPlayerFirstTeam(chance, true);
                    }
                    else {
                        chance = RandomGenerator.rnd.Next(secondTeamOfficial.Count - 1);
                        canContinue = SwapPlayerSecondTeam(chance, true);
                    }
                    if (!canContinue) {
                        exception = MatchException.NOT_ENOUGH_PLAYERS;
                    }
                }
            }
            if (FirstTeamGoal() == SecondTeamGoal() && !isExtraMatch && isKnockOut) {
                Match extraMatch1st = new Match(firstTeam, secondTeam, true);
                extraMatch.Add(extraMatch1st);
                extraMatch1st.isExtraMatch = true;
                extraMatch1st.Compete();
                if (extraMatch1st.result == Result.DRAW) {
                    Match extraMatch2nd = new Match(firstTeam, secondTeam, true);
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

        public void printHappening() {
            Console.WriteLine("Tran dau giua doi " + firstTeam.Name + " va doi " + secondTeam.Name);
            Console.WriteLine("Ti so: " + FirstTeamGoal().ToString() + " - " + SecondTeamGoal().ToString());
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
                if (g.Scorer.Team.ID == firstTeam.ID) firstTeamGoal++;
            }
            return firstTeamGoal;
        }
        public int FirstTeamCard() {
            int firstTeamCard = 0;
            foreach (Card c in cards) {
                if (c.Player.Team.ID == firstTeam.ID) firstTeamCard++;
            }
            return firstTeamCard;
        }
        public int SecondTeamGoal() {
            int secondTeamGoal = 0;
            foreach (Goal g in goals) {
                if (g.Scorer.Team.ID == secondTeam.ID) secondTeamGoal++;
            }
            return secondTeamGoal;
        }
        public int SecondTeamCard() {
            int secondTeamCard = 0;
            foreach (Card c in cards) {
                if (c.Player.Team.ID == secondTeam.ID) secondTeamCard++;
            }
            return secondTeamCard;
        }

        bool SwapPlayerFirstTeam(int id, bool isInjury) {
            PlayerCardCount temp = firstTeamOfficial[id];
            if (((temp.yellowCard < 2) || (temp.redCard < 1)) && (!isInjury)) {
                return true;
            }
            firstTeamOfficial.Remove(temp);
            PlayerCardCount reserve;
            foreach (PlayerCardCount p in firstTeamReserve) {
                if ((p.yellowCard < 2) || (p.redCard < 1)) {
                    reserve = p;
                    firstTeamOfficial.Add(reserve);
                    firstTeamReserve.Remove(reserve);
                    return true;
                }
            }
            if (firstTeamOfficial.Count >= 7) {
                return true;
            }
            result = Result.SECOND_TEAM_WIN;
            return false;
        }

        bool SwapPlayerSecondTeam(int id, bool isInjury) {
            PlayerCardCount temp = secondTeamOfficial[id];
            if (((temp.yellowCard < 2) || (temp.redCard < 1)) && (!isInjury)) {
                return true;
            }
            secondTeamOfficial.Remove(temp);
            PlayerCardCount reserve;
            foreach (PlayerCardCount p in secondTeamReserve) {
                if ((p.yellowCard < 2) || (p.redCard < 1)) {
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

        List<PlayerCardCount> initPlayerCardCount(List<Player> players) {
            List<PlayerCardCount> lstPlayerCardCount = new List<PlayerCardCount>();
            foreach (Player p in players) {
                PlayerCardCount plc;
                plc.player = p;
                plc.redCard = 0;
                plc.yellowCard = 0;
                lstPlayerCardCount.Add(plc);
            }
            return lstPlayerCardCount;
        }
    }
}
