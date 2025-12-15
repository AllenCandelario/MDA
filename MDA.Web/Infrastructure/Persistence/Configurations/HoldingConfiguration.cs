using MDA.Web.Domain.Holdings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MDA.Web.Infrastructure.Persistence.Configurations
{
    public sealed class HoldingConfiguration : IEntityTypeConfiguration<Holding>
    {
        public void Configure(EntityTypeBuilder<Holding> builder)
        {
            builder.HasKey(h => h.Id);

            // TODO: Consider adding index fr AccountId and InstrumentId

            builder.HasOne(h => h.Account)
                   .WithMany(a => a.Holdings)
                   .HasForeignKey(h => h.AccountId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(h => h.Instrument)
                   .WithMany(i => i.Holdings)
                   .HasForeignKey(h => h.InstrumentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.Category)
                   .WithMany(c => c.Holdings)
                   .HasForeignKey(h => h.CategoryId)
                   .OnDelete(DeleteBehavior.SetNull);

            /* For the following quantity / money-related properties, their values comes can hit up to 8 decimal places */
            builder.Property(h => h.Quantity)
                   .HasPrecision(18, 8);

            builder.Property(h => h.AveragePrice)
                   .HasPrecision(18, 8);

            builder.Property(h => h.UnrealizedPNL)
                   .HasPrecision(18, 8);

            builder.Property(h => h.RealizedPNL)
                   .HasPrecision(18, 8);
        }
    }
}
