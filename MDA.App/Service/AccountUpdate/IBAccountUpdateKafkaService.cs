using Confluent.Kafka;
using MDA.App.Infrastructure.Kafka;
using MDA.Model;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace MDA.App.Service.AccountUpdate
{
    public sealed class IBAccountUpdateKafkaService : IAccountUpdateHandler
    {
        private readonly KafkaProducer _kafka;
        private readonly string _topic;

        public IBAccountUpdateKafkaService(KafkaProducer kafka, IOptions<KafkaConfigOptions> opts)
        {
            _kafka = kafka;
            _topic = opts.Value.AccountUpdateTopic;
        }

        public Task HandleAsync(IBAccountUpdate update, CancellationToken ct)
        {
            string key;
            string value = JsonSerializer.Serialize(update);

            switch (update)
            {
                case IBUpdateAccountValue v:
                    key = $"{v.AccountName}:{v.Key}";
                    break;
                case IBUpdatePortfolio p:
                    key = $"{p.AccountName}:{p.Contract?.Symbol}";
                    break;
                case IBUpdateAccountTime t:
                    key = t.Timestamp;
                    break;
                case IBAccountDownloadEnd e:
                    key = e.Account;
                    break;
                default:
                    key = "unknown";
                    break;
            }

            var kafkaMessage = new Message<string, string> { Key = key, Value = JsonSerializer.Serialize(update) };
            Console.WriteLine($"[Kafka Received]: {JsonSerializer.Serialize(update)}");
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
