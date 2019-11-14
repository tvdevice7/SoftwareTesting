using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup {
    public class Doctor : Staff {
        public Doctor(DataRow data) {
            this.ID = (int)data["ID"];
            this.Name = data["SanSocVien"].ToString();
        }
    }
}
