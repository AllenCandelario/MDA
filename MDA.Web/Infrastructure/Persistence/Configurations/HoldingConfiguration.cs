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

            builder.Property(h => h.Quantity)
                   .HasColumnType("numeric(18,4)");

            builder.Property(h => h.AveragePrice)
                   .HasColumnType("numeric(18,4)");

            builder.Property(h => h.UnrealizedPNL)
                   .HasColumnType("numeric(18,4)");

            builder.Property(h => h.RealizedPNL)
                   .HasColumnType("numeric(18,4)");

            builder.HasOne(h => h.Account)
                   .WithMany(a => a.Holdings)
                   .HasForeignKey(h => h.AccountId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(h => h.Instrument)
                   .WithMany(i => i.Holdings)
                   .HasForeignKey(h => h.InstrumentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(h => h.Category)
                   .WithMany(c => c.Holdings)
                   .HasForeignKey(h => h.CategoryId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
