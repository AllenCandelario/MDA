using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDA.Implementation;


namespace MDA.App.Workers
{
    public sealed class IBClientAccountWorker : BackgroundService
    {
        private readonly IBClient _ib;

        public IBClientAccountWorker(IBClient ib)
        {
            _ib = ib;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            /* Race condition between IBClientWorker and IBClientAccount worker may result in the subscription happening before the connection and account ids are set. 
               Temporarily introduce a dirty thread blocking mechanism to pause this for 1s to ensure the IBClientWorker connection and details are set before subscribing to account updates
            */
            Thread.Sleep(1000); 

            _ib.SubscribeToAccountUpdates(true);
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _ib.SubscribeToAccountUpdates(false);
            return base.StopAsync(cancellationToken);
        }

    }
}
