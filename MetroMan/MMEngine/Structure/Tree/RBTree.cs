using System;
using System.Collections.Generic;

namespace MMEngine
{
    public class RBTree<ObjType> : IDisposable
    {
        #region Properties

        private const byte BLACK = 0;
        private const byte RED = 1;

        private Entry<ObjType> _root = null;
        private int _size = 0;
        private Func<ObjType, ObjType, int> _compareFunc = null;
        private List<ObjType> _inOrderList = new List<ObjType>();

        #endregion

        #region Public Methods

        public RBTree(Func<ObjType, ObjType, int> comparator)
        {
            _compareFunc = comparator;
        }

        public void Dispose()
        {
            PreorderFree(this._root);
            _size = 0;
            GC.SuppressFinalize(this);
        }

        public int Count
        {
            get { return _size; }
        }

        public bool InsertUnique(ObjType data)
        {
            Entry<ObjType> t = _root;
            if (t == null)
            {
                _root = new Entry<ObjType>(data, null);
                _size++;
                return true;
            }
            while (true)
            {
                int cmp = Compare(data, t.Data);
                if (cmp == 0) return false;
                else if (cmp < 0)
                {
                    if (t.LeftChild != null) t = t.LeftChild;
                    else
                    {
                        t.LeftChild = new Entry<ObjType>(data, t);
                        FixAfterInsertion(t.LeftChild);
                        _size++;
                        return true;
                    }
                }
                else if (cmp > 0)
                {
                    if (t.RightChild != null) t = t.RightChild;
                    else
                    {
                        t.RightChild = new Entry<ObjType>(data, t);
                        FixAfterInsertion(t.RightChild);
                        _size++;
                        return true;
                    }
                }
            }
        }

        public bool InsertUnion(ObjType data)
        {
            Entry<ObjType> t = _root;
            if (t == null)
            {
                _root = new Entry<ObjType>(data, null);
                _size++;
                return true;
            }
            while (true)
            {
                int cmp = Compare(data, t.Data);
                if (cmp <= 0)
                {
                    if (t.LeftChild != null) t = t.LeftChild;
                    else
                    {
                        t.LeftChild = new Entry<ObjType>(data, t);
                        FixAfterInsertion(t.LeftChild);
                        _size++;
                        return true;
                    }
                }
                else
                {
                    if (t.RightChild != null) t = t.RightChild;
                    else
                    {
                        t.RightChild = new Entry<ObjType>(data, t);
                        FixAfterInsertion(t.RightChild);
                        _size++;
                        return true;
                    }
                }
            }
        }

        public ObjType GetMax()
        {
            return ((this.GetMaxEntry() != null) ? this.GetMaxEntry().Data : default(ObjType));
        }

        public ObjType GetMin()
        {
            return ((this.GetMinEntry() != null) ? this.GetMinEntry().Data : default(ObjType));
        }

        public ObjType Remove(ObjType key)
        {
            Entry<ObjType> p = FindEntry(key);
            if (p == null) return default(ObjType);
            ObjType oldValue = p.Data;
            DeleteEntry(p);
            _size--;
            return oldValue;
        }

        public ObjType RemoveMax()
        {
            Entry<ObjType> max = this.GetMaxEntry();
            if (max == null) return default(ObjType);
            ObjType oldValue = max.Data;
            DeleteEntry(max);
            _size--;
            return oldValue;
        }

        public ObjType RemoveMin()
        {
            Entry<ObjType> min = this.GetMinEntry();
            if (min == null) return default(ObjType);
            ObjType oldValue = min.Data;
            DeleteEntry(min);
            _size--;
            return oldValue;
        }

        public int Compare(ObjType k1, ObjType k2)
        {
            return _compareFunc(k1, k2);
        }

        public bool Contains(ObjType data)
        {
            return (this.FindEntry(data) != null);
        }

        public ObjType Find(ObjType data)
        {
            Entry<ObjType> entry = this.FindEntry(data);
            if (entry == null)
            {
                return default(ObjType);
            }
            else
            {
                return entry.Data;
            }
        }

        public List<ObjType> GetInOrderList()
        {
            this._inOrderList = new List<ObjType>();
            TraverseInOrder(this._root);
            return this._inOrderList;
        }

        #endregion

        #region Private Methods

