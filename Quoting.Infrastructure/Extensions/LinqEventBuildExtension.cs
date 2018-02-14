using Quoting.Infrastructure.Bus.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Infrastructure.Extensions
{
    public static class LinqEventBuildExtension
    {
        public static IEnumerable<IEvent> Build(this IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                if (@event is IEventBuilder)
                    yield return (@event as IEventBuilder).Build();
                else
                    yield return @event;
            }
        }
    }
}
