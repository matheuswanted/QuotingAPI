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
            _context.Add(entity);
        }

        public async Task<Customer> GetBySSN(string SSN)
        {
            return await _context.Customers.Include(c => c.Vehicles).FirstOrDefaultAsync(c => c.SSN == SSN);
        }

        public async Task<Customer> Put(Customer customer)
        {
            var persisted = await GetBySSN(customer.SSN);

            if (persisted == null)
                Add(customer);
            else
            {
                foreach (Vehicle v in customer.Vehicles)
                    persisted.AddVehicle(v);

                persisted.Gender = customer.Gender;
                persisted.Address = customer.Address;
                persisted.BirthDate = customer.BirthDate;
                persisted.Email = customer.Email;
                persisted.Phone = customer.Phone;
            }
            return persisted ?? customer;
        }
    }
}
