using Quoting.Domain.Seedworking;
using Quoting.Infrastructure.Bus.Contracts;

namespace Quoting.Domain.Models.Events
{
    public class QuoteRequestedEvent : IEvent
    {
        public int QuoteId { get; set; }
    }
}
