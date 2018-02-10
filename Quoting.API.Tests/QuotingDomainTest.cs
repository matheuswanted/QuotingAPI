using Quoting.API.Tests.DataGenerator;
using Quoting.Domain.Models;
using Quoting.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Quoting.API.Tests
{
    public class QuotingDomainTest
    {
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
            decimal basePrice = calcService.CalculateBasePrice(new Vehicle
            {
                Maker = "Ford",
                Model = "Focus",
                ManufacturingYear = 2018,
                Type = "Van"
            }).Result;
            Assert.Equal(calcService.DefaultBasePrice(), basePrice);
        }

        [Theory]
        [MemberData(nameof(DomainGenerator.CreateBasePriceScenarios), MemberType = typeof(DomainGenerator))]
        public void CalculateBasePrice_ShouldReturnConfiguredPrice_WhenThereIsARuleAppliableToTheVehicle(Vehicle vehicle, IQuotingCalculator calculator, decimal basePrice)
        {
            Assert.Equal(basePrice, calculator.CalculateBasePrice(vehicle).Result);
        }
    }
}
