using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class DataProvider {
        private DataProvider() {
        }        
        
        private string connectionStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=./../Database2.accdb";
        private static DataProvider instance;

        public static DataProvider Instance {
            get {
                if (instance == null) instance = new DataProvider();
                return instance;
            }
            private set { instance = value; }
        }

        public DataTable ExecuteQuery(string q, object[] parameter = null) {
            DataTable data = new DataTable();
            using (OleDbConnection oleConnect = new OleDbConnection(connectionStr)) {
                oleConnect.Open();
                OleDbCommand command = new OleDbCommand(q, oleConnect);
                AddParameter(command, q, parameter);
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                adapter.Fill(data);
                oleConnect.Close();
            };
            return data;
        }

        private void AddParameter(OleDbCommand command, string q, object[] parameter) {
            if (parameter != null) {
                string[] listPara = q.Split(' ');
                int i = 0;
                foreach (string item in listPara) {
                    if (item.Contains('@')) {
                        command.Parameters.AddWithValue(item, parameter[i]);
                        i++;
                    };
                };
            };
        }
    }
}
