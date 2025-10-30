using MDA.Model;

namespace MDA.App.Service.Notification
{
    public interface INotificationHandler
    {
        Task HandleAsync(IBNotification notification, CancellationToken cancellationToken);
    }
}
