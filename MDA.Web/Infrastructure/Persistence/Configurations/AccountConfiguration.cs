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

            /* Format is generally a 8 alphanumeric character i.e. starting with I or U followed by 7 characters.
             * Can't find reputable sources for this, so we're leaving it as 32 for now
             */
            builder.Property(a => a.IbkrAccountId)
                   .IsRequired()
                   .HasMaxLength(32);

            /* Supposedly follows ISO 4217 which denotes strict 3 character count for currencies.
             * We set as 4 because IBKR uses tokens like "BASE"
             */
            builder.Property(a => a.BaseCurrency)
                   .IsRequired()
                   .HasMaxLength(4);

            /* Unsure of format, set to 32 max length for now */
            builder.Property(a => a.AccountType)
                   .IsRequired()
                   .HasMaxLength(32);

            /* For the following quantity / money-related properties, their values comes can hit up to 8 decimal places */
            builder.Property(a => a.TotalPortfolioValue)
                .HasPrecision(18, 8);

            builder.Property(a => a.SettledCash)
                .HasPrecision(18, 8);

            builder.Property(a => a.ExcessLiquidity)
                .HasPrecision(18, 8);

            builder.Property(a => a.BuyingPower)
                .HasPrecision(18, 8);

            builder.Property(a => a.DailyPnl)
                .HasPrecision(18, 8);

            builder.Property(a => a.UnrealizedPnl)
                .HasPrecision(18, 8);

            builder.Property(a => a.ExpectedDividendsYear)
                .HasPrecision(18, 8);

            builder.Property(a => a.DividendsPaidYtd)
                .HasPrecision(18, 8);

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
