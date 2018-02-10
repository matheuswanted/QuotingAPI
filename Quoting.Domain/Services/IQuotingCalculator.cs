using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quoting.Domain.Models;

namespace Quoting.Domain.Services
{
    public interface IQuotingCalculator
    {
        Task<decimal> CalculateModifier(Customer customer);
        Task<decimal> CalculateBasePrice(Vehicle vehicle);
        decimal DefaultBasePrice();
    }
}
