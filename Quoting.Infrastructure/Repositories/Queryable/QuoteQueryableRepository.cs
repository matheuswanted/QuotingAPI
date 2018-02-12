using Quoting.Domain.Queries;
using Quoting.Domain.Repositories.Queryable;
using System;

namespace Quoting.Infrastructure.Repositories.Queryable
{
    public class QuoteQueryableRepository : QueryableRepositoryBase<IQuoteQuery>, IQuoteQueryableRepository
    {
        public QuoteQueryableRepository(IServiceProvider provider) : base(provider)
        {
        }
    }
}
