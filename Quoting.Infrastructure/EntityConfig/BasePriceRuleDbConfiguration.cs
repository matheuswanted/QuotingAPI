using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quoting.Domain.ValueObjects;

namespace Quoting.Infrastructure.EntityConfig
{
    public class BasePriceRuleDbConfiguration : IEntityTypeConfiguration<BasePriceRule>
    {
        public void Configure(EntityTypeBuilder<BasePriceRule> builder)
        {
            builder.ToTable("BasePriceRules");
            builder.Property<int>("Id").ForSqlServerUseSequenceHiLo("seq_base_price_rules");
            builder.HasKey("Id");

            builder.Property(r => r.BasePrice);
            builder.Property(r => r.Make);
            builder.Property(r => r.Model);
            builder.Property(r => r.Type);
            builder.Property(r => r.Year);

        }
    }
}
