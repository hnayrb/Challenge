using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.QueensProblem
{
    /// <summary>
    /// Given you have an N queens on an NxN chessboard, using all the queens at once, how many configurations of queen placements
    /// are possible. Only one queen can occupy one space at a time (just in case anyone were to ask :).
    /// This is a well known problem, so we don't expect just a hard coded answer directly.
    /// 
    /// Will be judged on speed and correctness.
    /// </summary>
    public interface IQueensProblem
    {
        /// <param name="size">N</param>
        /// <returns>Number of configurations possible.</returns>
        ulong Calculate(uint size);
    }
}
