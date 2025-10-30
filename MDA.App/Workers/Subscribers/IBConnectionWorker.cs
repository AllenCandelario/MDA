using MDA.Implementation;
using Microsoft.Extensions.Hosting;

namespace MDA.App.Workers.Subscribers
{
    // Handles connections only 
    public sealed class IBConnectionWorker : BackgroundService
    {
        private readonly IBClient _ib;

        public IBConnectionWorker(IBClient ib)
        {
            _ib = ib;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ib.InitiateConnection();
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _ib.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}
