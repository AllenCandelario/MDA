using MDA.Implementation;
using MDA.Model;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
