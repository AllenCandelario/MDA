using Microsoft.Extensions.Hosting;
using MDA.Implementation;
using Microsoft.Extensions.Logging;
using MDA.App.Log;


namespace MDA.App.Workers.Subscribers
{
    public sealed class IBAccountWorker : BackgroundService
    {
        private readonly IBClient _ib;

        private readonly ILogger<IBAccountWorker> _logger;

        public IBAccountWorker(IBClient ib, ILogger<IBAccountWorker> logger)
        {
            _ib = ib;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            int retries = 0, maxRetries = 5;

            while (!_ib._accountUpdateSubscriptionReady && !ct.IsCancellationRequested && retries < maxRetries)
            {
                MDALog.AccountUpdateWaitingForReadiness(_logger, retries);
                await Task.Delay(500);
                retries++;
            }

            if (_ib._accountUpdateSubscriptionReady)
            {
                _ib.SubscribeToAccountUpdates(true);
                MDALog.AccountUpdateSubscribe(_logger);
            }
            else
            {
                MDALog.AccountUpdateNotReadyAfterRetries(_logger);
            }
        }

        public override Task StopAsync(CancellationToken ct)
        {
            _ib.SubscribeToAccountUpdates(false);
            MDALog.AccountUpdateUnsubscribe(_logger);
            return base.StopAsync(ct);
        }
    }
}
