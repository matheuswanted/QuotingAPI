using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quoting.Domain.Models;
using Quoting.Domain.Queries;
using Quoting.Domain.Repositories.Queryable;
using Quoting.Domain.ValueObjects;

namespace Quoting.Domain.Services
{
    public class QuotingCalculator : IQuotingCalculator
    {
        private readonly IQuotePriceQueryableRepository _queries;

        public QuotingCalculator(IQuotePriceQueryableRepository queries)
        {
            _queries = queries;
        }

        public async Task<BasePriceRule> SelectBasePriceRuleFor(Vehicle vehicle)
        {
            var rules = await _queries
                .Query<IBasePriceRulesAppliableToVehicleQuery>()
                .Run(new Queries.Requests.ByTypeAndYearOptional(vehicle.Type, vehicle.ManufacturingYear));

            var result = rules
                .Where(r => r.AppliableFor(vehicle))
                .Select(r => r.WithPriorityFor(vehicle))
                .OrderByDescending(r=>r.Priority)
                .FirstOrDefault();

            return result ?? DefaultBasePrice();
        }

        public async Task<PriceModifierRule> CalculateModifier(Customer customer)
        {
            IEnumerable<PriceModifierRule> rules = await _queries
                .Query<IPriceModifierRulesAppliableToCustomerQuery>()
                .Run();

            return rules.FirstOrDefault(r => r.Gender == customer.Gender && r.AgeRange.InRange(customer.Age));
        }

        public BasePriceRule DefaultBasePrice()
        {
            return new BasePriceRule(null,null,null,null,1000);
        }
    }
}
