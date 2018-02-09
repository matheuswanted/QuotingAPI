using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.Seedworking
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
    }
}
