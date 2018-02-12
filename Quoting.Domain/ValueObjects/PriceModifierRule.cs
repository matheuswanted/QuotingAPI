using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.ValueObjects
{
    public class PriceModifierRule : IValueObject
    {
        /// <summary>
        /// Used only for EF Core
        /// </summary>
        private PriceModifierRule() { }
        public PriceModifierRule(string gender, Range ageRange, decimal modifier)
        {
            Gender = gender;
            AgeRange = ageRange;
            Modifier = modifier;
        }
        public string Gender { get; private set; }
        public Range AgeRange { get; private set; }
        public decimal Modifier { get; private set; }

        public bool Same(IValueObject @object)
        {
            var rule = @object as PriceModifierRule;
            return rule != null && Gender == rule.Gender && Modifier == rule.Modifier && AgeRange.Same(rule.AgeRange);
        }
    }
}
