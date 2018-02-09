using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quoting.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private QuotingDbContext _context;

        public UnitOfWork(QuotingDbContext context)
        {
            _context = context;
        }

        public IDbContext Context()
        {
            return _context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveChangesAsync();
        }
    }
}
