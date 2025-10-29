using MDA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDA.App.Service.Notification
{
    public sealed class IBNotificationTestLongService : INotificationHandler
    {
        public IBNotificationTestLongService() { }

        public async Task HandleAsync(IBNotification notification, CancellationToken ct)
        {
            Console.WriteLine($"[Long Received]: {JsonSerializer.Serialize(notification)}");
            await Task.Delay(5000);
            Console.WriteLine($"[Long]: {JsonSerializer.Serialize(notification)}");
        }
    }
}
