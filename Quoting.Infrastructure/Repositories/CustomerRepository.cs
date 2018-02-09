using Microsoft.EntityFrameworkCore;
using Quoting.Domain.Models;
using Quoting.Domain.Repositories;
using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quoting.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private QuotingDbContext _context;

        public CustomerRepository(IUnitOfWork context)
        {
            _context = context.Context() as QuotingDbContext ?? throw new ArgumentException($"Expected context {typeof(QuotingDbContext).Name}");
        }
        public void Add(Customer entity)
        {
            _context.Attach<Customer>(entity);
        }

        public async Task<Customer> GetBySSN(string SSN)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.SSN == SSN);
        }

        public void Update(Customer entity)
        {
            _context.Attach<Customer>(entity);
        }
    }
}
