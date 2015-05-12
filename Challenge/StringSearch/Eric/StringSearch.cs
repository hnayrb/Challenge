using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.StringSearch.Eric
{
    public class StringSearch : ISearch
    {
        private HashSet<string> _words;
        private SortedSet<string> _sortedWords;

        public void LoadWordSet(IEnumerable<string> unorderedWordSet)
        {
            _words = new HashSet<string>(unorderedWordSet, StringComparer.OrdinalIgnoreCase);
            _sortedWords = new SortedSet<string>(unorderedWordSet, StringComparer.OrdinalIgnoreCase);
        }

        public IEnumerable<string> GetOrderedWordSet()
        {
            return _sortedWords;
        }

        public bool WordExistsInSet(string wordToFind)
        {
            return _words.Contains(wordToFind);
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            foreach (var item in _sortedWords)
                output.AppendFormat("{0},", item);
            if (output.Length > 0)
                output.Remove(output.Length - 1, 1);
            return output.ToString();
        }
    }
}
