using MDA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDA.App.Service
{
    public sealed class IBNotificationService
    {
        public IBNotificationService() { }

        public void HandleNotification(IBNotification notification)
        {
            Console.WriteLine($"Notification: {JsonSerializer.Serialize(notification)}");
        }
    }
}
