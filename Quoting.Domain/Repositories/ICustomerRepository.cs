using Quoting.Domain.Models;
using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quoting.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetBySSN(string SSN);
    }
}
