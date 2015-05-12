using System;
using NUnit.Framework;
using System.Linq;

namespace Challenge.StringSearch
{
    [TestFixture(typeof(Basic.BasicSearch))]
    [TestFixture(typeof(James.James_Search))]
    [TestFixture(typeof(Alina.Alina_Search_Hard))]
    [TestFixture(typeof(Eric.StringSearch))]
    [TestFixture(typeof(Eric.StringSearch2))]
    [TestFixture(typeof(Van.VT_Search))]
    [Category("StringSearch")]
    public class StringSearchTests
    {
        private const string CHARACTERS_ALL = "abcdefghijklmnopqrstuvwxyzABCDEFHIJKLMNOPQRSTUVWXYZ";
        private const string CHARACTERS_NO_A = "bcdefghijklmnopqrstuvwxyzBCDEFHIJKLMNOPQRSTUVWXYZ";

        private ISearch _stringSearch;
        private string[] _randomWords;
        private string[] _randomWordsWithA;

        public StringSearchTests(Type type)
        {
            _stringSearch = type.CreateInstance<ISearch>();

            var random = new Random();
            var randomLength = new Random();
            var totalWords = 100000;
            var totalWordsWithA = 100;

            _randomWords = new string[totalWords];
            _randomWordsWithA = new string[totalWordsWithA];
            for (var i = 0; i < totalWords; i++)
            {
                _randomWords[i] = new string(Enumerable.Repeat(CHARACTERS_NO_A, randomLength.Next(20) + 1).Select(s => s[random.Next(s.Length)]).ToArray());
                if (i < totalWordsWithA)
                    _randomWordsWithA[i] = string.Concat("a", new string(Enumerable.Repeat(CHARACTERS_ALL, randomLength.Next(20)).Select(s => s[random.Next(s.Length)]).ToArray()));
            }
            _stringSearch.LoadWordSet(_randomWords);
        }

        [Test]
        public void FindAllWordsTest()
        {
            var foundAll = true;
            for (var i = 0; i < _randomWords.Length; i++)
            {
                if (!_stringSearch.WordExistsInSet(_randomWords[i]))
                {
                    foundAll = false;
                    break;
                }
            }
            Assert.IsTrue(foundAll);
        }

        [Test]
        public void NotContainsAllTest()
        {
            var foundAny = false;
            for (var i = 0; i < _randomWordsWithA.Length; i++)
            {
                if (_stringSearch.WordExistsInSet(_randomWordsWithA[i]))
                {
                    foundAny = true;
                    break;
                }
            }
            Assert.False(foundAny);
        }

        [Test]
        [TestCase(0)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(99999)]
        public void ContainsTestRandomTest(int index)
        {
            Assert.IsTrue(_stringSearch.WordExistsInSet(_randomWords[index]));
        }

        [Test]
        [TestCase(0)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(99999)]
        public void ContainsTestRandomLowerTest(int index)
        {
            Assert.IsTrue(_stringSearch.WordExistsInSet(_randomWords[index].ToLower()));
        }

        [Test]
        [TestCase(0)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(99999)]
        public void ContainsTestRandomUpperTest(int index)
        {
            Assert.IsTrue(_stringSearch.WordExistsInSet(_randomWords[index].ToUpper()));
        }

        [Test]
        public void IsSortedTest()
        {
            var isSorted = true;
            var lastWord = string.Empty;
            foreach (var item in _stringSearch.GetOrderedWordSet())
            {
                if (!string.IsNullOrEmpty(lastWord) && string.Compare(lastWord, item, true) > 0)
                {
                    isSorted = false;
                    break;
                }

                lastWord = item;
            }
            Assert.IsTrue(isSorted);
        }
    }
}
