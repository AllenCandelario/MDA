using MDA.App.Service;
using MDA.Implementation;
using MDA.Model;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDA.App.Workers
{
    public sealed class IBNotificationListener : BackgroundService
    {
        private readonly IBClient _ib;
        private readonly IBNotificationService _ibNotificationService;
        
        public IBNotificationListener(IBClient ib, IBNotificationService ibNotificationService)
        {
            _ib = ib;
            _ibNotificationService = ibNotificationService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ib.NotificationReceived += OnNotification;
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _ib.NotificationReceived -= OnNotification;
            return base.StopAsync(cancellationToken);
        }

        public void OnNotification(IBNotification notification)
        {
            _ibNotificationService.HandleNotification(notification);
        }


    }
}
