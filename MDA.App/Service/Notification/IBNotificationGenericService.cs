using MDA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
