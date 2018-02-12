using Quoting.Domain.Models;
using Quoting.Domain.Repositories;
using Quoting.Domain.Seedworking;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Quoting.Infrastructure.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly QuotingDbContext _context;

        public QuoteRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = _unitOfWork.Context() as QuotingDbContext ?? throw new ArgumentException($"Expected context {typeof(QuotingDbContext).Name}");
        }
        public void Add(Quote entity)
        {
            _context.Add(entity);
        }

        public async Task<Quote> FindById(int id)
        {
            return await _context.Quotes
                .Include(q => q.Customer)
                .Include(q => q.Vehicle)
                .Where(q => q.Id == id)
                .FirstOrDefaultAsync();
        }

        public void Update(Quote quote)
        {
            if (_context.Entry(quote).State == EntityState.Detached)
                _context.Update(quote);
        }
    }
}
