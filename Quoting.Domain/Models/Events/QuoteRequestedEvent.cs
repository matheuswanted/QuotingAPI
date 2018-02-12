using Quoting.Domain.Seedworking;
using Quoting.Infrastructure.Bus.Contracts;

namespace Quoting.Domain.Models.Notifications
{
    public class QuoteRequestedEvent : IEvent
    {
        private readonly Quote _quote;

        public QuoteRequestedEvent(Quote quote)
        {
            _quote = quote;
        }
        public int QuoteId => _quote.Id;
    }
}
