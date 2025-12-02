using Microsoft.Extensions.Logging;

namespace MDA.App.Log
{
    internal static partial class MDALog
    {
        // 5xx = errors, 4xx = warnings, 2xx = info, 1xx = debug (suggested scheme)

        // Listener & connections/subscriptions
        [LoggerMessage(EventId = 200, Level = LogLevel.Information, Message = "[Notification] Listener attached")]
        public static partial void NotificationListenerAttached(ILogger logger);

        [LoggerMessage(EventId = 201, Level = LogLevel.Information, Message = "[Notification] Listener detached")]
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

        [LoggerMessage(EventId = 209, Level = LogLevel.Information, Message = "[AccountUpdate] Refreshing subscription (unsub)")]
        public static partial void AccountUpdateResubscribeUnsub(ILogger logger);

        [LoggerMessage(EventId = 210, Level = LogLevel.Information, Message = "[AccountUpdate] Refreshing subscription (sub)")]
        public static partial void AccountUpdateResubscribeSub(ILogger logger);

        [LoggerMessage(EventId = 410, Level = LogLevel.Warning, Message = "[AccountUpdate] IB not ready after retries. Skipping subscription.")]
        public static partial void AccountUpdateNotReadyAfterRetries(ILogger logger);


        [LoggerMessage(EventId = 211, Level = LogLevel.Information, Message = "[MarketData] Subscribed to delayed market data")]
        public static partial void MarketDataSubscribeDelayed(ILogger logger);

        [LoggerMessage(EventId = 212, Level = LogLevel.Information, Message = "[MarketData] Unsubscribed all")]
        public static partial void MarketDataUnsubscribeAll(ILogger logger);

        [LoggerMessage(EventId = 213, Level = LogLevel.Information, Message = "[MarketData] Contract {ContractId} with Request {RequestId} already subscdribed")]
        public static partial void MarketDataAlreadySubscribed(ILogger logger, int ContractId, int RequestId);
        [LoggerMessage(EventId = 214, Level = LogLevel.Information, Message = "[Market Data] Listener attached")]
        public static partial void MarketDataListenerAttached(ILogger logger);

        [LoggerMessage(EventId = 215, Level = LogLevel.Information, Message = "[Market Data] Listener detached")]
        public static partial void MarketDataListenerDetached(ILogger logger);



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
