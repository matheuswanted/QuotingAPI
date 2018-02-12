using Quoting.Domain.Queries;
using Quoting.Domain.Seedworking;

namespace Quoting.Domain.Repositories.Queryable
{
    public interface IQuotePriceQueryableRepository : IQuotePriceQuery, IQueryableRepository<IQuotePriceQuery>
    {

    }
}
