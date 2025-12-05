using System.Diagnostics.Contracts;

namespace MDA.Web.Application.Accounts.Contracts
{
    // updateAccountValue 
    public sealed record IBUpdateAccountValue(string Key, string Value, string Currency, string AccountName);

    // updatePortfolio
    public sealed record IBUpdatePortfolio(IBContract Contract, decimal Position, double MarketPrice, double MarketValue,
            double AverageCost, double UnrealizedPNL, double RealizedPNL, string AccountName);

    public sealed record IBContract(int ConId, string Symbol, string SecType, string Exchange, string Currency, string LocalSymbol, string PrimaryExch, string Description);

}
