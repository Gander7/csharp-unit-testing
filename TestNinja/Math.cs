using System;
using System.Collections.Generic;
using System.Text;

namespace TestNinja
{
    public class Math
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
        public int Max(int a, int b)
        {
            return (a > b) ? a : b;
        }
        public IEnumerable<int> GetOddNumbers(int limit)
        {
            throw new NotImplementedException();
        }
    }
}
