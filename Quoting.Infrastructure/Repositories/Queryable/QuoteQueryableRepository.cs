using Microsoft.Extensions.DependencyInjection;
using Quoting.Domain.Queries;
using Quoting.Domain.Repositories.Queryable;
using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quoting.Infrastructure.Repositories.Queryable
{
    public class QuoteQueryableRepository : IQuoteQueryableRepository
    {
        private readonly IEnumerable<IQuoteQuery> _queries;

        public QuoteQueryableRepository(IServiceProvider provider)
        {
            _queries = provider.GetServices<IQuoteQuery>();
        }
        public Q Query<Q>() where Q : IQuoteQuery
        {
            var query = _queries.OfType<Q>().FirstOrDefault();

            if (query != null)
                return query;

            throw new NotImplementedException("Query not implemented in the repository.");
        }
    }
}
