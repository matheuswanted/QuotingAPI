using Quoting.Domain.Models;
using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quoting.Domain.Repositories
{
    public interface IQuoteRepository : IRepository<Quote>
    {
        Task<Quote> FindById(int id);
        void Update(Quote quote);
    }
}
