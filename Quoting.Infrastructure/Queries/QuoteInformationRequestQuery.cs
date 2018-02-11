using Microsoft.EntityFrameworkCore;
using Quoting.Domain.Models;
using Quoting.Domain.Queries;
using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoting.Infrastructure.Queries
{
    public class QuoteInformationRequestQuery : IQuoteInformationRequestQuery
    {
        private readonly QuotingDbContext _context;

        public QuoteInformationRequestQuery(IUnitOfWork unitOfWork)
        {
            _context = unitOfWork.Context() as QuotingDbContext;
        }
        public Task<Quote> Run(int request)
        {
            return _context.Quotes
                .Include(q => q.Customer)
                .Include(q => q.Vehicle)
                .Where(q => q.Id == request)
                .FirstOrDefaultAsync();
        }
    }
}
