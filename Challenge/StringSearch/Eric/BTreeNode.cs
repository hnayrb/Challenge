using System;
using System.Collections;
using System.Collections.Generic;

namespace Challenge.StringSearch.Eric
{
    public class BTreeNode<T> : IComparable<BTreeNode<T>>, IEnumerable<T> where T : IComparable
    {
        public T Value { get; private set; }
        public BTreeNode<T> LeftNode;
        public BTreeNode<T> RightNode;

        private IComparer<T> _comparer;

        public BTreeNode(T value)
        {
            Value = value;
        }

        public BTreeNode(T value, IComparer<T> comparer)
            : this(value)
        {
            _comparer = comparer;
        }

        #region IComparable

        public int CompareTo(BTreeNode<T> other)
        {
            if (_comparer != null)
                return _comparer.Compare(Value, other.Value);

            return Value.CompareTo(other.Value);
        }

        #endregion

        #region IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            if (LeftNode != null)
                foreach (var leftNode in LeftNode)
                    yield return leftNode;

            yield return Value;

            if (RightNode != null)
                foreach (var rightNode in RightNode)
                    yield return rightNode;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        #endregion
    }
}
