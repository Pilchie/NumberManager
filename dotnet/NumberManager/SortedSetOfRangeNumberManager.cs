using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NumberManager
{
    public class SortedSetOfRangeNumberManager : INumberManager
    {
        private readonly SortedSet<Range> _available = new(new[] { new Range(0, int.MaxValue) });

        public int GetNumber()
        {
            var first = _available.Min;
            _available.Remove(first);
            var result = first.Start;
            if (first.Length > 1)
            {
                _available.Add(new Range(first.Start + 1, first.Length - 1));
            }
            return result;
        }

        public void ReleaseNumber(int number)
        {
            // TODO: Find the item without iterating?
            Range? prev = null;
            foreach (var cur in _available)
            {
                if (cur.Start > number)
                {
                    if (cur.Start == number + 1 &&
                        prev?.Start + prev?.Length + 1 == number)
                    {
                        _available.Remove(prev!.Value);
                        _available.Remove(cur);
                        _available.Add(new Range(prev.Value.Start, prev.Value.Length + 1 + cur.Length));
                    }
                    else if (cur.Start == number + 1)
                    {
                        _available.Remove(cur);
                        _available.Add(new Range(cur.Start - 1, cur.Length + 1));
                    }
                    else if (prev?.Start + prev?.Length == number)
                    {
                        _available.Remove(prev.Value);
                        _available.Add(new Range(prev.Value.Start, prev.Value.Length + 1));
                    }
                    else
                    {
                        _available.Add(new Range(number, 1));
                    }

                    break;
                }

                prev = cur;
            }
        }
    }
}