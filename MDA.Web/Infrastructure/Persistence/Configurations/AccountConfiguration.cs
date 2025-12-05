using MDA.Web.Domain.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MDA.Web.Infrastructure.Persistence.Configurations
{
    public sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public static readonly Guid DefaultUserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        public static readonly Guid DefaultAccountId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.IbkrAccountId)
                   .IsRequired()
                   .HasMaxLength(32);

            builder.Property(a => a.BaseCurrency)
                   .IsRequired()
                   .HasMaxLength(8);

            builder.Property(a => a.AccountType)
                   .IsRequired()
                   .HasMaxLength(32);

            // Dev default account
            builder.HasData(new
            {
                Id = DefaultAccountId,
                UserId = DefaultUserId,
                IbkrAccountId = "U8515462",
                BaseCurrency = "USD",
                AccountType = "INDIVIDUAL",
                LastUpdatedUtc = (DateTime?)null,
                TotalPortfolioValue = (decimal?)null,
                SettledCash = (decimal?)null,
                ExcessLiquidity = (decimal?)null,
                BuyingPower = (decimal?)null,
                DailyPnl = (decimal?)null,
                UnrealizedPnl = (decimal?)null,
                ExpectedDividendsYear = (decimal?)null,
                DividendsPaidYtd = (decimal?)null
            });
        }
    }
}
