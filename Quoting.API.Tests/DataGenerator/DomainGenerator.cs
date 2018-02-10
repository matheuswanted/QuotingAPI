using Moq;
using Quoting.Domain.Models;
using Quoting.Domain.Queries;
using Quoting.Domain.Services;
using Quoting.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.API.Tests.DataGenerator
{
    public class DomainGenerator
    {
        public static IEnumerable<object[]> CreateModifierScenarios()
        {
            var calculator = NewCalculator();
            yield return new object[] { NewCustomer(16, "M"), calculator, 1.5M };
            yield return new object[] { NewCustomer(24, "M"), calculator, 1.5M };
            yield return new object[] { NewCustomer(25, "M"), calculator, 1.2M };
            yield return new object[] { NewCustomer(34, "M"), calculator, 1.2M };
            yield return new object[] { NewCustomer(35, "M"), calculator, 1M };
            yield return new object[] { NewCustomer(60, "M"), calculator, 1M };
            yield return new object[] { NewCustomer(80, "M"), calculator, 1.3M };
            yield return new object[] { NewCustomer(16, "F"), calculator, 1.4M };
            yield return new object[] { NewCustomer(24, "F"), calculator, 1.4M };
            yield return new object[] { NewCustomer(25, "F"), calculator, 1M };
            yield return new object[] { NewCustomer(60, "F"), calculator, 1M };
            yield return new object[] { NewCustomer(80, "F"), calculator, 1.2M };
        }
        public static IEnumerable<object[]> CreateBasePriceScenarios()
        {
            var calculator = NewCalculator();
            yield return new object[] { NewVehicle(2008, "Fiat", "Palio"), calculator, 1800M };
            yield return new object[] { NewVehicle(1966, "Fiat", "Palio"), calculator, 1400M };
            yield return new object[] { NewVehicle(1966, "Fiat", "Uno"), calculator, 2500M };
            yield return new object[] { NewVehicle(2006, "BMW", "M3"), calculator, 3000M };
        }
        private static Customer NewCustomer(int age, string gender)
            => new Customer
            {
                BirthDate = DateTime.Now.AddYears(-age),
                Gender = gender
            };
        private static Vehicle NewVehicle(int year, string maker, string model)
            => new Vehicle
            {
                Type = "Car",
                ManufacturingYear = year,
                Maker = maker,
                Model = model
            };
        public static IQuotingCalculator NewCalculator()
        {
            var mockPriceModifierRulesAppliableToCustomerQuery = new Mock<IPriceModifierRulesAppliableToCustomerQuery>();
            var mockBasePriceRulesAppliableToVehicleQuery = new Mock<IBasePriceRulesAppliableToVehicleQuery>();

            mockPriceModifierRulesAppliableToCustomerQuery.Setup(m => m.Run()).ReturnsAsync(Run);
            mockBasePriceRulesAppliableToVehicleQuery
                .Setup(m => m.Run(It.IsAny<Domain.Queries.Requests.ByTypeAndYearOptional>()))
                .ReturnsAsync(() => RunBasePriceQuery());

            return new QuotingCalculator(mockPriceModifierRulesAppliableToCustomerQuery.Object, mockBasePriceRulesAppliableToVehicleQuery.Object);
        }
        private static IEnumerable<BasePriceRule> RunBasePriceQuery()
        {
            yield return new BasePriceRule(null, "Car", null, null, 3000);
            yield return new BasePriceRule(1966, "Car", "Palio", "Fiat", 1400);
            yield return new BasePriceRule(2008, "Car", null, null, 1800);
            yield return new BasePriceRule(1966, "Car", null, "Fiat", 2500);
        }
        private static IEnumerable<PriceModifierRule> Run()
        {
            yield return new PriceModifierRule("M", new Domain.ValueObjects.Range(16, 24), 1.5M);
            yield return new PriceModifierRule("M", new Domain.ValueObjects.Range(25, 34), 1.2M);
            yield return new PriceModifierRule("M", new Domain.ValueObjects.Range(35, 60), 1M);
            yield return new PriceModifierRule("M", new Domain.ValueObjects.Range(61, null), 1.3M);
            yield return new PriceModifierRule("F", new Domain.ValueObjects.Range(16, 24), 1.4M);
            yield return new PriceModifierRule("F", new Domain.ValueObjects.Range(25, 60), 1M);
            yield return new PriceModifierRule("F", new Domain.ValueObjects.Range(61, null), 1.2M);
        }
    }
}
