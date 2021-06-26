using System;
using System.Diagnostics;

namespace NumberManager
{
    [DebuggerDisplay("{GetDebuggerDisplay()}")]
    public readonly struct Range : IComparable<Range>
    {
        public readonly int Start;
        public readonly int Length;

        public Range(int start, int length)
        {
            this.Start = start;
            this.Length = length;
        }

        public int CompareTo(Range other)
        {
            return Start - other.Start;
        }

        public override string ToString()
        {
            return $"({Start}, {Length})";
        }

        private string GetDebuggerDisplay()
        {
            return $"[{Start}..{Start + Length - 1}]";
        }
    }
}