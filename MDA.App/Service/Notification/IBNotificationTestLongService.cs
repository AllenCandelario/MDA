using MDA.Model;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MDA.App.Service.Notification
{
    public sealed class IBNotificationTestLongService : INotificationHandler
    {
        private readonly ILogger<IBNotificationTestLongService> _logger;
        public IBNotificationTestLongService(ILogger<IBNotificationTestLongService> logger) 
        { 
            _logger = logger;
        }

        public async Task HandleAsync(IBNotification notification, CancellationToken ct)
        {
            _logger.LogInformation($"[Notification Long Received]: {JsonSerializer.Serialize(notification)}");
            await Task.Delay(5000);
            _logger.LogInformation("[Notification Long Processed]");
        }
    }
}
