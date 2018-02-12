using Microsoft.EntityFrameworkCore;
using Quoting.Domain.Queries;
using Quoting.Domain.Seedworking;
using Quoting.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quoting.Infrastructure.Queries
{
    public class PriceModifierRulesAppliableToCustomerQuery : IPriceModifierRulesAppliableToCustomerQuery
    {
        private readonly QuotingDbContext _context;

        public PriceModifierRulesAppliableToCustomerQuery(IUnitOfWork unitOfWork)
        {
            _context = unitOfWork.Context() as QuotingDbContext;
        }
        public async Task<IEnumerable<PriceModifierRule>> Run()
            => await _context.PriceModifierRules
                .ToListAsync();
    }
}
