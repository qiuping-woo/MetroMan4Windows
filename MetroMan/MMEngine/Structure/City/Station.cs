using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMEngine
{
    public class Station
    {
        public int ID;
        public string UNO;
        public int MultiLine;
        public int MultiWay;

        public Station()
        {
        }

        public Station(int id, string uno, int multiLine, int multiWay)
        {
            this.ID = id;
            this.UNO = uno;
            this.MultiLine = multiLine;
            this.MultiWay = multiWay;
        }

        public Station(Station obj)
        {
            this.ID = obj.ID;
            this.UNO = obj.UNO;
            this.MultiLine = obj.MultiLine;
            this.MultiWay = obj.MultiWay;
        }
    }
}
