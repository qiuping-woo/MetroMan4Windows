using System;
using System.Collections.Generic;

namespace MMEngine
{
    public class FixRBTree<ObjType> : IDisposable
    {
        #region Properties

        private int _maxCount = -1;
        private RBTree<ObjType> _rbTree = null;

        #endregion

        #region Public Methods

        public FixRBTree(Func<ObjType, ObjType, int> comparator, int maxCount)
        {
            this._rbTree = new RBTree<ObjType>(comparator);
            this._maxCount = maxCount;
        }

        public void Dispose()
        {
            _rbTree.Dispose();
            _rbTree = null;
        }

        public bool InsertUnique(ObjType data)
        {
            bool result = _rbTree.InsertUnique(data);
            if (result == true && _rbTree.Count > _maxCount)
            {
                if (_rbTree.Compare(data, _rbTree.GetMax()) == 0)
                {
                    result = false;
                }
                _rbTree.RemoveMax();
            }
            return result;
        }

        public bool InsertUnion(ObjType data)
        {
            bool result = _rbTree.InsertUnion(data);
            if (result == true && _rbTree.Count > _maxCount)
            {
                if (_rbTree.Compare(data, _rbTree.GetMax()) > 0)
                {
                    result = false;
                }
                _rbTree.RemoveMax();
            }
            return result;
        }

        public int Count
        {
            get { return _rbTree.Count; }
        }

        public int MaxCount
        {
            get { return _maxCount; }
        }

        public ObjType Remove(ObjType key)
        {
            return _rbTree.Remove(key);
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

        public ObjType Find(ObjType data)
        {
            return _rbTree.Find(data);
        }

        public List<ObjType> GetInOrderList()
        {
            return _rbTree.GetInOrderList();
        }

        #endregion
    }
}
