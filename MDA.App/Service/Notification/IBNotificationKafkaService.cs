using Confluent.Kafka;
using MDA.App.Infrastructure.Kafka;
using MDA.Model;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace MDA.App.Service.Notification
{
    public sealed class IBNotificationKafkaService : INotificationHandler
    {
        private readonly KafkaProducer _kafka;

        private readonly string _topic;
        
        public IBNotificationKafkaService(KafkaProducer kafka, IOptions<KafkaConfigOptions> opts) 
        { 
            _kafka = kafka;
            _topic = opts.Value.NotificationTopic;
        }

        public Task HandleAsync(IBNotification n, CancellationToken ct)
        {
            var kafkaMessage = new Message<string, string> { Key = "ibNotification", Value = JsonSerializer.Serialize(n) };
            Console.WriteLine($"[Kafka Received]: {JsonSerializer.Serialize(n)}");
            try
            {
                _kafka.Producer.Produce(_topic, kafkaMessage);
            }
            catch (ProduceException<string, string> ex)
            {
                Console.Error.WriteLine($"[Kafka] drop: {ex.Error.Reason}");
            }
            Console.WriteLine($"[Kafka Processed]");
            return Task.CompletedTask;
        }
    }
}