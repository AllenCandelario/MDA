using Confluent.Kafka;
using MDA.App.Infrastructure.Kafka;
using MDA.App.Log;
using MDA.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace MDA.App.Service.AccountUpdate
{
    public sealed class IBAccountUpdateKafkaService : IAccountUpdateHandler
    {
        private readonly KafkaProducer _kafka;
        private readonly string _topic;
        private readonly ILogger<IBAccountUpdateKafkaService> _logger;

        public IBAccountUpdateKafkaService(KafkaProducer kafka, IOptions<KafkaConfigOptions> opts, ILogger<IBAccountUpdateKafkaService> logger)
        {
            _kafka = kafka;
            _topic = opts.Value.AccountUpdateTopic;
            _logger = logger;
        }

        public Task HandleAsync(IBAccountUpdate update, CancellationToken ct)
        {
            string key;
            string value = JsonSerializer.Serialize(update, update.GetType());

            switch (update)
            {
                case IBUpdateAccountValue v:
                    key = "IBUpdateAccountValue";
                    break;
                case IBUpdatePortfolio p:
                    key = "IBUpdatePortfolio";
                    break;
                case IBUpdateAccountTime t:
                    key = "IBUpdateAccountTime";
                    break;
                case IBAccountDownloadEnd e:
                    key = "IBAccountDownloadEnd";
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
    