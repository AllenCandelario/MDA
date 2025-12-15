using MDA.App.Log;
using MDA.Implementation;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDA.App.Workers.Subscribers
{
    public sealed class IBMarketDataWorker : BackgroundService
    {
        private readonly IBClient _ib;

        private readonly ILogger<IBMarketDataWorker> _logger;

        public IBMarketDataWorker(IBClient ib, ILogger<IBMarketDataWorker> logger)
        {
            _ib = ib;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            _ib.SubscribeToDelayedMarketDataType();
            MDALog.MarketDataSubscribeDelayed(_logger);
            // actual subscription is reliant on account update portfolio data. It'll be handled in an event handler from account updates
        }

        public override Task StopAsync(CancellationToken ct)
        {
            _ib.CancelAllMarketData();
            MDALog.MarketDataUnsubscribeAll(_logger);
            return base.StopAsync(ct);
        }
    }
}
