
namespace MDA.App.Infrastructure.Kafka
{
    public sealed class KafkaConfigOptions
    {
        public string BootstrapServers { get; set; }
        public string ClientId { get; set; }
        public bool EnableIdempotence { get; set; }
        public int MessageTimeoutMs { get; set; }


        public string NotificationTopic { get; set; }
        public string AccountUpdateTopic { get; set; }

    }
}
