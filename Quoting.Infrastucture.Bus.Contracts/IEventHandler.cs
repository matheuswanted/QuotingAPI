using System.Threading;
using System.Threading.Tasks;

namespace Quoting.Infrastructure.Bus.Contracts
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        Task Handle(TEvent @event);

    }
}
