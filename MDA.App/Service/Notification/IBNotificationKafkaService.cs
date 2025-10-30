using Confluent.Kafka;
using MDA.Model;

namespace MDA.App.Service.Notification
{
    public sealed class IBNotificationKafkaService : INotificationHandler
    {
        public IBNotificationKafkaService() { }

        public async Task HandleAsync(IBNotification notification, CancellationToken ct)
        {
            string topic = "dev.mda.ib.notification.v1";
            string key = "ibNotification";
            var kafkaConfig = new ProducerConfig
            { 
                BootstrapServers = "localhost:9092",
                Acks = Acks.All
            };

            using (var producer = new ProducerBuilder<string, string>(kafkaConfig).Build())
            {
                producer.Produce(topic, new Message<string, string> { Key = key, Value = notification.ErrorMsg },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"Produced event to topic {topic}: key = {key} value = {notification.ErrorMsg}");
                        }
                    });
                producer.Flush(TimeSpan.FromSeconds(10));
            }
        }
    }
}
