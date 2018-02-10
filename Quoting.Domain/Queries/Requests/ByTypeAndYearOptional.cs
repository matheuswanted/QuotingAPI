using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.Queries.Requests
{
    public class ByTypeAndYearOptional
    {
        public ByTypeAndYearOptional(string type, int year)
        {
            Type = type;
            Year = year;
        }
        public int Year { get; }
        public string Type { get; }
    }
}
