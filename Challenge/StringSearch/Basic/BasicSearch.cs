using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.StringSearch.Basic
{
    public class BasicSearch : ISearch
    {
        List<string> _List;

        public void LoadWordSet(IEnumerable<string> unorderedWordSet)
        {
            _List = unorderedWordSet.ToList();
            _List.Sort();
        }

        public IEnumerable<string> GetOrderedWordSet()
        {
            return _List.ToArray();
        }

        public bool WordExistsInSet(string wordToFind)
        {
            return _List.Any(s => s.Equals(wordToFind, StringComparison.OrdinalIgnoreCase));
        }
    }
}
