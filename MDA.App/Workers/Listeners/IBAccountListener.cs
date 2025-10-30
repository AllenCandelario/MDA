using MDA.App.Service.AccountUpdate;
using MDA.Implementation;
using MDA.Model;
using Microsoft.Extensions.Hosting;

namespace MDA.App.Workers.Listeners
{
    public sealed class IBAccountListener : BackgroundService
    {
        private readonly IBClient _ib;
        private readonly IEnumerable<IAccountUpdateHandler> _handlers;
        private CancellationToken _ct;

        public IBAccountListener(IBClient ib, IEnumerable<IAccountUpdateHandler> handlers)
        {
            _ib = ib;
            _handlers = handlers;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _ct = cancellationToken;
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
            foreach (var handler in _handlers)
            {
                _ = RunHandler(handler, accountUpdate, _ct);
            }
        }

        private static async Task RunHandler(IAccountUpdateHandler handler, IBAccountUpdate accountUpdate, CancellationToken ct)
        {
            try
            {
                await handler.HandleAsync(accountUpdate, ct);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[{handler.GetType().Name}] {ex}");
            }
        }

    }
}
