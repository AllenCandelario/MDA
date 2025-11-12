namespace MDA.Web.Messaging.Service
{
    public sealed class IBAccountUpdateKafkaHandler
    {
        private readonly ILogger<IBAccountUpdateKafkaHandler> _logger;
        public IBAccountUpdateKafkaHandler(ILogger<IBAccountUpdateKafkaHandler> logger)
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
