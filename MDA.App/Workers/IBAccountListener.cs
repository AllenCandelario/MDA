using MDA.App.Service;
using MDA.Implementation;
using MDA.Model;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDA.App.Workers
{
    public sealed class IBAccountListener : BackgroundService
    {
        private readonly IBClient _ib;
        private readonly IBAccountService _ibAccountService;

        public IBAccountListener(IBClient ib, IBAccountService ibAccountService)
        {
            _ib = ib;
            _ibAccountService = ibAccountService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ib.AccountUpdateReceived += OnAccountUpdate;
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _ib.AccountUpdateReceived -= OnAccountUpdate;
            return base.StopAsync(cancellationToken);
        }

        private void OnAccountUpdate(IBAccountUpdate accountUpdate)
        {
            _ibAccountService.HandleAccountUpdate(accountUpdate);
        }
    }
}