        private Entry<ObjType> GetMaxEntry()
        {
            Entry<ObjType> p = _root;
            if (p != null)
            {
                while (p != null)
                {
                    if (p.RightChild != null) p = p.RightChild;
                    else break;
                }
            }
            return p;
        }

        private Entry<ObjType> GetMinEntry()
        {
            Entry<ObjType> p = _root;
            if (p != null)
            {
                while (p != null)
                {
                    if (p.LeftChild != null) p = p.LeftChild;
                    else break;
                }
            }
            return p;
        }

        private Entry<ObjType> FindEntry(ObjType key)
        {
            Entry<ObjType> p = _root;
            while (p != null)
            {
                int cmp = Compare(key, p.Data);
                if (cmp == 0) return p;
                else if (cmp < 0) p = p.LeftChild;
                else p = p.RightChild;
            }
            return null;
        }

        private void DeleteEntry(Entry<ObjType> target)
        {
            if (target.LeftChild != null && target.RightChild != null)
            {
                Entry<ObjType> s = Successor(target);
                target.Data = s.Data;
                target = s;
            }
            Entry<ObjType> replacement = (target.LeftChild != null ? target.LeftChild : target.RightChild);
            if (replacement != null)
            {
                replacement.Parent = target.Parent;
                if (target.Parent == null) _root = replacement;
                else if (target == target.Parent.LeftChild) target.Parent.LeftChild = replacement;
                else target.Parent.RightChild = replacement;
                target.LeftChild = target.RightChild = target.Parent = null;
                if (target.Color == BLACK) FixAfterDeletion(replacement);
            }
            else if (target.Parent == null) _root = null;
            else
            {
                if (target.Color == BLACK) FixAfterDeletion(target);
                if (target.Parent != null)
                {
                    if (target == target.Parent.LeftChild) target.Parent.LeftChild = null;
                    else if (target == target.Parent.RightChild) target.Parent.RightChild = null;
                    target.Parent = null;
                }
            }
        }

        private Entry<ObjType> Successor(Entry<ObjType> target)
        {
            if (target == null) return null;
            else if (target.RightChild != null)
            {
                Entry<ObjType> tmp = target.RightChild;
                while (tmp.LeftChild != null) tmp = tmp.LeftChild;
                return tmp;
            }
            else
            {
                Entry<ObjType> tmp = target.Parent;
                Entry<ObjType> ch = target;
                while (tmp != null && ch == tmp.RightChild)
                {
                    ch = tmp;
                    tmp = tmp.Parent;
                }
                return tmp;
            }
        }

        private void PreorderFree(Entry<ObjType> start)
        {
            if (start != null)
            {
                PreorderFree(start.LeftChild);
                PreorderFree(start.RightChild);
                start = null;
            }
        }

        private Entry<ObjType> LeftOf(Entry<ObjType> target)
        {
            return (target == null) ? null : target.LeftChild;
        }

        private Entry<ObjType> RightOf(Entry<ObjType> target)
        {
            return (target == null) ? null : target.RightChild;
        }

        private Entry<ObjType> ParentOf(Entry<ObjType> target)
        {
            return (target == null) ? null : target.Parent;
        }

        private byte ColorOf(Entry<ObjType> target)
        {
            return (target == null) ? BLACK : target.Color;
        }

        private void SetColor(Entry<ObjType> p, byte c)
        {
            if (p != null) p.Color = c;
        }

        private void RotateLeft(Entry<ObjType> target)
        {
            Entry<ObjType> right = target.RightChild;
            target.RightChild = right.LeftChild;
            if (right.LeftChild != null) right.LeftChild.Parent = target;
            right.Parent = target.Parent;
            if (target.Parent == null) _root = right;
            else if (target.Parent.LeftChild == target) target.Parent.LeftChild = right;
            else target.Parent.RightChild = right;
            right.LeftChild = target;
            target.Parent = right;
        }

        private void RotateRight(Entry<ObjType> target)
        {
            Entry<ObjType> left = target.LeftChild;
            target.LeftChild = left.RightChild;
            if (left.RightChild != null) left.RightChild.Parent = target;
            left.Parent = target.Parent;
            if (target.Parent == null) _root = left;
            else if (target.Parent.RightChild == target) target.Parent.RightChild = left;
            else target.Parent.LeftChild = left;
            left.RightChild = target;
            target.Parent = left;
        }

