using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quoting.Infrastructure.Bus.Contracts
{
    public interface IPublisher
    {
        Task PublishAsync(IEvent @event);
        Task PublishAsync(IEnumerable<IEvent> events);
    }
}
