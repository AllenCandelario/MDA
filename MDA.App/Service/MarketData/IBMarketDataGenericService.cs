using MDA.Model;
using System.Text.Json;

namespace MDA.App.Service.MarketData
{
    public sealed class IBMarketDataGenericService : IMarketDataHandler
    {
        public IBMarketDataGenericService() { }

        public Task HandleAsync(IBMarketData marketData, CancellationToken cancellationToken)
        {
            string key;
            string value = JsonSerializer.Serialize(marketData, marketData.GetType());

            switch (marketData)
            {
                case IBTickPrice:
                    key = "IBTickPrice";
                    break;
                case IBTickSize:
                    key = "IBTickSize";
                    break;
                case IBTickReqParams:
                    key = "IBTickReqParams";
                    break;
                case IBTickString:
                    key = "IBTickString";
                    break;
                case IBTickEFP:
                    key = "IBTickEFP";
                    break;
                case IBTickGeneric:
                    key = "IBTickGeneric";
                    break;
                case IBTickOptionComputation:
                    key = "IBTickOptionComputation";
                    break;
                case IBTickSnapshotEnd:
                    key = "IBTickSnapshotEnd";
                    break;
                default:
                    key = "Unknown";
                    break;
                 
            }
            Console.WriteLine($"[IBMarketData] Key = {key}, Value = {value}");

            return Task.CompletedTask;
        }
    }
}
