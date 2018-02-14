using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Infrastructure.Bus.Contracts
{
    public interface IEventManager
    {
        void Subscribe<TEvent, THandler>() where TEvent : IEvent where THandler : IEventHandler<TEvent>;
    }
}
