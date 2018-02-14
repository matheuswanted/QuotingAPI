using Quoting.Domain.Seedworking;
using Quoting.Infrastructure.Bus.Contracts;
using Quoting.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quoting.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private QuotingDbContext _context;
        private IPublisher _publisher;

        public UnitOfWork(QuotingDbContext context, IPublisher publisher)
        {
            _context = context;
            _publisher = publisher;
        }

        public IDbContext Context()
        {
            return _context;
        }

        private async Task PublishEvents()
        {
            var producers = _context.ChangeTracker
                .Entries<IEventProducer>();

            var events = producers
                .SelectMany(e => e.Entity.Events)
                .Build();


            await _publisher.PublishAsync(events);

        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var r = await _context.SaveChangesAsync();
            await PublishEvents();
            return r;
        }
    }
}
