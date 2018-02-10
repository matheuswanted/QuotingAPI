using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quoting.Domain.Models;

namespace Quoting.Infrastructure.EntityConfig
{
    public class VehicleDbConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicles");
            builder.HasKey(v => v.Id);
            builder.Ignore(v => v.ModelInconsistecies);
            builder.Property(v => v.Id).ForSqlServerUseSequenceHiLo("seq_vehicles");

            builder.Property(v => v.Make).IsRequired();
            builder.Property(v => v.ManufacturingYear).IsRequired();
            builder.Property(v => v.Model).IsRequired();
            builder.Property(v => v.Type).IsRequired();
            builder.Property("CustomerId").IsRequired();
        }
    }
}
