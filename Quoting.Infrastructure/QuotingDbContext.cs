using Microsoft.EntityFrameworkCore;
using Quoting.Domain.Models;
using Quoting.Domain.Seedworking;
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
        public QuotingDbContext(DbContextOptions<QuotingDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerDbConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleDbConfiguration());
        }
    }
}
