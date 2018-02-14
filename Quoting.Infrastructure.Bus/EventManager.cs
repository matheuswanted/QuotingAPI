using Quoting.Infrastructure.Bus.Contracts;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Infrastructure.Bus
{
    public class EventManager : IEventManager
    {
        private readonly IBusClient _bus;
        private readonly IServiceProvider _provider;

        public EventManager(IBusClient bus, IServiceProvider provider)
        {
            _bus = bus;
            _provider = provider;
        }

        public void Subscribe<TEvent, THandler>()
            where TEvent : IEvent
            where THandler : IEventHandler<TEvent>
            => _bus.SubscribeAsync<TEvent>(e => (_provider.GetService(typeof(THandler)) as IEventHandler<TEvent>).Handle(e)).Wait();
    }
}
