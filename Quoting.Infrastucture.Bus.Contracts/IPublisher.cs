using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quoting.Infrastructure.Bus.Contracts
{
    public interface IPublisher
    {
        Task Publish(IEvent @event);
        Task Publish(IEnumerable<IEvent> events);
    }
}
