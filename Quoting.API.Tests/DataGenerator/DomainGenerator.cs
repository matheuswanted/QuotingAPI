using Moq;
using Quoting.Domain.Models;
using Quoting.Domain.Queries;
using Quoting.Domain.Repositories.Queryable;
using Quoting.Domain.Services;
using Quoting.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quoting.API.Tests.DataGenerator
{
    public class DomainGenerator
    {
        private static BasePriceRule R_1800 = new BasePriceRule(2008, "Car", null, null, 1800);
        private static BasePriceRule R_1400 = new BasePriceRule(1966, "Car", "Palio", "Fiat", 1400);
        private static BasePriceRule R_2500 = new BasePriceRule(1966, "Car", null, "Fiat", 2500);
        private static BasePriceRule R_3000 = new BasePriceRule(null, "Car", null, null, 3000);
        public static IEnumerable<object[]> CreateModifierScenarios()
        {
            var calculator = NewCalculator();
            var rules = Run().ToArray();
            yield return new object[] { NewCustomer(16, "M"), calculator, rules[0] };
            yield return new object[] { NewCustomer(24, "M"), calculator, rules[0] };
            yield return new object[] { NewCustomer(25, "M"), calculator, rules[1] };
            yield return new object[] { NewCustomer(34, "M"), calculator, rules[1] };
            yield return new object[] { NewCustomer(35, "M"), calculator, rules[2] };
            yield return new object[] { NewCustomer(60, "M"), calculator, rules[2] };
            yield return new object[] { NewCustomer(80, "M"), calculator, rules[3] };
            yield return new object[] { NewCustomer(16, "F"), calculator, rules[4] };
            yield return new object[] { NewCustomer(24, "F"), calculator, rules[4] };
            yield return new object[] { NewCustomer(25, "F"), calculator, rules[5] };
            yield return new object[] { NewCustomer(60, "F"), calculator, rules[5] };
            yield return new object[] { NewCustomer(80, "F"), calculator, rules[6] };
        }
        public static IEnumerable<object[]> CreateBasePriceScenarios()
        {
            var calculator = NewCalculator();
            yield return new object[] { NewVehicle(2008, "Fiat", "Palio"), calculator, R_1800 };
            yield return new object[] { NewVehicle(1966, "Fiat", "Palio"), calculator, R_1400 };
            yield return new object[] { NewVehicle(1966, "Fiat", "Uno"), calculator, R_2500 };
            yield return new object[] { NewVehicle(2006, "BMW", "M3"), calculator, R_3000 };
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
                Make = maker,
                Model = model
            };
        public static IQuoteRulesCalculatorService NewCalculator()
        {
            var mockPriceModifierRulesAppliableToCustomerQuery = new Mock<IPriceModifierRulesAppliableToCustomerQuery>();
            var mockBasePriceRulesAppliableToVehicleQuery = new Mock<IBasePriceRulesAppliableToVehicleQuery>();
            var mockQueryableRepo = new Mock<IQuotePriceQueryableRepository>();

            mockPriceModifierRulesAppliableToCustomerQuery.Setup(m => m.Run()).ReturnsAsync(Run);
            mockBasePriceRulesAppliableToVehicleQuery
                .Setup(m => m.Run(It.IsAny<Domain.Queries.Requests.ByTypeAndYearOptional>()))
                .ReturnsAsync(() => RunBasePriceQuery());
            mockQueryableRepo
                .Setup(m => m.Query<IPriceModifierRulesAppliableToCustomerQuery>())
                .Returns(() => mockPriceModifierRulesAppliableToCustomerQuery.Object);
            mockQueryableRepo
                .Setup(m => m.Query<IBasePriceRulesAppliableToVehicleQuery>())
                .Returns(() => mockBasePriceRulesAppliableToVehicleQuery.Object);

            return new QuoteRulesCalculatorService(mockQueryableRepo.Object);
        }
        private static IEnumerable<BasePriceRule> RunBasePriceQuery()
        {
            yield return R_3000;
            yield return R_1400;
            yield return R_1800;
            yield return R_2500;
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
