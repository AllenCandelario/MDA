using Microsoft.Extensions.Logging;

namespace MDA.Web.Log
{
    internal static partial class WebLog
    {
        // 3xx = consumer lifecycle, 1xx = debug, 5xx = errors, 4xx = warnings (suggested scheme)

        [LoggerMessage(EventId = 300, Level = LogLevel.Information, Message = "Kafka consumer starting. Group={Group} Topics=[{Topics}]")]
        public static partial void ConsumerStarting(ILogger logger, string Group, string Topics);

        [LoggerMessage(EventId = 301, Level = LogLevel.Information, Message = "Kafka consumer stopped.")]
        public static partial void ConsumerStopped(ILogger logger);

        [LoggerMessage(EventId = 110, Level = LogLevel.Debug, Message = "Consumed topic={Topic} partition={Partition} offset={Offset} key={Key} bytes={Bytes}")]
        public static partial void ConsumedDebug(ILogger logger, string Topic, int Partition, long Offset, string Key, int Bytes);

        [LoggerMessage(EventId = 111, Level = LogLevel.Debug, Message = "Kafka({Facility}): {Message}")]
        public static partial void KafkaLibLog(ILogger logger, string Facility, string Message);

        [LoggerMessage(EventId = 420, Level = LogLevel.Warning, Message = "Kafka warning: {Reason} ({Code})")]
        public static partial void KafkaWarn(ILogger logger, string Reason, int Code);

        [LoggerMessage(EventId = 520, Level = LogLevel.Error, Message = "Kafka fatal error: {Reason} ({Code})")]
        public static partial void KafkaFatal(ILogger logger, string Reason, int Code);

        [LoggerMessage(EventId = 521, Level = LogLevel.Error, Message = "Consume error: {Reason}")]
        public static partial void ConsumeError(ILogger logger, string Reason, Exception exception);

        [LoggerMessage(EventId = 522, Level = LogLevel.Error, Message = "JSON deserialization failed. Topic={Topic} Key={Key} ValueLen={Len}")]
        public static partial void JsonDeserError(ILogger logger, string Topic, string Key, int Len, Exception exception);

        [LoggerMessage(EventId = 523, Level = LogLevel.Error, Message = "Processing failed. Topic={Topic} Partition={Partition} Offset={Offset}")]
        public static partial void ProcessingFailed(ILogger logger, string Topic, int Partition, long Offset, Exception exception);

        [LoggerMessage(EventId = 421, Level = LogLevel.Warning, Message = "Unknown message key '{Key}' on topic {Topic}. Payload length={Len}")]
        public static partial void UnhandledTopic(ILogger logger, string Topic, string Key, int Len);

        [LoggerMessage(EventId = 310, Level = LogLevel.Information, Message = "[Notification] handled type={Type}")]
        public static partial void NotificationHandled(ILogger logger, string Type);

        [LoggerMessage(EventId = 311, Level = LogLevel.Information, Message = "[AccountUpdate] handled type={Type}")]
        public static partial void AccountUpdateHandled(ILogger logger, string Type);
    }
}
