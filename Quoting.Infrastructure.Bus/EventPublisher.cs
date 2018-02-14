using Quoting.Infrastructure.Bus.Contracts;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoting.Infrastructure.Bus
{
    public class EventPublisher : IPublisher
    {
        private readonly IBusClient _bus;

        public EventPublisher(IBusClient bus)
            => _bus = bus;

        public async Task PublishAsync(IEvent @event)
            => await _bus.PublishAsync(@event);

        public async Task PublishAsync(IEnumerable<IEvent> events)
            => await Task.WhenAll(events.Select(async e => await PublishAsync(e)));
    }
}
