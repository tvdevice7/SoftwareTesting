using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test {
    class DataProvider {
        private DataProvider() { }
        private string connectionStr = @"";
        private static DataProvider instance;

        public static DataProvider Instance {
            get {
                if (instance == null) instance = new DataProvider();
                return instance;
            }
            private set { instance = value; }
        }
    }
}
