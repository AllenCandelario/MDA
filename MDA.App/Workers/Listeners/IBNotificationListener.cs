using MDA.App.Service.Notification;
using MDA.Implementation;
using MDA.Model;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDA.App.Workers.Listeners
{
    public sealed class IBNotificationListener : BackgroundService
    {
        private readonly IBClient _ib;
        private readonly IEnumerable<INotificationHandler> _handlers;
        private CancellationToken _ct;

        public IBNotificationListener(IBClient ib, IEnumerable<INotificationHandler> handlers)
        {
            _ib = ib;
            _handlers = handlers;
            
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _ct = cancellationToken;
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
            foreach (var handler in _handlers)
            {
                _ = RunHandler(handler, notification, _ct);
            }
        }

        private static async Task RunHandler(INotificationHandler handler, IBNotification notification, CancellationToken ct)
        {
            try
            {
                await handler.HandleAsync(notification, ct);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[{handler.GetType().Name}] {ex}");
            }

        }
    }
}
