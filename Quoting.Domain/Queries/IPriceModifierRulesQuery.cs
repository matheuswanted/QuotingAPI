using Quoting.Domain.Seedworking;
using Quoting.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.Queries
{
    public interface IPriceModifierRulesAppliableToCustomerQuery : IQuery<IEnumerable<PriceModifierRule>>, IQuotePriceQuery
    {
    }
}
