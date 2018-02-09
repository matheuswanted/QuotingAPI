﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quoting.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Infrastructure.EntityConfig
{
    public class CustomerDbConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(c => c.Id);
            builder.Ignore(c => c.ModelInconsistecies);

            builder.Property(c => c.Id).ForSqlServerUseSequenceHiLo("seq_customers");
            
            builder.Property(c => c.Phone).IsRequired();
            builder.Property(c => c.SSN).IsRequired();
            builder.Property(c => c.Address).IsRequired();
            builder.Property(c => c.BirthDate).IsRequired();
            builder.Property(c => c.Email).IsRequired();
            builder.Property(c => c.Gender).IsRequired();
            builder.Property<int?>("VehicleId").IsRequired(false);

            builder.HasOne(c => c.Vehicle)
                .WithOne()
                .IsRequired(false)
                .HasForeignKey(typeof(Customer),"VehicleId");
        }
    }
}
