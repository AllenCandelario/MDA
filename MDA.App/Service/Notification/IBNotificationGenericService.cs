using MDA.Model;

namespace MDA.App.Service.Notification
{
    public sealed class IBNotificationGenericService : INotificationHandler
    {
        public IBNotificationGenericService() { }

        public async Task HandleAsync(IBNotification notification, CancellationToken ct)
        {
            throw new Exception("test error");
        }
    }
}
