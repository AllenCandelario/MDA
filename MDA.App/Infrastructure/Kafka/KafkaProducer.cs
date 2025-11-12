using Confluent.Kafka;
using MDA.App.Log;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MDA.App.Infrastructure.Kafka
{
    public sealed class KafkaProducer : IDisposable
    {
        public IProducer<string, string> Producer { get; }

        public KafkaProducer(IOptions<KafkaConfigOptions> options, ILogger<KafkaProducer> logger)
        {
            var o = options.Value;
            var kafkaConfig = new ProducerConfig()
            {
                BootstrapServers = o.BootstrapServers,
                ClientId = o.ClientId,
                Acks = Acks.All,
                EnableIdempotence = o.EnableIdempotence,
                MessageTimeoutMs = o.MessageTimeoutMs,
                LogConnectionClose = o.LogConnectionClose,
                ReconnectBackoffMs = o.ReconnectBackoffMs,
                ReconnectBackoffMaxMs = o.ReconnectBackoffMaxMs,
                StatisticsIntervalMs = o.StatisticsIntervalMs
            };

            var builder = new ProducerBuilder<string, string>(kafkaConfig)
                .SetErrorHandler((_, e) =>
                {
                    var code = e.Code;
                    var isSevere =
                    e.IsFatal ||
                    code == ErrorCode.Local_AllBrokersDown ||
                    code == ErrorCode.Local_Transport ||
                    code == ErrorCode.Local_Authentication ||
                    code == ErrorCode.Local_Resolve ||
                    code == ErrorCode.Local_MsgTimedOut; // signals systemic delivery failure
                    if (isSevere)
                        MDALog.KafkaLibError(logger, e.Reason, (int)code, true);
                    else
                        MDALog.KafkaLibWarn(logger, e.Reason, (int)code, false);
                })
                .SetLogHandler((_, m) =>
                {
                    MDALog.KafkaLibLog(logger, m.Facility, m.Message);
                });

            Producer = builder.Build();
        }

        public void Dispose() => Producer.Dispose();
    }
}
