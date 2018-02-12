using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.ValueObjects
{
    public class Range : IValueObject
    {
        private Range() { }
        public Range(int? start, int? end)
        {
            Start = start;
            End = end;
        }
        public int? Start { get; private set; }
        public int? End { get; private set; }
        public bool InRange(int value)
            => (Start == null || value >= Start) && (End == null || value <= End);

        public bool Same(IValueObject @object)
        {
            var range = @object as Range;
            return range != null && range.Start == Start && End == range.End;
        }
    }
}
