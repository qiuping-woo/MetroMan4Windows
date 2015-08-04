namespace MMEngine
{
    public class Entry<ObjType>
    {
        public Entry<ObjType> LeftChild;
        public Entry<ObjType> RightChild;
        public Entry<ObjType> Parent;
        public ObjType Data;
        public byte Color;

        public Entry(ObjType data, Entry<ObjType> parent)
        {
            this.Data = data;
            this.Parent = parent;
        }

        public bool Equals(Entry<ObjType> other)
        {
            return (this.Color == other.Color) && (this.Data.Equals(other.Data));
        }
    }
}
