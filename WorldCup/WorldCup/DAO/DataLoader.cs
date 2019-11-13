using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class DataLoader {
        private DataLoader() { }
        private static DataLoader instance;

        public static DataLoader Instance {
            get {
                if (instance == null) instance = new DataLoader();
                return instance;
            }
            private set { instance = value; }
        }

        public List<Region> LoadRegions() {
            string query = "SELECT * FROM KhuVuc";
            DataTable data = new DataTable();
            data = DataProvider.Instance.ExecuteQuery(query);

            List<Region> regions = new List<Region>();
            foreach (DataRow row in data.Rows) {
                Region r = new Region(row);
                List<Team> teams = LoadTeams(r.ID);
                r.Teams = teams;
                regions.Add(r);
            }
            return regions;
        } 

        public List<Team> LoadTeams(int idRegion) {
            string query = "SELECT * FROM Doi WHERE RegionID=" + idRegion.ToString();
            DataTable data = new DataTable();
            data = DataProvider.Instance.ExecuteQuery(query);

            List<Team> teams = new List<Team>();
            foreach (DataRow row in data.Rows) {
                Team t = new Team(row);
                List<Player> players = LoadPlayer(t.ID);
                t.Players = players;
                foreach (Player p in players) p.Team = t;
                Coach coach = loadCoach(t.ID);
                t.Coach = coach;
                Doctor doctor = loadDoctor(t.ID);
                t.Doctor = doctor;
                List<AssistantCoach> assistantCoachs = loadAssistantCoach(t.ID);
                t.AssistantCoach = assistantCoachs;
                teams.Add(t);
            }
            return teams;
        }

        public List<Player> LoadPlayer(int idTeam) {
            string query = "SELECT * FROM CauThu WHERE Team=" + idTeam.ToString();
            DataTable data = new DataTable();
            data = DataProvider.Instance.ExecuteQuery(query);

            List<Player> players = new List<Player>();
            foreach (DataRow row in data.Rows) {
                Player p = new Player(row);
                players.Add(p);
            }
            return players;
        }

        public Coach loadCoach(int idTeam) {
            string query = "SELECT * FROM HuanLuyenVien WHERE TeamID=" + idTeam.ToString();
            DataTable data = new DataTable();
            data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count != 1) throw new Exception("Invalid number of coach");
            Coach coach = new Coach(data.Rows[0]);                        
            return coach;
        }
        public Doctor loadDoctor(int idTeam) {
            string query = "SELECT * FROM SanSocVien WHERE Team=" + idTeam.ToString();
            DataTable data = new DataTable();
            data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count != 1) throw new Exception("Invalid number of doctor");
            Doctor doctor = new Doctor(data.Rows[0]);
            return doctor;
        }
        public List<AssistantCoach> loadAssistantCoach(int idTeam) {
            string query = "SELECT * FROM TroLyHLV WHERE Team=" + idTeam.ToString();
            DataTable data = new DataTable();
            data = DataProvider.Instance.ExecuteQuery(query);
            List<AssistantCoach> assistantCoachs = new List<AssistantCoach>();
            foreach (DataRow row in data.Rows) {
                AssistantCoach a = new AssistantCoach(row);
                assistantCoachs.Add(a);
            }
            return assistantCoachs;
        }
    }
}
