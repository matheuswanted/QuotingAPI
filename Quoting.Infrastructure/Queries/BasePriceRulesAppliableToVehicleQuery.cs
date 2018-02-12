using Microsoft.EntityFrameworkCore;
using Quoting.Domain.Queries;
using Quoting.Domain.Queries.Requests;
using Quoting.Domain.Seedworking;
using Quoting.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoting.Infrastructure.Queries
{
    public class BasePriceRulesAppliableToVehicleQuery : IBasePriceRulesAppliableToVehicleQuery
    {
        private readonly QuotingDbContext _context;

        public BasePriceRulesAppliableToVehicleQuery(IUnitOfWork unitOfWork)
        {
            _context = unitOfWork.Context() as QuotingDbContext;
        }
        public async Task<IEnumerable<BasePriceRule>> Run(ByTypeAndYearOptional request)
            => await _context.BasePriceRules
                .Where(r => (r.Year == null || r.Year == request.Year) && r.Type == request.Type)
                .ToListAsync();
    }
}
