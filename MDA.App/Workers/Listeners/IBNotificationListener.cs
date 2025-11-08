using MDA.App.Log;
using MDA.App.Service.Notification;
using MDA.Implementation;
using MDA.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace MDA.App.Workers.Listeners
{
    public sealed class IBNotificationListener : BackgroundService
    {
        private readonly IBClient _ib;
        private readonly IEnumerable<INotificationHandler> _handlers;
        private CancellationToken _ct;

        private readonly ILogger<IBNotificationListener> _logger;

        public IBNotificationListener(IBClient ib, IEnumerable<INotificationHandler> handlers, ILogger<IBNotificationListener> logger)
        {
            _ib = ib;
            _handlers = handlers;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _ct = cancellationToken;
            _ib.NotificationReceived += OnNotification;
            MDALog.NotificationListenerAttached(_logger);
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _ib.NotificationReceived -= OnNotification;
            MDALog.NotificationListenerDetached(_logger);
            return base.StopAsync(cancellationToken);
        }

        public void OnNotification(IBNotification notification)
        {
            foreach (var handler in _handlers)
            {
                _ = RunHandler(handler, notification, _ct);
            }
        }

        private async Task RunHandler(INotificationHandler handler, IBNotification notification, CancellationToken ct)
        {
            try
            {
                await handler.HandleAsync(notification, ct);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { }
            catch (Exception ex)
            {
                MDALog.HandlerUnhandledException(_logger, ex, handler.GetType().Name);
            }

        }
    }
}
