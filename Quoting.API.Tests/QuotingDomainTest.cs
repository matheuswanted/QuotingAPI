using Quoting.API.Tests.DataGenerator;
using Quoting.Domain.Models;
using Quoting.Domain.Seedworking;
using Quoting.Domain.Services;
using Quoting.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Quoting.API.Tests
{
    public class QuotingDomainTest
    {
        private void AssertValueObject(IValueObject expected, IValueObject actual)
        {
            if (!expected.Same(actual))
                throw new Xunit.Sdk.EqualException(expected, actual);
        }
        [Theory]
        [MemberData(nameof(DomainGenerator.CreateModifierScenarios), MemberType = typeof(DomainGenerator))]
        public void CalculateModifier_ShouldReturnAModifierByAgeRangeAndGender(Customer customer, IQuotingCalculator calculator, decimal modifier)
        {
            Assert.Equal(modifier, calculator.CalculateModifier(customer).Result);
        }

        [Fact]
        public void CalculateBasePrice_ShouldReturnDefaultBasePrice_WhenNoRuleSatisfyingTheCriteriasExist()
        {
            var calcService = DomainGenerator.NewCalculator();
            BasePriceRule basePrice = calcService.SelectBasePriceRuleFor(new Vehicle
            {
                Make = "Ford",
                Model = "Focus",
                ManufacturingYear = 2018,
                Type = "Van"
            }).Result;

            AssertValueObject(calcService.DefaultBasePrice(), basePrice);
        }

        [Theory]
        [MemberData(nameof(DomainGenerator.CreateBasePriceScenarios), MemberType = typeof(DomainGenerator))]
        public void SelectBasePriceRuleFor_ShouldReturnConfiguredPrice_WhenThereIsARuleAppliableToTheVehicle(Vehicle vehicle, IQuotingCalculator calculator, BasePriceRule basePrice)
        {
            BasePriceRule resultRule = calculator.SelectBasePriceRuleFor(vehicle).Result;

            AssertValueObject(basePrice, resultRule);
        }
    }
}
