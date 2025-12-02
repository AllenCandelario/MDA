using MDA.App.Log;
using MDA.App.Service.MarketData;
using MDA.Implementation;
using MDA.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MDA.App.Workers.Listeners
{
    public sealed class IBMarketDataListener : BackgroundService
    {
        private readonly IBClient _ib;
        private readonly IEnumerable<IMarketDataHandler> _handlers;
        private CancellationToken _ct;

        private readonly ILogger<IBMarketDataListener> _logger;

        public IBMarketDataListener(IBClient ib, IEnumerable<IMarketDataHandler> handlers, ILogger<IBMarketDataListener> logger)
        {
            _ib = ib;
            _handlers = handlers;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _ct = cancellationToken;
            _ib.MarketDataReceived += OnMarketData;
            MDALog.MarketDataListenerAttached(_logger);
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _ib.MarketDataReceived -= OnMarketData;
            MDALog.MarketDataListenerDetached(_logger);
            return base.StopAsync(cancellationToken);
        }

        private void OnMarketData(IBMarketData marketData)
        {
            foreach (var handler in _handlers)
            {
                _ = RunHandler(handler, marketData, _ct);
            }
        }

        private async Task RunHandler(IMarketDataHandler handler, IBMarketData marketData, CancellationToken ct)
        {
            try
            {
                await handler.HandleAsync(marketData, ct);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { }
            catch (Exception ex)
            {
                MDALog.HandlerUnhandledException(_logger, ex, handler.GetType().Name);
            }
        }
    }
}
