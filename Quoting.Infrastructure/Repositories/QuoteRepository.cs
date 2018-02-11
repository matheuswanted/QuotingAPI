using Quoting.Domain.Models;
using Quoting.Domain.Repositories;
using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
