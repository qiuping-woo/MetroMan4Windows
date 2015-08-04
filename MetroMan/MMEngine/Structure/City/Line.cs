using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMEngine
{
    public class Line
    {
        public int ID;
        public string UNO;
        public List<int> StationIDList = new List<int>();

        public Line()
        {
        }

        public Line(int id, string uno)
        {
            this.ID = id;
            this.UNO = uno;
        }

        public Line(Line obj)
        {
            this.ID = obj.ID;
            this.UNO = obj.UNO;
            this.StationIDList.AddRange(obj.StationIDList);
        }
    }
}
