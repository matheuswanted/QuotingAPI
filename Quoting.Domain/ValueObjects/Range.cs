using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.ValueObjects
{
    public class Range
    {
        public Range(int? start, int? end)
        {
            Start = start;
            End = end;
        }
        public int? Start { get; }
        public int? End { get; }
        public bool InRange(int value)
            => (Start == null || value >= Start) && (End == null || value <= End);
    }
}
