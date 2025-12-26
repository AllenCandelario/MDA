using MDA.Web.Domain.Accounts;
using MDA.Web.Application.Shared;

namespace MDA.Web.Application.Accounts.Contracts
{
    public sealed record AccountSummaryResponse(
        string IbkrAccountId,
        string BaseCurrency,
        string AccountType,
        DateTime? LastUpdatedUtc,
        decimal? TotalPortfolioValue,
        decimal? SettledCash,
        decimal? ExcessLiquidity,
        decimal? BuyingPower,
        decimal? InvestedAsset,
        decimal? DailyPnl,
        decimal? UnrealizedPnl
    )
    {
        public static AccountSummaryResponse From(Account account)
        {
            decimal? investedAssets = null;

            if (account.TotalPortfolioValue.HasValue && account.SettledCash.HasValue)
            {
                investedAssets = account.TotalPortfolioValue.Value - account.SettledCash.Value;
            }

            return new AccountSummaryResponse(
                account.IbkrAccountId,
                account.BaseCurrency,
                account.AccountType,
                account.LastUpdatedUtc,
                SharedLogic.R2(account.TotalPortfolioValue),
                SharedLogic.R2(account.SettledCash),
                SharedLogic.R2(account.ExcessLiquidity),
                SharedLogic.R2(account.BuyingPower),
                SharedLogic.R2(investedAssets),
                SharedLogic.R2(account.DailyPnl),
                SharedLogic.R2(account.UnrealizedPnl)
            );
        }
    }
}
