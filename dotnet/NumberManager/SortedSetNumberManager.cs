using System.Collections.Generic;
using System.Linq;

namespace NumberManager
{
    public class SortedSetNumberManager : INumberManager
    {
        private SortedSet<int> _available;
        private int _max = 0;

        public int GetNumber()
        {
            if (_available?.Any() == true)
            {
                var result = _available.Min;
                _available.Remove(result);
                return result;
            }
            else
            {
                return _max++;
            }

        }

        public void ReleaseNumber(int number)
        {
            if (number + 1 == _max)
            {
                _max--;
                return;
            }

            _available = _available ?? new SortedSet<int>();
            _available.Add(number);
        }
    }
}