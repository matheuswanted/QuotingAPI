using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quoting.Domain.Models;
using Quoting.Domain.ValueObjects;

namespace Quoting.Domain.Services
{
    public interface IQuotingCalculator
    {
        Task<PriceModifierRule> CalculateModifier(Customer customer);
        Task<BasePriceRule> SelectBasePriceRuleFor(Vehicle vehicle);
        BasePriceRule DefaultBasePrice();
    }
}
