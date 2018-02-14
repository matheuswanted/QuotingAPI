using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quoting.Domain.ValueObjects;

namespace Quoting.Infrastructure.EntityConfig
{
    public class QuoteRequestDbConfiguration : IEntityTypeConfiguration<QuoteRequest>
    {
        public void Configure(EntityTypeBuilder<QuoteRequest> builder)
        {
            builder.ToTable("QuoteRequests");
            builder.Property<int>("Id").ForSqlServerUseSequenceHiLo("seq_quote_requests");
            builder.HasKey("Id");

            builder.OwnsOne(req => req.Customer, c =>
            {
                c.Property(cs => cs.Address);
                c.Property(cs => cs.BirthDate);
                c.Property(cs => cs.Email);
                c.Property(cs => cs.Gender);
                c.Property(cs => cs.Phone);
                c.Property(cs => cs.SSN);
            });
            builder.OwnsOne(req => req.Vehicle, v => {
                v.Property(ve => ve.Make);
                v.Property(ve => ve.ManufacturingYear);
                v.Property(ve => ve.Model);
                v.Property(ve => ve.Type);
            });
        }
    }
}
