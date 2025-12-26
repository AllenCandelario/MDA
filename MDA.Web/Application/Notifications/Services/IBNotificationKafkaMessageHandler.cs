using MDA.Web.Application.Shared;
using MDA.Web.Log;

namespace MDA.Web.Application.Notifications.Services
{
    public sealed class IBNotificationKafkaMessageHandler : IKafkaMessageHandler
    {
        private readonly ILogger<IBNotificationKafkaMessageHandler> _logger;
        public IBNotificationKafkaMessageHandler(ILogger<IBNotificationKafkaMessageHandler> logger) 
        { 
            _logger = logger;
        }

        public Task HandleKafkaMessageAsync(string key, string message, CancellationToken cancellationToken)
        {
            _logger.LogInformation(message);
            return Task.CompletedTask;
        }
    }
}
