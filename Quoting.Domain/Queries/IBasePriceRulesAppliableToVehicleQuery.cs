using Quoting.Domain.Models;
using Quoting.Domain.Queries.Requests;
using Quoting.Domain.ValueObjects;

namespace Quoting.Domain.Queries
{
    public interface IBasePriceRulesAppliableToVehicleQuery : IQuery<BasePriceRule, ByTypeAndYearOptional>
    {

    }
}
