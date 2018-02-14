using Microsoft.EntityFrameworkCore;
using Quoting.Domain.Models;
using Quoting.Domain.Seedworking;
using Quoting.Domain.ValueObjects;
using Quoting.Infrastructure.EntityConfig;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Infrastructure
{
    public class QuotingDbContext : DbContext, IDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteRequest> QuoteRequests { get; set; }
        public DbSet<BasePriceRule> BasePriceRules { get; set; }
        public DbSet<PriceModifierRule> PriceModifierRules { get; set; }
        public QuotingDbContext(DbContextOptions<QuotingDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerDbConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleDbConfiguration());
            modelBuilder.ApplyConfiguration(new BasePriceRuleDbConfiguration());
            modelBuilder.ApplyConfiguration(new PriceModifierRuleDbConfiguration());
            modelBuilder.ApplyConfiguration(new QuoteRequestDbConfiguration());
        }
    }
}
