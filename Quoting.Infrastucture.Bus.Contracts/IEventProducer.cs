
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Infrastructure.Bus.Contracts
{
    public interface IEventProducer
    {
        IReadOnlyCollection<IEvent> Events { get; }
        void Raise(IEvent @event);
    }
}
