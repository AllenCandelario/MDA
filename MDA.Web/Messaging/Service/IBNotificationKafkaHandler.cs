using MDA.Web.Log;

namespace MDA.Web.Messaging.Service
{
    public sealed class IBNotificationKafkaHandler
    {
        private readonly ILogger<IBNotificationKafkaHandler> _logger;
        public IBNotificationKafkaHandler(ILogger<IBNotificationKafkaHandler> logger) 
        { 
            _logger = logger;
        }

        public Task HandleAsync(string message, CancellationToken cancellationToken)
        {
            _logger.LogInformation(message);
            return Task.CompletedTask;
        }
    }
}
