using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Challenge.StringSearch.Eric
{
    public class BTree<T> : IEnumerable<T> where T : IComparable
    {
        private BTreeNode<T> _rootNode;
        private IComparer<T> _comparer;

        public BTree() { }

        public BTree(IComparer<T> comparer)
        {
            _comparer = comparer;
        }

        public void Add(T value)
        {
            AddRecursive(ref _rootNode, new BTreeNode<T>(value, _comparer));
        }

        public bool Exists(T value)
        {
            return ExistsRecursive(_rootNode, new BTreeNode<T>(value, _comparer));
        }

        // This method will only add unique nodes
        private void AddRecursive(ref BTreeNode<T> node, BTreeNode<T> valueNode)
        {
            if (node == null)
                node = valueNode;
            else if (valueNode.CompareTo(node) < 0)
            {
                if (node.LeftNode == null)
                    node.LeftNode = valueNode;
                else
                    AddRecursive(ref node.LeftNode, valueNode);
            }
            else if (valueNode.CompareTo(node) > 0)
            {
                if (node.RightNode == null)
                    node.RightNode = valueNode;
                else
                    AddRecursive(ref node.RightNode, valueNode);
            }
        }

        private bool ExistsRecursive(BTreeNode<T> node, BTreeNode<T> valueNode)
        {
            if (node == null)
                return false;

            var compare = valueNode.CompareTo(node);

            if (compare == 0)
                return true;
            else if (compare < 0)
                return ExistsRecursive(node.LeftNode, valueNode);
            else
                return ExistsRecursive(node.RightNode, valueNode);
        }

        #region IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return _rootNode.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        #endregion
    }
}
