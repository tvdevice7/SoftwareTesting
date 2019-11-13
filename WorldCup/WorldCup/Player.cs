using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class Player {
        int id;
        string name;

        public int ID {
            get {return id;}
            set {id = value;}
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }
    }
}
