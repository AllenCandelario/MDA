using MDA.Model;
using System.Text.Json;

namespace MDA.App.Service.Notification
{
    public sealed class IBNotificationTestLongService : INotificationHandler
    {
        public IBNotificationTestLongService() { }

        public async Task HandleAsync(IBNotification notification, CancellationToken ct)
        {
            Console.WriteLine($"[Long Received]: {JsonSerializer.Serialize(notification)}");
            await Task.Delay(5000);
            Console.WriteLine("[Long Processed]");
        }
    }
}
