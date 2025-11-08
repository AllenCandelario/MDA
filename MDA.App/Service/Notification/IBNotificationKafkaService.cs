using Confluent.Kafka;
using MDA.App.Infrastructure.Kafka;
using MDA.App.Log;
using MDA.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace MDA.App.Service.Notification
{
    public sealed class IBNotificationKafkaService : INotificationHandler
    {
        private readonly KafkaProducer _kafka;

        private readonly string _topic;

        private readonly ILogger<IBNotificationKafkaService> _logger;
        
        public IBNotificationKafkaService(KafkaProducer kafka, IOptions<KafkaConfigOptions> opts, ILogger<IBNotificationKafkaService> logger) 
        { 
            _kafka = kafka;
            _topic = opts.Value.NotificationTopic;
            _logger = logger;
        }

        public Task HandleAsync(IBNotification n, CancellationToken ct)
        {
            var payload = JsonSerializer.Serialize(n);
            var kafkaMessage = new Message<string, string> { Key = "ibNotification", Value = payload };
            try
            {
                _kafka.Producer.Produce(_topic, kafkaMessage, report =>
                {
                    if (report.Error.IsError)
                        MDALog.KafkaDrop(_logger, new Exception(report.Error.Reason), report.Error.Reason);
                    else if (_logger.IsEnabled(LogLevel.Debug))
                        MDALog.KafkaProduceInfo(_logger, report.Topic, report.Message.Key!, payload.Length);
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