using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quoting.Domain.Models;
using Quoting.Domain.Queries;
using Quoting.Domain.ValueObjects;

namespace Quoting.Domain.Services
{
    public class QuotingCalculator : IQuotingCalculator
    {
        private readonly IPriceModifierRulesAppliableToCustomerQuery _priceModifierQuery;
        private readonly IBasePriceRulesAppliableToVehicleQuery _basePriceRulesQuery;

        public QuotingCalculator(IPriceModifierRulesAppliableToCustomerQuery priceModifierRulesQuery, IBasePriceRulesAppliableToVehicleQuery basePriceRulesQuery)
        {
            _priceModifierQuery = priceModifierRulesQuery;
            _basePriceRulesQuery = basePriceRulesQuery;
        }

        public async Task<decimal> CalculateBasePrice(Vehicle vehicle)
        {
            var result = await _basePriceRulesQuery.Run(new Queries.Requests.ByTypeAndYearOptional(vehicle.Type, vehicle.ManufacturingYear));
            return DefaultBasePrice();
        }

        public async Task<decimal> CalculateModifier(Customer customer)
        {
            IEnumerable<PriceModifierRule> rules = await _priceModifierQuery.Run();
            PriceModifierRule rule = rules.FirstOrDefault(r => r.Gender == customer.Gender && r.AgeRange.InRange(customer.Age));
            return rule.Modifier;
        }

        public decimal DefaultBasePrice()
        {

            return 1000;
        }
    }
}
