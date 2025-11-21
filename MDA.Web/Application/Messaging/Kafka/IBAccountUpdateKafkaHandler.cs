using MDA.Web.API.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace MDA.Web.Application.Messaging.Kafka
{
    public sealed class IBAccountUpdateKafkaHandler
    {
        private readonly ILogger<IBAccountUpdateKafkaHandler> _logger;
        private readonly IHubContext<MDAHub> _hub;
        public IBAccountUpdateKafkaHandler(ILogger<IBAccountUpdateKafkaHandler> logger, IHubContext<MDAHub> hub)
        {
            _logger = logger;
            _hub = hub;
        }

        public async Task HandleAsync(string message, CancellationToken cancellationToken)
        {
            _logger.LogInformation(message);
            await _hub.Clients.All.SendAsync("accountUpdate", new
            {
                key = "accountUpdate",
                value = message
            }, cancellationToken);
        }
    }
}
