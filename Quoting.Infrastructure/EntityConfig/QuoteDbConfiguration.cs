using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quoting.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Infrastructure.EntityConfig
{
    public class QuoteDbConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            builder.ToTable("Quotes");
            builder.Ignore(q => q.ModelInconsistecies);
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Id).ForSqlServerUseSequenceHiLo("seq_quote");
            builder.Property(q => q.Value);
            builder.Property(q=>q.Status).HasField("_status");

            builder.HasOne(q => q.Customer)
                .WithOne()
                .IsRequired(true)
                .HasForeignKey(typeof(Quote), "CustomerId");

            builder.HasOne(q => q.Vehicle)
                .WithOne()
                .HasForeignKey(typeof(Quote), "VehicleId");

        }
    }
}
