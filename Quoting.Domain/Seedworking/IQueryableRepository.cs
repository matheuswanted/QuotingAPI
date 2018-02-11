using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.Seedworking
{
    public interface IQueryableRepository<T>
    {
        Q Query<Q>() where Q : T;
    }
}
