using System;
using System.Collections.Generic;

namespace NumberManager
{
    public class HashSetNumberManager : INumberManager
    {
        private HashSet<int> _numbers = new HashSet<int>();
        
        public int GetNumber()
        {
            for (int i = 0; i <= int.MaxValue; i++)
            {
                if (!_numbers.Contains(i))
                {
                    _numbers.Add(i);
                    return i;
                }
            }

            throw new Exception("Out of numbers!!!");
        }

        public void ReleaseNumber(int number)
        {
            _numbers.Remove(number);
        }
    }
}