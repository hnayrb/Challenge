using System;
using NUnit.Framework;
using System.Linq;

namespace Challenge.QueensProblem
{
    /// <summary>
    /// Tests will include, but may not be limited to the following.
    /// </summary>
    [TestFixture(typeof(Base.BaseQueensProblem))]
    [Category("QueensProblem")]
    public class QueensProblemTests
    {
        private IQueensProblem _queensProblem;

        public QueensProblemTests(Type type)
        {
            _queensProblem = type.CreateInstance<IQueensProblem>();
        }

        [TestCase(0U, 0UL)]
        [TestCase(1U, 1UL)]
        [TestCase(2U, 0UL)]
        [TestCase(3U, 0UL)]
        [TestCase(4U, 2UL)]
        [TestCase(5U, 10UL)]
        [TestCase(6U, 4UL)]
        [TestCase(7U, 40UL)]
        [TestCase(8U, 92UL)]
        [TestCase(9U, 352UL)]
        [TestCase(10U, 724UL)]
        [TestCase(11U, 2680UL)]
        [TestCase(12U, 14200UL)]
        [TestCase(13U, 73712UL)]
        [TestCase(14U, 365596UL)]
        [TestCase(15U, 2279184UL)]
        public void CalculateTest(uint size, ulong expectedValue)
        {
            Assert.AreEqual(expectedValue, _queensProblem.Calculate(size));
        }
    }
}
