using MDA.Web.API.Hubs;
using MDA.Web.Application.Accounts.Contracts;
using MDA.Web.Application.Instruments.Contracts;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace MDA.Web.Application.Instruments.Service
{
    public sealed class InstrumentService
    {
        private readonly ILogger<InstrumentService> _logger;
        private readonly IHubContext<MDAHub> _hub;
        public InstrumentService(ILogger<InstrumentService> logger, IHubContext<MDAHub> hub)
        {
            _logger = logger;
            _hub = hub;
        }

        public async Task HandleKafkaMessageAsync(string message, CancellationToken cancellationToken)
        {
            //_logger.LogInformation(message);
            //await _hub.Clients.All.SendAsync("marketData", new
            //{
            //    key = "marketData",
            //    value = message
            //}, cancellationToken);

            var marketDataMessage = JsonSerializer.Deserialize<MarketDataKafkaMessage>(message);
            if (marketDataMessage == null)
            {
                // TODO: Proper logging
                _logger.LogWarning("Null market data message");
            }
            else
            {
                _logger.LogInformation($"Incoming MarketData message: {message}");
            }
        }
    }
}
