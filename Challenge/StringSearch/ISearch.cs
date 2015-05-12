using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.StringSearch
{
    /// <summary>
    /// Assume no NULL or EMPTYSTRINGS will be used
    /// </summary>
    public interface ISearch
    {
        /// <summary>
        /// Performance only is checked here.
        /// Naming convention for your inherited class should be named as public class [YourName]_Search { ... }
        /// </summary>
        void LoadWordSet(IEnumerable<string> unorderedWordSet);

        /// <summary>
        /// Validity only checked here. No performance.
        /// </summary>
        IEnumerable<string> GetOrderedWordSet();

        /// <summary>
        /// Performance and validity checked here.
        /// </summary>
        /// <param name="wordToFind">Should be case insensitive.</param>
        /// <returns>True if word is found in list.</returns>
        bool WordExistsInSet(string wordToFind);
    }
}
