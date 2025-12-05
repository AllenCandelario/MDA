using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MDA.Web.Domain.Instruments;

namespace MDA.Web.Infrastructure.Persistence.Configurations
{
    public sealed class InstrumentConfiguration : IEntityTypeConfiguration<Instrument>
    {
        public void Configure(EntityTypeBuilder<Instrument> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Symbol)
                   .IsRequired()
                   .HasMaxLength(32);

            builder.Property(i => i.Name)
                   .IsRequired()
                   .HasMaxLength(128);

            builder.Property(i => i.AssetClass)
                   .IsRequired()
                   .HasMaxLength(32);

            builder.HasIndex(i => i.IbkrConId)
                   .IsUnique();
        }
    }
}
