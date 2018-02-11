using Quoting.Domain.Queries;
using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.Repositories.Queryable
{
    public interface IQuoteQueryableRepository 
        : IQueryableRepository<IQuoteQuery>
    {
    }
}
