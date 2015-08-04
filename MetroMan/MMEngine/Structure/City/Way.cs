using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMEngine
{
    public class Way
    {
        public int ID;
        public string UNO;
        public int LineID;
        public int WaitTime;
        public List<int> StationIDList = new List<int>();

        public Way()
        {
        }

        public Way(int id, string uno, int lineID, int waitTime, List<int> stationIDList)
        {
            this.ID = id;
            this.UNO = uno;
            this.LineID = lineID;
            this.WaitTime = waitTime;
            this.StationIDList.AddRange(stationIDList);
        }

        public Way(Way obj)
        {
            this.ID = obj.ID;
            this.UNO = obj.UNO;
            this.LineID = obj.LineID;
            this.WaitTime = obj.WaitTime;
            this.StationIDList.AddRange(obj.StationIDList);
        }
    }
}
