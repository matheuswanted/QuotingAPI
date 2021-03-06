﻿using System;
using Quoting.Domain.Models;
using Quoting.Domain.Seedworking;

namespace Quoting.Domain.ValueObjects
{
    public class BasePriceRule : IValueObject
    {
        private BasePriceRule() { }
        public BasePriceRule(int? year, string type, string model, string make, decimal basePrice)
        {
            BasePrice = basePrice;
            Make = make;
            Model = model;
            Type = type;
            Year = year;
        }
        public int? Year { get; private set; }
        public string Type { get; private set; }
        public string Make { get; private set; }
        public string Model { get; private set; }
        public decimal BasePrice { get; private set; }

        public bool Same(IValueObject @object)
        {
            var rule = @object as BasePriceRule;
            return rule != null && rule.Year == Year && rule.Type == Type && rule.Model == Model && rule.Make == Make && rule.BasePrice == BasePrice;
        }

        public bool AppliableFor(Vehicle vehicle)
        {
            if (Type != vehicle.Type)
                return false;
            if (Year.HasValue)
            {
                if (Year != vehicle.ManufacturingYear)
                    return false;
                if (!string.IsNullOrEmpty(Make))
                {
                    if(Make != vehicle.Make)
                        return false;

                    if (!string.IsNullOrEmpty(Model) && Model != vehicle.Model)
                        return false;
                }
            }
            return true;
        }

        internal PriorityBasePriceRule WithPriorityFor(Vehicle vehicle)
        {
            int priority = -1;
            if (Type == vehicle.Type)
                priority++;
            if (Year == vehicle.ManufacturingYear)
                priority++;
            if (Make == vehicle.Make)
                priority++;
            if (Model == vehicle.Model)
                priority++;
            return new PriorityBasePriceRule(this, priority);
        }
    }
}
