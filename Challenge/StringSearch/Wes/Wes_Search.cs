using System.Collections.Generic;
using System;

namespace Challenge.StringSearch.Wes
{
	public class Wes_Search : ISearch
	{
		private List<string> _words;

		public Wes_Search()
		{
			_words = new List<string>();
		}

		public void LoadWordSet(IEnumerable<string> unorderedWordSet)
		{
			foreach (var word in unorderedWordSet)
			{
				_words.Add(word);
			}
		}

		public IEnumerable<string> GetOrderedWordSet()
		{
			for (var i = 0; i < _words.Count; i++)
			{
				var first = _words[i][0];
				var l = i;

				for (var j = i + 1; j < _words.Count; j++)
				{
					var second = _words[j][0];
					if (first > second)
					{
						l = j;
						first = second;
					}
				}

				if (i != l) // Don't swap if they are equal
					Swap(i, l);
			}
			return _words;
		}

		public bool WordExistsInSet(string word)
		{
			var min = 0;
			var max = _words.Count - 1;
			var target = word[0]; // find me

			while (max >= min)
			{
				var g = (min + max) / 2;
				var guess = _words[g][0];

				if (guess == target)
				{
					return true;
				}
				else if (guess < target)
				{
					min = g + 1;
				}
				else if (guess > target)
				{
					max = g - 1;
				}
			}

			return false;
		}

#region Peek

		/// <summary>Take a peek at the private unordered word set</summary>
		public string Peek()
		{
			return String.Join(", ", _words);
		}

#endregion

#region Private methods

		private void Swap(int i, int j)
		{
			var _ = _words[i];
			_words[i] = _words[j];
			_words[j] = _;
		}

#endregion
	}
}

