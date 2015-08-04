using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMEngine
{
    public class Timetable
    {
        public int ID;
        public int LinkID;
        public int DepTime;
        public int ArvTime;

        public Timetable()
        {
        }

        public Timetable(int id, int depTime, int arvTime)
        {
            this.ID = id;
            this.DepTime = depTime;
            this.ArvTime = arvTime;
        }

        public Timetable(int id, int linkID, int depTime, int arvTime)
        {
            this.ID = id;
            this.LinkID = linkID;
            this.DepTime = depTime;
            this.ArvTime = arvTime;
        }

        public Timetable(Timetable obj)
        {
            this.ID = obj.ID;
            this.DepTime = obj.DepTime;
            this.ArvTime = obj.ArvTime;
        }
    }
}
