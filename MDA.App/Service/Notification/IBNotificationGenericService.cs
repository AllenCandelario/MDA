using MDA.Model;
using System.Text.Json;

namespace MDA.App.Service.Notification
{
    public sealed class IBNotificationGenericService : INotificationHandler
    {
        public IBNotificationGenericService() { }

        public async Task HandleAsync(IBNotification notification, CancellationToken ct)
        {
            Console.WriteLine($"[Generic Received]: {JsonSerializer.Serialize(notification)}");
            throw new Exception("[Generic Processed]: Throwing test exception");
        }
    }
}
