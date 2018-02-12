using Microsoft.Extensions.DependencyInjection;
using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quoting.Infrastructure.Repositories.Queryable
{
    public abstract class QueryableRepositoryBase<T> : IQueryableRepository<T>
    {
        private readonly IEnumerable<T> _queries;
        public QueryableRepositoryBase(IServiceProvider provider)
        {
            _queries = provider.GetServices<T>();
        }
        public Q Query<Q>() where Q : T
        {
            var query = _queries.OfType<Q>().FirstOrDefault();

            if (query != null)
                return query;

            throw new NotImplementedException("Query not implemented in the repository.");
        }
    }
}