        private void FixAfterInsertion(Entry<ObjType> x)
        {
            x.Color = RED;
            while (x != null && x != _root && x.Parent.Color == RED)
            {
                if (ParentOf(x) == LeftOf(ParentOf(ParentOf(x))))
                {
                    Entry<ObjType> y = RightOf(ParentOf(ParentOf(x)));
                    if (ColorOf(y) == RED)
                    {
                        SetColor(ParentOf(x), BLACK);
                        SetColor(y, BLACK);
                        SetColor(ParentOf(ParentOf(x)), RED);
                        x = ParentOf(ParentOf(x));
                    }
                    else
                    {
                        if (x == RightOf(ParentOf(x)))
                        {
                            x = ParentOf(x);
                            RotateLeft(x);
                        }
                        SetColor(ParentOf(x), BLACK);
                        SetColor(ParentOf(ParentOf(x)), RED);
                        if (ParentOf(ParentOf(x)) != null) RotateRight(ParentOf(ParentOf(x)));
                    }
                }
                else
                {
                    Entry<ObjType> y = LeftOf(ParentOf(ParentOf(x)));
                    if (ColorOf(y) == RED)
                    {
                        SetColor(ParentOf(x), BLACK);
                        SetColor(y, BLACK);
                        SetColor(ParentOf(ParentOf(x)), RED);
                        x = ParentOf(ParentOf(x));
                    }
                    else
                    {
                        if (x == LeftOf(ParentOf(x)))
                        {
                            x = ParentOf(x);
                            RotateRight(x);
                        }
                        SetColor(ParentOf(x), BLACK);
                        SetColor(ParentOf(ParentOf(x)), RED);
                        if (ParentOf(ParentOf(x)) != null) RotateLeft(ParentOf(ParentOf(x)));
                    }
                }
            }
            _root.Color = BLACK;
        }

        private void FixAfterDeletion(Entry<ObjType> target)
        {
            while (target != _root && target.Color == BLACK)
            {
                if (target == LeftOf(ParentOf(target)))
                {
                    Entry<ObjType> sib = RightOf(ParentOf(target));
                    if (ColorOf(sib) == RED)
                    {
                        SetColor(sib, BLACK);
                        SetColor(ParentOf(target), RED);
                        RotateLeft(ParentOf(target));
                        sib = RightOf(ParentOf(target));
                    }
                    if (ColorOf(LeftOf(sib)) == BLACK && ColorOf(RightOf(sib)) == BLACK)
                    {
                        SetColor(sib, RED);
                        target = ParentOf(target);
                    }
                    else
                    {
                        if (ColorOf(RightOf(sib)) == BLACK)
                        {
                            SetColor(LeftOf(sib), BLACK);
                            SetColor(sib, RED);
                            RotateRight(sib);
                            sib = RightOf(ParentOf(target));
                        }
                        SetColor(sib, ColorOf(ParentOf(target)));
                        SetColor(ParentOf(target), BLACK);
                        SetColor(RightOf(sib), BLACK);
                        RotateLeft(ParentOf(target));
                        target = _root;
                    }
                }
                else
                {
                    Entry<ObjType> sib = LeftOf(ParentOf(target));
                    if (ColorOf(sib) == RED)
                    {
                        SetColor(sib, BLACK);
                        SetColor(ParentOf(target), RED);
                        RotateRight(ParentOf(target));
                        sib = LeftOf(ParentOf(target));
                    }
                    if (ColorOf(RightOf(sib)) == BLACK && ColorOf(LeftOf(sib)) == BLACK)
                    {
                        SetColor(sib, RED);
                        target = ParentOf(target);
                    }
                    else
                    {
                        if (ColorOf(LeftOf(sib)) == BLACK)
                        {
                            SetColor(RightOf(sib), BLACK);
                            SetColor(sib, RED);
                            RotateLeft(sib);
                            sib = LeftOf(ParentOf(target));
                        }
                        SetColor(sib, ColorOf(ParentOf(target)));
                        SetColor(ParentOf(target), BLACK);
                        SetColor(LeftOf(sib), BLACK);
                        RotateRight(ParentOf(target));
                        target = _root;
                    }
                }
            }
            SetColor(target, BLACK);
        }

        private void TraverseInOrder(Entry<ObjType> start)
        {
            if (start != null)
            {
                TraverseInOrder(start.LeftChild);
                this._inOrderList.Add(start.Data);
                TraverseInOrder(start.RightChild);
            }
        }

        #endregion
    }
}
