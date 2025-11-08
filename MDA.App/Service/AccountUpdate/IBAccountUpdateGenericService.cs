using MDA.Model;

namespace MDA.App.Service.AccountUpdate
{
    public sealed class IBAccountUpdateGenericService : IAccountUpdateHandler
    {
        public IBAccountUpdateGenericService() { }

        public async Task HandleAsync(IBAccountUpdate accountUpdate, CancellationToken cancellationToken)
        {
            // Sample handler for reference, do nothing
        }
    }
}
