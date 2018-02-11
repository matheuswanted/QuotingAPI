using Microsoft.EntityFrameworkCore;
using Quoting.Domain.Models;
using Quoting.Domain.Queries;
using Quoting.Domain.Seedworking;
using System.Linq;
using System.Threading.Tasks;

namespace Quoting.Infrastructure.Queries
{
    public class QuoteStatusRequestQuery : IQuoteStatusRequestQuery
    {
        private readonly QuotingDbContext _context;

        public QuoteStatusRequestQuery(IUnitOfWork unitOfWork)
        {
            _context = unitOfWork.Context() as QuotingDbContext;
        }
        public Task<Quote> Run(int request)
        {
            return _context.Quotes
                .Where(q => q.Id == request)
                .FirstOrDefaultAsync();
        }
    }
}
