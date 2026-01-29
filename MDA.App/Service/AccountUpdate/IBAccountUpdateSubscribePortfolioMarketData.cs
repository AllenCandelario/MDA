using IBApi;
using MDA.App.Log;
using MDA.Implementation;
using MDA.Model;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace MDA.App.Service.AccountUpdate
{
    public sealed class IBAccountUpdateSubscribePortfolioMarketData : IAccountUpdateHandler
    {
        private readonly IBClient _ib;
        private readonly ILogger<IBAccountUpdateSubscribePortfolioMarketData> _logger;
        //private readonly ConcurrentDictionary<int, int> _subscriptionsByConId = new(); // Used to track and prevent resubscriptions of the same contract
        public IBAccountUpdateSubscribePortfolioMarketData(IBClient ib, ILogger<IBAccountUpdateSubscribePortfolioMarketData> logger) 
        { 
            _ib = ib;   
            _logger = logger;
        }

        public Task HandleAsync(IBAccountUpdate update, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.CompletedTask;
            }

            if (update is not IBUpdatePortfolio p)
            {
                return Task.CompletedTask;
            }

            var contract = p.Contract;
            var conId = contract.ConId;

            var mdContract = new Contract
            {
                ConId = contract.ConId,
                Symbol = contract.Symbol,
                SecType = contract.SecType,
                Currency = contract.Currency,
                LocalSymbol = contract.LocalSymbol,
                TradingClass = contract.TradingClass,
                PrimaryExch = contract.PrimaryExch,
                Exchange = !string.IsNullOrEmpty(contract.Exchange)
                    ? contract.Exchange
                    : (!string.IsNullOrEmpty(contract.PrimaryExch)
                        ? contract.PrimaryExch
                        : "SMART")
            };

            /* Refer to IBKR or API docs for more info on the numbers, but they're requests for additional information
                165 = 13, 26, 52 week low and highs. Currently using it for 52 week high
                258 = Fundamental ratios. Currently using it to get p/e and forward p/e
            */
            var reqId = _ib.SubscribeToMarketData(mdContract, "165,258");

            return Task.CompletedTask;
        }
    }
}
