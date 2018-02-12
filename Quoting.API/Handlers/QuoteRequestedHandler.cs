using Quoting.Domain.Models.Notifications;
using Quoting.Domain.Repositories;
using Quoting.Domain.Seedworking;
using Quoting.Domain.Services;
using Quoting.Infrastructure.Bus.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Quoting.API.Handlers
{
    public class QuoteRequestedHandler : IEventHandler<QuoteRequestedEvent>
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuotingCalculator _quotingCalculator;

        public QuoteRequestedHandler(IUnitOfWork unitOfWork, IQuoteRepository quoteRepository, IQuotingCalculator quoteingCalculator)
        {
            _quoteRepository = quoteRepository;
            _unitOfWork = unitOfWork;
            _quotingCalculator = quoteingCalculator;
        }
        public async Task Handle(QuoteRequestedEvent notification)
        {
            var quote = await _quoteRepository.FindById(notification.QuoteId);
            var modifier = _quotingCalculator.CalculateModifier(quote.Customer);
            var basePriceRule = _quotingCalculator.SelectBasePriceRuleFor(quote.Vehicle);

            quote.CalculatePriceWithRules(await basePriceRule, await modifier);

            _quoteRepository.Update(quote);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
