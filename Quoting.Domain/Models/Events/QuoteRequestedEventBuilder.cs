using Quoting.Infrastructure.Bus.Contracts;

namespace Quoting.Domain.Models.Events
{
    public class QuoteRequestedEventBuilder : IEventBuilder
    {
        private readonly Quote _quote;

        private QuoteRequestedEventBuilder() { }
        public QuoteRequestedEventBuilder(Quote quote)
        {
            _quote = quote;
        }
        public IEvent Build()
            => new QuoteRequestedEvent { QuoteId = _quote.Id };
    }
}
