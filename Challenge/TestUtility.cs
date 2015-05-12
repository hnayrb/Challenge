using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge
{
    public static class TestUtility
    {
        /// <summary>
        /// NOTE: DO NOT EVER CREATE CODE LIKE THIS IN PRODUCTION.
        /// It's okay though for tests.
        /// </summary>
        public static T CreateInstance<T>(this Type type)
        {
            var constructorInfo = type.GetConstructor(new Type[0]);
            return (T)constructorInfo.Invoke(new object[0]);
        }
    }
}
