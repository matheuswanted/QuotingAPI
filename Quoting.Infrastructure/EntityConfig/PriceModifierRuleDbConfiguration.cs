using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quoting.Domain.ValueObjects;

namespace Quoting.Infrastructure.EntityConfig
{
    public class PriceModifierRuleDbConfiguration : IEntityTypeConfiguration<PriceModifierRule>
    {
        public void Configure(EntityTypeBuilder<PriceModifierRule> builder)
        {
            builder.ToTable("PriceModifierRules");
            builder.Property<int>("Id").ForSqlServerUseSequenceHiLo("seq_price_modifier_rules");
            builder.HasKey("Id");

            builder.Property(r => r.Gender);
            builder.Property(r => r.Modifier);
            builder.OwnsOne(r => r.AgeRange, a =>
            {
                a.Property(p => p.Start);
                a.Property(p => p.End);
            });

        }
    }
}
