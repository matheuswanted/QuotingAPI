using Microsoft.EntityFrameworkCore;
using Quoting.Domain.Seedworking;
using Quoting.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quoting.Infrastructure.Seed
{
    public class DbSeed
    {
        private readonly QuotingDbContext _context;

        public DbSeed(QuotingDbContext context)
        {
            _context = context;
        }
        public async Task Seed()
        {
            using (_context)
            {
                await _context.Database.MigrateAsync();
                if (!await _context.BasePriceRules.AnyAsync())
                    _context.BasePriceRules.AddRange(FactoryBasePriceRules());
                if (!await _context.BasePriceRules.AnyAsync())
                    _context.PriceModifierRules.AddRange(FactoryPriceModifierRules());

                await _context.SaveChangesAsync();
            }
        }
        public IEnumerable<BasePriceRule> FactoryBasePriceRules()
        {
            yield return new BasePriceRule(2008, "Car", null, null, 1800);
            yield return new BasePriceRule(1966, "Car", "Palio", "Fiat", 1400);
            yield return new BasePriceRule(1966, "Car", null, "Fiat", 2500);
            yield return new BasePriceRule(null, "Car", null, null, 3000);
        }
        public IEnumerable<PriceModifierRule> FactoryPriceModifierRules()
        {
            yield return new PriceModifierRule("M", new Range(16, 24), 1.5M);
            yield return new PriceModifierRule("M", new Range(25, 34), 1.2M);
            yield return new PriceModifierRule("M", new Range(35, 60), 1M);
            yield return new PriceModifierRule("M", new Range(61, null), 1.3M);
            yield return new PriceModifierRule("F", new Range(16, 24), 1.4M);
            yield return new PriceModifierRule("F", new Range(25, 60), 1M);
            yield return new PriceModifierRule("F", new Range(61, null), 1.2M);
        }
    }
}
