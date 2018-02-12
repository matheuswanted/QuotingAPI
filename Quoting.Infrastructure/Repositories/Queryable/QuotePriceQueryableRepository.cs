using Quoting.Domain.Queries;
using Quoting.Domain.Repositories.Queryable;
using System;

namespace Quoting.Infrastructure.Repositories.Queryable
{
    public class QuotePriceQueryableRepository : QueryableRepositoryBase<IQuotePriceQuery>, IQuotePriceQueryableRepository
    {
        public QuotePriceQueryableRepository(IServiceProvider provider) : base(provider)
        {
        }
    }
}
