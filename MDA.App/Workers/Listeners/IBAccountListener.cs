using MDA.App.Log;
using MDA.App.Service.AccountUpdate;
using MDA.Implementation;
using MDA.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MDA.App.Workers.Listeners
{
    public sealed class IBAccountListener : BackgroundService
    {
        private readonly IBClient _ib;
        private readonly IEnumerable<IAccountUpdateHandler> _handlers;
        private CancellationToken _ct;

        private readonly ILogger<IBAccountListener> _logger;

        public IBAccountListener(IBClient ib, IEnumerable<IAccountUpdateHandler> handlers, ILogger<IBAccountListener> logger)
        {
            _ib = ib;
            _handlers = handlers;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _ct = cancellationToken;
            _ib.AccountUpdateReceived += OnAccountUpdate;
            MDALog.AccountUpdateListenerAttached(_logger);
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _ib.AccountUpdateReceived -= OnAccountUpdate;
            MDALog.AccountUpdateListenerDetached(_logger);
            return base.StopAsync(cancellationToken);
        }

        private void OnAccountUpdate(IBAccountUpdate accountUpdate)
        {
            foreach (var handler in _handlers)
            {
                _ = RunHandler(handler, accountUpdate, _ct);
            }
        }

        private async Task RunHandler(IAccountUpdateHandler handler, IBAccountUpdate accountUpdate, CancellationToken ct)
        {
            try
            {
                await handler.HandleAsync(accountUpdate, ct);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { }
            catch (Exception ex)
            {
                MDALog.HandlerUnhandledException(_logger, ex, handler.GetType().Name);
            }
        }

    }
}
