using MDA.Web.Log;

namespace MDA.Web.Application.Notifications.Service
{
    public sealed class IBNotificationService
    {
        private readonly ILogger<IBNotificationService> _logger;
        public IBNotificationService(ILogger<IBNotificationService> logger) 
        { 
            _logger = logger;
        }

        public Task HandleKafkaMessageAsync(string message, CancellationToken cancellationToken)
        {
            _logger.LogInformation(message);
            return Task.CompletedTask;
        }
    }
}
