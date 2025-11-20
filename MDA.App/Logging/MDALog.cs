using Microsoft.Extensions.Logging;

namespace MDA.App.Log
{
    internal static partial class MDALog
    {
        // 5xx = errors, 4xx = warnings, 2xx = info, 1xx = debug (suggested scheme)

        // Listener & connections/subscriptions
        [LoggerMessage(EventId = 200, Level = LogLevel.Information, Message = "Notification listener attached")]
        public static partial void NotificationListenerAttached(ILogger logger);

        [LoggerMessage(EventId = 201, Level = LogLevel.Information, Message = "Notification listener detached")]
        public static partial void NotificationListenerDetached(ILogger logger);

        [LoggerMessage(EventId = 202, Level = LogLevel.Information, Message = "IBKR Connected")]
        public static partial void IBKRConnected(ILogger logger);

        [LoggerMessage(EventId = 203, Level = LogLevel.Information, Message = "IBKR Disconnected")]
        public static partial void IBKRDisconnected(ILogger logger);


        [LoggerMessage(EventId = 204, Level = LogLevel.Information, Message = "[AccountUpdate] Listener attached")]
        public static partial void AccountUpdateListenerAttached(ILogger logger);

        [LoggerMessage(EventId = 205, Level = LogLevel.Information, Message = "[AccountUpdate] Listener detached")]
        public static partial void AccountUpdateListenerDetached(ILogger logger);

        [LoggerMessage(EventId = 206, Level = LogLevel.Information, Message = "[AccountUpdate] Subscribed")]
        public static partial void AccountUpdateSubscribe(ILogger logger);

        [LoggerMessage(EventId = 207, Level = LogLevel.Information, Message = "[AccountUpdate] Unsubscribed")]
        public static partial void AccountUpdateUnsubscribe(ILogger logger);

        [LoggerMessage(EventId = 208, Level = LogLevel.Information, Message = "[AccountUpdate] Waiting for IB readiness... attempt={Attempt}")]
        public static partial void AccountUpdateWaitingForReadiness(ILogger logger, int Attempt);

        [LoggerMessage(EventId = 410, Level = LogLevel.Warning, Message = "[AccountUpdate] IB not ready after retries. Skipping subscription.")]
        public static partial void AccountUpdateNotReadyAfterRetries(ILogger logger);



        // Handler fanout
        [LoggerMessage(EventId = 120, Level = LogLevel.Debug, Message = "Dispatching to handler {Handler}")]
        public static partial void DispatchingToHandler(ILogger logger, string Handler);

        [LoggerMessage(EventId = 500, Level = LogLevel.Error, Message = "[{Handler}] unhandled exception")]
        public static partial void HandlerUnhandledException(ILogger logger, Exception exception, string Handler);

        // Kafka
        [LoggerMessage(EventId = 220, Level = LogLevel.Information, Message = "Kafka produce topic={Topic} key={Key} bytes={Bytes}")]
        public static partial void KafkaProduceInfo(ILogger logger, string Topic, string Key, int Bytes);

        [LoggerMessage(EventId = 501, Level = LogLevel.Error, Message = "Kafka drop: {Reason}")]
        public static partial void KafkaDrop(ILogger logger, Exception exception, string Reason);

        [LoggerMessage(EventId = 221, Level = LogLevel.Warning, Message = "Kafka warn: {Reason} code={Code} is_critical={IsFatal}")]
        public static partial void KafkaLibWarn(ILogger logger, string Reason, int Code, bool IsFatal);

        [LoggerMessage(EventId = 222, Level = LogLevel.Error, Message = "Kafka error: {Reason} code={Code} is_critical={IsFatal}")]
        public static partial void KafkaLibError(ILogger logger, string Reason, int Code, bool IsFatal);

        [LoggerMessage(EventId = 223, Level = LogLevel.Debug, Message = "Kafka log ({Facility}): {Message}")]
        public static partial void KafkaLibLog(ILogger logger, string Facility, string Message);

        [LoggerMessage(EventId = 224, Level = LogLevel.Debug, Message = "Kafka stats: {StatsJson}")]
        public static partial void KafkaStats(ILogger logger, string StatsJson);
    }
}
