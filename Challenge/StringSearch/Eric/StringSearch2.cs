using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.StringSearch.Eric
{
    public class StringSearch2 : ISearch
    {
        private BTree<string> _tree = new BTree<string>(StringComparer.OrdinalIgnoreCase);

        public void LoadWordSet(IEnumerable<string> unorderedWordSet)
        {
            foreach (var word in unorderedWordSet)
                _tree.Add(word);
        }

        public IEnumerable<string> GetOrderedWordSet()
        {
            return _tree;
        }

        public bool WordExistsInSet(string wordToFind)
        {
            return _tree.Exists(wordToFind);
        }

        public override string ToString()
        {
            return _tree.ToString();
        }
    }
}
