using Quoting.Domain.Models;
using Quoting.Domain.Queries.Requests;
using Quoting.Domain.Seedworking;
using Quoting.Domain.ValueObjects;
using System.Collections.Generic;

namespace Quoting.Domain.Queries
{
    public interface IBasePriceRulesAppliableToVehicleQuery : IQuery<IEnumerable<BasePriceRule>, ByTypeAndYearOptional>
    {

    }
}
