using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.ValueObjects
{
    public class PriceModifierRule : IValueObject
    {
        public PriceModifierRule(string gender, Range ageRange, decimal modifier)
        {
            Gender = gender;
            AgeRange = ageRange;
            Modifier = modifier;
        }
        public string Gender { get; }
        public Range AgeRange { get; }
        public decimal Modifier { get;  }

        public bool Same(IValueObject @object)
        {
            var rule = @object as PriceModifierRule;
            return rule != null && Gender == rule.Gender && Modifier == rule.Modifier && AgeRange.Same(rule.AgeRange);
        }
    }
}
