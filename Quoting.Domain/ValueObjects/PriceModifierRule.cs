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
    public class BasePriceRule : IValueObject
    {
        public BasePriceRule(int? year, string type, string model, string make, decimal basePrice)
        {
            BasePrice = basePrice;
            Make = make;
            Model = model;
            Type = type;
            Year = year;
        }
        public int? Year { get; }
        public string Type { get; }
        public string Make { get; }
        public string Model { get; }
        public decimal BasePrice { get; }

        public bool Same(IValueObject @object)
        {
            var rule = @object as BasePriceRule;
            return rule != null && rule.Year == Year && rule.Type == Type && rule.Model == Model && rule.Make == Make && rule.BasePrice == BasePrice;
        }
    }
}
