using MDA.Model;

namespace MDA.App.Service.MarketData
{
    public interface IMarketDataHandler
    {
        Task HandleAsync(IBMarketData marketData, CancellationToken cancellationToken);
    }
}
