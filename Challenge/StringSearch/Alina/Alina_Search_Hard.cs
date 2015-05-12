using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.StringSearch.Alina
{
    public class Alina_Search_Hard : ISearch
    {
        private AlinaList wordsList;
        private int maxLength;
        private int listCount;
        private TrieNode trie;

        public Alina_Search_Hard()
        {
            trie = CreateTrie();
        }

        #region Interface implementation

        public void LoadWordSet(IEnumerable<string> unorderedWordSet)
        {
            foreach (var word in unorderedWordSet)
            {
                Add(this.trie, word);
                listCount++;
            }
        }

        public IEnumerable<string> GetOrderedWordSet()
        {
            this.wordsList = new AlinaList(listCount);
            TraverseTree(this.trie, 0, new char[maxLength]);
            this.wordsList.Sort();
            // horrible hack to account for doubles
            string[] trimmedArray = this.wordsList.Trim();
            return trimmedArray;
        }

        public bool WordExistsInSet(string wordToFind)
        {
            if (wordToFind.Length > this.maxLength)
                return false;

            int letterShift = 97;
            TrieNode node = this.trie;
            char[] chars = wordToFind.ToLower().ToCharArray();
            int l = chars.Length;

            // search length(word) nodes correspoding to 
            // potentially each character in the word
            for (int i = 0; i < l; i++)
            {
                if (node == null)
                    return false;

                node = node.links[chars[i] - letterShift];
            }

            if (node == null)
                return false;

            // our word is just a prefix of one word in the collection, no bueno
            if (node != null && !node.fullWord)
                return false;

            return true;
        }

        #endregion

        #region private methods

        private TrieNode CreateTrie()
        {
            return new TrieNode('\0');
        }

        //Add a new word, starting with the root
        private void Add(TrieNode root, string word)
        {
            int offset = 97;
            var l = word.Length;
            var chars = word.ToLower().ToCharArray();
            TrieNode currentNode = root;


            if (l > maxLength)
                maxLength = l;

            // build a new trie branch, character by character
            // for each node, links[0] =a', links[1] = 'b', ..links[26] ='c'
            for (int i = 0; i < l; i++)
            {
                // has the current node a child with character chars[i] - offset ?
                if (currentNode.links[chars[i] - offset] == null)
                {
                    currentNode.links[chars[i] - offset] = new TrieNode(chars[i]);
                }
                currentNode = currentNode.links[chars[i] - offset];
            }

            currentNode.fullWord = true;
        }

        private void TraverseTree(TrieNode root, int level, char[] branch)
        {
            if (root == null)
                return;

            for (int i = 0; i < root.links.Length; i++)
            {
                if (root.links[i] == null)
                {
                    continue;
                }
                branch[level] = root.links[i].letter;
                TraverseTree(root.links[i], level + 1, branch);
            }

            if (root.fullWord)
            {
                string word = new string(branch).Substring(0, level);
                this.wordsList.Add(word);
            }
        }

        #endregion

        #region helper classes

        private class AlinaList : IEnumerable<string>
        {
            private string[] items;
            private int size = 0;

            public AlinaList(int capacity)
            {
                size = 0;
                items = new string[capacity]; //considering the root
            }

            #region public
            public void Add(string word)
            {
                if (word != null)
                {
                    items[size] = word;
                    size++;
                }
            }

            public void Sort()
            {
                items = QuickSort(items, 0, size - 1);
            }

            public string[] Trim()
            {
                int count = 0;
                for (int i = 0; i < size - 1; i++)
                {
                    if (items[i] != null)
                        count++;
                }
                string[] trimmedArray = new string[count];
                for (int i = 0; i < count; i++)
                {
                    trimmedArray[i] = items[i];
                }

                return trimmedArray;
            }

            #endregion

            #region interface implementation
            public IEnumerator<string> GetEnumerator()
            {
                foreach (string s in items)
                {
                    yield return s;
                }
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
            #endregion

            #region private
            private string[] QuickSort(string[] a, int left, int right)
            {
                int i = left;
                int j = right;
                double pivotValue = ((left + right) / 2);
                string x = a[int.Parse(pivotValue.ToString())];

                while (i <= j)
                {
                    while (((IComparable)a[i]).CompareTo(x) < 0)
                    {
                        i++;
                    }
                    while (((IComparable)x).CompareTo(a[j]) < 0)
                    {
                        j--;
                    }

                    if (i <= j)
                    {
                        string temp = a[i];
                        a[i] = a[j];
                        i++;
                        a[j] = temp;
                        j--;
                    }
                }

                if (left < j)
                {
                    QuickSort(a, left, j);
                }
                if (i < right)
                {
                    QuickSort(a, i, right);
                }
                return a;
            }
            #endregion
        }

        private class TrieNode
        {
            public char letter;
            public TrieNode[] links;
            public Boolean fullWord;

            public TrieNode(char letter)
            {
                this.letter = letter;
                links = new TrieNode[26];
                this.fullWord = false;
            }
        }

        #endregion
    }

}
