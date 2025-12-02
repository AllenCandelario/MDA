using Confluent.Kafka;
using MDA.App.Log;
using MDA.Model;
using System.Text.Json;

namespace MDA.App.Service.Notification
{
    public sealed class IBNotificationGenericService : INotificationHandler
    {
        public IBNotificationGenericService() { }

        public Task HandleAsync(IBNotification n, CancellationToken ct)
        {
            var value = JsonSerializer.Serialize(n);
            string key = "Notification";

            Console.WriteLine($"[IBNotification] Key = {key}, Value = {value}");

            return Task.CompletedTask;
        }
    }
}
