

using Confluent.Kafka;
using MDA.App.Infrastructure.Kafka;
using MDA.App.Log;
using MDA.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace MDA.App.Service.MarketData
{
    public sealed class IBMarketDataKafkaService : IMarketDataHandler
    {
        private readonly KafkaProducer _kafka;
        private readonly string _topic;
        private readonly ILogger<IBMarketDataKafkaService> _logger;

        public IBMarketDataKafkaService(KafkaProducer kafka, IOptions<KafkaConfigOptions> opts, ILogger<IBMarketDataKafkaService> logger)
        {
            _kafka = kafka;
            _topic = opts.Value.MarketDataTopic;
            _logger = logger;
        }

        public Task HandleAsync(IBMarketData marketData, CancellationToken ct)
        {
            string key;
            string value = JsonSerializer.Serialize(marketData, marketData.GetType());

            switch (marketData)
            {
                case IBTickPrice:
                    key = "IBTickPrice";
                    break;
                case IBTickSize:
                    key = "IBTickSize";
                    break;
                case IBTickReqParams:
                    key = "IBTickReqParams";
                    break;
                case IBTickString:
                    key = "IBTickString";
                    break;
                case IBTickEFP:
                    key = "IBTickEFP";
                    break;
                case IBTickGeneric:
                    key = "IBTickGeneric";
                    break;
                case IBTickOptionComputation:
                    key = "IBTickOptionComputation";
                    break;
                case IBTickSnapshotEnd:
                    key = "IBTickSnapshotEnd";
                    break;
                default:
                    key = "Unknown";
                    break;

            }

            var kafkaMessage = new Message<string, string> { Key = key, Value = value };
            try
            {
                _kafka.Producer.Produce(_topic, kafkaMessage, report =>
                {
                    if (report.Error.IsError)
                        MDALog.KafkaDrop(_logger, new Exception(report.Error.Reason), report.Error.Reason);
                    else if (_logger.IsEnabled(LogLevel.Debug))
                        MDALog.KafkaProduceInfo(_logger, report.Topic, key, value.Length);
                });
            }
            catch (ProduceException<string, string> ex)
            {
                MDALog.KafkaDrop(_logger, ex, ex.Error.Reason);
            }
            return Task.CompletedTask;
        }
    }
}
