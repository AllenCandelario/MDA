using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDA.Implementation;
using MDA.Model;
using System.Security.Principal;


namespace MDA.App.Workers
{
    public sealed class IBAccountWorker : BackgroundService
    {
        private readonly IBClient _ib;

        public IBAccountWorker(IBClient ib)
        {
            _ib = ib;
        }

        protected override Task ExecuteAsync(CancellationToken ct)
        {
            int retries = 0, maxRetries = 5;

            while (!_ib._accountUpdateSubscriptionReady
                   && !ct.IsCancellationRequested
                   && retries < maxRetries)
            {
                Console.WriteLine("[Acct] Waiting for IB readiness...");
                Thread.Sleep(500);
                retries++;
            }

            if (_ib._accountUpdateSubscriptionReady)
            {
                _ib.SubscribeToAccountUpdates(true);
                Console.WriteLine("[Acct] Subscribed to account updates.");
            }
            else
            {
                Console.WriteLine("[WARN] IB not ready after retries. Skipping subscription.");
            }

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken ct)
        {
            _ib.SubscribeToAccountUpdates(false);
            return base.StopAsync(ct);
        }
    }
}
