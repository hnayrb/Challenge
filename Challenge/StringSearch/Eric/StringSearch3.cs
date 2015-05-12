using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.StringSearch.Eric
{
    public class StringSearch3 : ISearch
    {
        private string[][] wordHash;

        public void LoadWordSet(IEnumerable<string> unorderedWordSet)
        {
            var setLength = 0;
            foreach (var word in unorderedWordSet) setLength++;

            wordHash = new string[setLength][];
            foreach (var word in unorderedWordSet)
            {
                var wordLower = word.ToLower();
                var wordToFindIndex = Math.Abs(wordLower.GetHashCode()) % setLength;
                if (wordHash[wordToFindIndex] == null)
                    wordHash[wordToFindIndex] = new string[0];

                var found = false;
                for (var i = 0; i < wordHash[wordToFindIndex].Length; i++)
                {
                    if (wordHash[wordToFindIndex][i] == wordLower)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    var oldArray = wordHash[wordToFindIndex];
                    wordHash[wordToFindIndex] = new string[oldArray.Length + 1];
                    for (var i = 0; i < oldArray.Length; i++)
                        wordHash[wordToFindIndex][i] = oldArray[i];
                    wordHash[wordToFindIndex][oldArray.Length] = wordLower;
                }
            }
        }

        public IEnumerable<string> GetOrderedWordSet()
        {
            var bTree = new BTree<string>(StringComparer.OrdinalIgnoreCase);

            var allWords = new string[wordHash.Length];

            for (var i = 0; i < wordHash.Length; i++)
            {
                if (wordHash[i] != null)
                {
                    for (var j = 0; j < wordHash[i].Length; j++)
                    {
                        bTree.Add(wordHash[i][j]);
                    }
                }
            }

            return bTree;
        }

        public bool WordExistsInSet(string wordToFind)
        {
            wordToFind = wordToFind.ToLower();
            var wordToFindIndex = Math.Abs(wordToFind.GetHashCode()) % wordHash.Length;
            var words = wordHash[wordToFindIndex];

            if (words != null)
            {
                for (var i = 0; i < words.Length; i++)
                {
                    if (words[i] == wordToFind)
                        return true;
                }
            }
            return false;
        }
    }
}
