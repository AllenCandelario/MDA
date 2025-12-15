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

            /* Unsure of format, set to 32 max length for now */
            builder.Property(i => i.Symbol)
                .IsRequired()
                .HasMaxLength(32);

            /* Unsure of format, set to 32 max length for now */
            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.HasIndex(i => i.IbkrConId)
                .IsUnique();

            /* STK – stock (or ETF) 
             * OPT – option 
             * FUT – future 
             * IND – index 
             * FOP – futures option 
             * CASH – forex pair 
             * BAG – combo 
             * WAR – warrant 
             * BOND- bond 
             * CMDTY- commodity 
             * NEWS- news 
             * FUND- mutual fund.
             * Set to 6 max length in case
             */
            builder.Property(i => i.AssetClass)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(i => i.LastPrice)
                .HasPrecision(18, 8);

            builder.Property(i => i.Pe)
                .HasPrecision(18, 8);

            builder.Property(i => i.ForwardPe)
                .HasPrecision(18, 8);

            builder.Property(i => i.Week52High)
                .HasPrecision(18, 8);

            builder.Property(i => i.Ath)
                .HasPrecision(18, 8);
        }
    }
}
