using Confluent.Kafka;

namespace MDA.Web.Infrastructure.Messaging.Kafka
{
    public sealed class KafkaConsumerConfigOptions
    {
        public string BootstrapServers { get; set; }
        public string GroupId { get; set; }
        public string[] Topics { get; set; }
        public AutoOffsetReset AutoOffsetReset { get; set; } = AutoOffsetReset.Earliest;
        public bool EnableAutoCommit { get; set; }
        public int MaxPollIntervalMs { get; set; }
        public int SessionTimeoutMs { get; set; }
    }
}
