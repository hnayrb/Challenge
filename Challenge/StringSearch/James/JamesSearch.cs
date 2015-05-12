using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.StringSearch.James
{
    public class James_Search : ISearch
    {
        private StringItem root;

        public void LoadWordSet(IEnumerable<string> unorderedWordSet)
        {
            root = null;
            StringItem y = null;

            foreach (string word in unorderedWordSet)
            {
                root = TreeInsert(root, word);
            }

        }

        private StringItem TreeInsert(StringItem root, string word)
        {
            if (root == null)
            {
                root = new StringItem();
            }

            StringItem y = null;
            StringItem x = root;
            StringItem z = new StringItem();
            z.Key = word;

            while (!string.IsNullOrEmpty(x.Key))
            {
                y = x;
                if (string.Compare(z.Key, x.Key, true) < 0)
                {
                    if (x.Left == null)
                    {
                        x.Left = new StringItem();
                    }
                    x = x.Left;
                }
                else
                {
                    if (x.Right == null)
                    {
                        x.Right = new StringItem();
                    }
                    x = x.Right;
                }
            }

            if (y == null)
            {
                root.Key = z.Key;
            }
            else if (string.Compare(z.Key, y.Key, true) < 0)
            {
                y.Left = z;
            }
            else if (string.Compare(z.Key, y.Key, true) != 0)
            {
                y.Right = z;
            }

            return root;
        }

        public IEnumerable<string> GetOrderedWordSet()
        {
            List<string> orderedList = new List<string>();
            InorderTreeWalk(orderedList, root);
            return orderedList;
        }

        private void InorderTreeWalk(List<string> orderedList, StringItem x)
        {
            if (x != null)
            {
                InorderTreeWalk(orderedList, x.Left);
                orderedList.Add(x.Key);
                InorderTreeWalk(orderedList, x.Right);
            }
        }

        public bool WordExistsInSet(string wordToFind)
        {
            return TreeSearch(root, wordToFind);
        }

        public bool TreeSearch(StringItem root, string word)
        {
            if (root == null)
            {
                return false;
            }
            else if (string.Compare(root.Key, word, true) == 0)
            {
                return true;
            }
            else if (string.Compare(root.Key, word, true) < 0)
            {
                return TreeSearch(root.Left, word);
            }
            else
            {
                return TreeSearch(root.Right, word);
            }
        }
    }

    public class StringItem
    {
        public string Key;
        public StringItem Parent;
        public StringItem Left;
        public StringItem Right;
    }
}
