using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMEngine
{
    public class Link
    {
        public int ID;
        public int WayID;
        public int FromStationID;
        public int ToStationID;
        public int AverageTime;
        public int Distance;
        public int StartTransferFromID;
        public int EndTransferFromID;
        public int StartTransferToID;
        public int EndTransferToID;
        public int StartWeekdayID;
        public int EndWeekdayID;
        public int StartWeekendID;
        public int EndWeekendID;

        public Link()
        {
        }

        public Link(int id, int wayID, int fromStationID, int toStationID, int averageTime, int distance, int startTransferFromID, int endTransferFromID, int startTransferToID, int endTransferToID, int startWeekdayID, int endWeekdayID, int startWeekendID, int endWeekendID)
        {
            this.ID = id;
            this.WayID = wayID;
            this.FromStationID = fromStationID;
            this.ToStationID = toStationID;
            this.AverageTime = averageTime;
            this.Distance = distance;
            this.StartTransferFromID = startTransferFromID;
            this.EndTransferFromID = endTransferFromID;
            this.StartTransferToID = startTransferToID;
            this.EndTransferToID = endTransferToID;
            this.StartWeekdayID = startWeekdayID;
            this.EndWeekdayID = endWeekdayID;
            this.StartWeekendID = startWeekendID;
            this.EndWeekendID = endWeekendID;
        }

        public Link(Link obj)
        {
            this.ID = obj.ID;
            this.WayID = obj.WayID;
            this.FromStationID = obj.FromStationID;
            this.ToStationID = obj.ToStationID;
            this.AverageTime = obj.AverageTime;
            this.Distance = obj.Distance;
            this.StartTransferFromID = obj.StartTransferFromID;
            this.EndTransferFromID = obj.EndTransferFromID;
            this.StartTransferToID = obj.StartTransferToID;
            this.EndTransferToID = obj.EndTransferToID;
            this.StartWeekdayID = obj.StartWeekdayID;
            this.EndWeekdayID = obj.EndWeekdayID;
            this.StartWeekendID = obj.StartWeekendID;
            this.EndWeekendID = obj.EndWeekendID;
        }
    }
}
