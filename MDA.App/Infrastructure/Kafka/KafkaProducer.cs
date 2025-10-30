using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDA.App.Infrastructure.Kafka
{
    public sealed class KafkaProducer : IDisposable
    {
        public IProducer<string, string> Producer { get; }

        public KafkaProducer(IOptions<KafkaConfigOptions> options)
        {
            var o = options.Value;
            var kafkaConfig = new ProducerConfig()
            {
                BootstrapServers = o.BootstrapServers,
                ClientId = o.ClientId,
                Acks = Acks.All,
                EnableIdempotence = o.EnableIdempotence,
                MessageTimeoutMs = o.MessageTimeoutMs,
            };
            Producer = new ProducerBuilder<string, string>(kafkaConfig).Build();
        }

        public void Dispose() => Producer.Dispose();
    }
}
