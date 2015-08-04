using System;
using System.Collections.Generic;

namespace MMEngine
{
    public class RBMap<ObjType> : IDisposable
    {
        #region Properties

        private RBTree<ObjType> _rbTree = null;
        private Func<ObjType, ObjType, int> _keyComparator = null;
        private Func<ObjType, ObjType, int> _valueComparator = null;

        #endregion

        #region Public Methods

        public RBMap(Func<ObjType, ObjType, int> keyComparator, Func<ObjType, ObjType, int> valueComparator)
        {
            this._rbTree = new RBTree<ObjType>(keyComparator);
            this._keyComparator = keyComparator;
            this._valueComparator = valueComparator;
        }

        public void Dispose()
        {
            _rbTree.Dispose();
            _rbTree = null;
        }

        public int InsertUnique(ObjType data)
        {
            int result = 0; // -1:Failure 0:Insert 1:Update
            ObjType tData = _rbTree.Find(data);
            if (tData != null)
            {
                if (this._valueComparator(tData, data) <= 0)
                {
                    result = -1;
                }
                else
                {
                    result = 1;
                    this._rbTree.Remove(tData);
                    this._rbTree.InsertUnique(data);
                }
            }
            else
            {
                _rbTree.InsertUnique(data);
            }
            return result;
        }

        public int Count
        {
            get { return _rbTree.Count; }
        }

        public List<ObjType> GetInOrderList()
        {
            return _rbTree.GetInOrderList();
        }

        public ObjType RemoveMax()
        {
            return _rbTree.RemoveMax();
        }

        public ObjType RemoveMin()
        {
            return _rbTree.RemoveMin();
        }

        public ObjType GetMax()
        {
            return _rbTree.GetMax();
        }

        public ObjType GetMin()
        {
            return _rbTree.GetMin();
        }

        #endregion
    }
}
