using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    class AssistantCoach : Staff {
        public AssistantCoach(DataRow data) {
            this.ID = (int)data["ID"];
            this.Name = data["AssistantCoach"].ToString();
        }
    }
}
