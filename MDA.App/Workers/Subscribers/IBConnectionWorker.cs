using MDA.Implementation;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MDA.App.Log;

namespace MDA.App.Workers.Subscribers
{
    // Handles connections only 
    public sealed class IBConnectionWorker : BackgroundService
    {
        private readonly IBClient _ib;

        private readonly ILogger<IBConnectionWorker> _logger;

        public IBConnectionWorker(IBClient ib, ILogger<IBConnectionWorker> logger)
        {
            _ib = ib;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ib.InitiateConnection();
            MDALog.IBKRConnected(_logger);
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _ib.Dispose();
            MDALog.IBKRDisconnected(_logger);
            return base.StopAsync(cancellationToken);
        }
    }
}
