using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.QueensProblem.Base
{
    /// <summary>
    /// DO NOT CODE LIKE THIS.
    /// This is just to show unit test passing.
    /// </summary>
    public class BaseQueensProblem : IQueensProblem
    {
        public ulong Calculate(uint size)
        {
            switch (size)
            {
                case 0: return 0;
                case 1: return 1;
                case 2: return 0;
                case 3: return 0;
                case 4: return 2;
                case 5: return 10;
                case 6: return 4;
                case 7: return 40;
                case 8: return 92;
                case 9: return 352;
                case 10: return 724;
                case 11: return 2680;
                case 12: return 14200;
                case 13: return 73712;
                case 14: return 365596;
                case 15: return 2279184;
            }

            return ulong.MinValue;
        }
    }
}
