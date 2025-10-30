using MDA.Model;

namespace MDA.App.Service.AccountUpdate
{
    public interface IAccountUpdateHandler
    {
         Task HandleAsync(IBAccountUpdate accountUpdate, CancellationToken cancellationToken);
    }
}
