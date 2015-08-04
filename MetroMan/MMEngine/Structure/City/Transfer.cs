using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMEngine
{
    public class Transfer
    {
        public int ID;
        public int FromLinkID;
        public int ToLinkID;
        public int TransTime;
        public int TransDistance;
        public int TransType;

        public Transfer()
        {
        }

        public Transfer(int id, int fromLinkID, int toLinkID, int transTime, int transDistance, int transType)
        {
            this.ID = id;
            this.FromLinkID = fromLinkID;
            this.ToLinkID = toLinkID;
            this.TransTime = transTime;
            this.TransDistance = transDistance;
            this.TransType = transType;
        }

        public Transfer(Transfer obj)
        {
            this.ID = obj.ID;
            this.FromLinkID = obj.FromLinkID;
            this.ToLinkID = obj.ToLinkID;
            this.TransTime = obj.TransTime;
            this.TransDistance = obj.TransDistance;
            this.TransType = obj.TransType;
        }
    }
}
