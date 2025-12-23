using Confluent.Kafka;
using MDA.Web.Application.Accounts.Service;
using MDA.Web.Application.Instruments.Service;
using MDA.Web.Application.Notifications.Service;
using MDA.Web.Log;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace MDA.Web.Infrastructure.Messaging.Kafka
{
    public sealed class KafkaConsumer : BackgroundService
    {

        private readonly ILogger<KafkaConsumer> _logger;
        private readonly KafkaConsumerConfigOptions _options;
        private readonly IServiceScopeFactory _scopeFactory;

        public KafkaConsumer(IOptions<KafkaConsumerConfigOptions> options, ILogger<KafkaConsumer> logger, IServiceScopeFactory scopeFactory) 
        {
            _options = options.Value;
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {

            return Task.Run(async () =>
            {
                var kafkaConfig = new ConsumerConfig
                {
                    BootstrapServers = _options.BootstrapServers,
                    GroupId = _options.GroupId,
                    EnableAutoCommit = _options.EnableAutoCommit,
                    MaxPollIntervalMs = _options.MaxPollIntervalMs,
                    SessionTimeoutMs = _options.SessionTimeoutMs
                };

                using var consumer = new ConsumerBuilder<string, string>(kafkaConfig)
                   .SetErrorHandler((_, e) =>
                   {
                       if (e.IsFatal)
                       {
                           WebLog.KafkaFatal(_logger, e.Reason, (int)e.Code);
                       }
                       else
                       {
                           WebLog.KafkaWarn(_logger, e.Reason, (int)e.Code);
                       }
                   })
                   .SetLogHandler((_, m) =>
                   {
                       WebLog.KafkaLibLog(_logger, m.Facility, m.Message);
                   })
                   .Build();

                WebLog.ConsumerStarting(_logger, _options.GroupId, string.Join(",", _options.Topics));
                consumer.Subscribe(_options.Topics);

                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        ConsumeResult<string, string> cr;

                        try
                        {
                            cr = consumer.Consume(cancellationToken);
                        }
                        catch (ConsumeException ex)
                        {
                            WebLog.ConsumeError(_logger, ex.Error.Reason, ex);
                            continue; // try next poll
                        }

                        if (cr?.Message == null) continue;

                        try
                        {
                            await ProcessWithRetryAsync(cr, cancellationToken);

                            if (!_options.EnableAutoCommit)
                            {
                                consumer.Commit(cr);
                            }
                        }
                        catch (JsonException ex)
                        {
                            WebLog.JsonDeserError(_logger, cr.Topic, cr.Message.Key ?? "", cr.Message.Value?.Length ?? 0, ex);
                            if (!_options.EnableAutoCommit)
                            {
                                consumer.Commit(cr); // skip bad message permanently
                            }
                        }
                        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { }
                        catch (Exception ex)
                        {
                            WebLog.ProcessingFailed(_logger, cr.Topic, cr.Partition.Value, cr.Offset.Value, ex);
                            // TODO: Consider kafka DLQ
                            if (!_options.EnableAutoCommit)
                            {
                                consumer.Commit(cr); // skip bad message permanently
                            }
                        }
                    }
                }
                finally
                {
                    try
                    {
                        consumer.Close();
                    }
                    catch { }
                    WebLog.ConsumerStopped(_logger);
                }
            }, cancellationToken);
        }

        // Process messages with a max retries of 3 and 100ms between each attempt
        private async Task ProcessWithRetryAsync(ConsumeResult<string, string> cr, CancellationToken ct)
        {
            const int maxAttempts = 3;
            var attempt = 0;

            while (true)
            {
                try
                {
                    await ProcessByTopicAsync(cr, ct);
                    return;
                }
                catch (Exception) when (++attempt < maxAttempts)
                {
                    await Task.Delay(100, ct);
                }
            }
        }

        /// <summary>
        /// Dispatch by key → deserialize → do work.
        /// Keep this **fast**. If work is heavy/slow IO, hand off to a bounded Channel and return quickly.
        /// </summary>
        private async Task ProcessByTopicAsync(ConsumeResult<string, string> cr, CancellationToken ct)
        {

            // Single scope per message
            using var scope = _scopeFactory.CreateScope();
            var sp = scope.ServiceProvider;

            // TODO: might need to add a cancellation token condition for processing to not be more than x seconds
            switch (cr.Topic)
            {
                case "dev.mda.ib.notification.v1":
                    var notificationHandler = sp.GetRequiredService<IBNotificationService>();
                    await notificationHandler.HandleKafkaMessageAsync(cr.Message.Value, ct);
                    break;
                case "dev.mda.ib.account.update.v1":
                    var accountUpdateHandler = sp.GetRequiredService<AccountService>();
                    await accountUpdateHandler.HandleKafkaMessageAsync(cr.Message.Key, cr.Message.Value, ct);
                    break;
                case "dev.mda.ib.market.data.v1":
                    var marketDataHandler = sp.GetRequiredService<InstrumentService>();
                    await marketDataHandler.HandleKafkaMessageAsync(cr.Message.Key, cr.Message.Value, ct);
                    break;
                default:
                    WebLog.UnhandledTopic(_logger, cr.Topic, cr.Message.Key, cr.Message.Value?.Length ?? 0);
                    break;
            }
        }
    }
}
