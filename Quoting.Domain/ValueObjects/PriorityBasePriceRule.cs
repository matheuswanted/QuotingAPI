using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.ValueObjects
{
    public class PriorityBasePriceRule : BasePriceRule
    {
        public PriorityBasePriceRule(BasePriceRule rule, int priority) 
            : base(rule.Year, rule.Type, rule.Model, rule.Make, rule.BasePrice)
        {
            Priority = priority;
        }
        
        public int Priority { get; }
    }
}
