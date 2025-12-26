using MDA.Web.Application.Shared;
using MDA.Web.Domain.Holdings;
using MDA.Web.Domain.Accounts;

namespace MDA.Web.Application.Holdings.Contracts
{
    public sealed record HoldingsResponse(IReadOnlyList<HoldingRow> Holdings)
    {
        public static HoldingsResponse From (IReadOnlyList<Holding> fullHoldingDetails, decimal? totalPortfolioValue)
        {
            decimal portfolioValue = (totalPortfolioValue.HasValue && totalPortfolioValue.Value > 0m) ? totalPortfolioValue.Value: 0m; // make it 0 instead of null because you need a number to compute marketValuePct below

            var rows = new List<HoldingRow>(fullHoldingDetails.Count);

            foreach (var holding in fullHoldingDetails)
            {
                rows.Add(MapRow(holding, portfolioValue));
            }

            return new HoldingsResponse(rows);
        }

        private static HoldingRow MapRow(Holding holding, decimal portfolioValue)
        {

            // Instrument related 
            var instrument = holding.Instrument;

            // Category related
            var category = holding.Category;
            var categoryName = holding.Category != null ? holding.Category.Name : "Uncategorized";

            // Account related
            var account = holding.Account;

            // Holding and calculations 
            decimal lastPrice = instrument.LastPrice ?? 0m; // make it 0 instead of null because you need a number to compute marketValue below

            decimal? changeAbs = null;
            decimal? changePercent = null;

            decimal costBasis = holding.Quantity * holding.AveragePrice;
            decimal marketValue = holding.Quantity * lastPrice;
            decimal marketValuePct = portfolioValue > 0m ? (marketValue / portfolioValue) * 100m : 0m;

            decimal unrealizedAbs = holding.UnrealizedPNL;
            decimal unrealizedPct = costBasis > 0m ? (unrealizedAbs / costBasis) * 100m : 0m;

            return new HoldingRow(
                Symbol: instrument.Symbol,
                Name: instrument.Name,
                Currency: account.BaseCurrency,
                Category: categoryName,
                LastPrice: SharedLogic.R2(instrument.LastPrice),
                ChangeAbs: SharedLogic.R2(changeAbs),
                ChangePct: SharedLogic.R2(changePercent),
                Week52High: SharedLogic.R2(instrument.Week52High),
                AllTimeHigh: SharedLogic.R2(instrument.Ath),
                Pe: SharedLogic.R2(instrument.Pe),
                FwdPe: SharedLogic.R2(instrument.ForwardPe),
                Quantity: SharedLogic.R(holding.Quantity),
                AvgPrice: SharedLogic.R2(holding.AveragePrice) ?? 0m,
                CostBasis: SharedLogic.R2(costBasis) ?? 0m,
                MarketValue: SharedLogic.R2(marketValue) ?? 0m,
                MarketValuePctOfAssets: SharedLogic.R2(marketValuePct) ?? 0m,
                UnrealizedAbs: SharedLogic.R2(unrealizedAbs) ?? 0m,
                UnrealizedPct: SharedLogic.R2(unrealizedPct) ?? 0m,
                Notes: holding.Notes,
                Rating: holding.Rating
            );
        }
    }

    public sealed record HoldingRow(
        string Symbol,
        string Name,
        string Currency,
        string Category,
        decimal? LastPrice,
        decimal? ChangeAbs,
        decimal? ChangePct,
        decimal? Week52High,
        decimal? AllTimeHigh,
        decimal? Pe,
        decimal? FwdPe,

        decimal Quantity,
        decimal AvgPrice,
        decimal CostBasis,
        decimal MarketValue,
        decimal MarketValuePctOfAssets,
        decimal UnrealizedAbs,
        decimal UnrealizedPct,

        string? Notes,
        int? Rating
    );
}
