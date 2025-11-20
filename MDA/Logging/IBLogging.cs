using Microsoft.Extensions.Logging;

namespace MDA.Config
{
    internal static partial class IBLogging
    {
        // EventId is not confirmed, currently it's 100 for connections, 200 for accounts, 300 for errors


        #region Connection logs
        [LoggerMessage(EventId = 100, Level = LogLevel.Information, Message = "Connection acknowledged")]
        public static partial void ConnectionAck(ILogger logger);

        [LoggerMessage(EventId = 101, Level = LogLevel.Information, Message = "Connection closed")]
        public static partial void ConnectionClosed(ILogger logger);

        [LoggerMessage(EventId = 102, Level = LogLevel.Information, Message = "nextValidId: {orderId}")]
        public static partial void NextValidId(ILogger logger, int orderId);
        #endregion

        #region Account-related logs
        [LoggerMessage(EventId = 200, Level = LogLevel.Information, Message = "Managed accounts: {accounts}")]
        public static partial void ManagedAccounts(ILogger logger, string accounts);

        [LoggerMessage(EventId = 210, Level = LogLevel.Information, Message = "[AccountSummary] Subscribing for account group={accountGroup} with reqId={requestId}")]
        public static partial void AccountSummarySub(ILogger logger, string accountGroup, int requestId);

        [LoggerMessage(EventId = 211, Level = LogLevel.Information, Message = "[AccountSummary] Cancelling reqId={requestId}")]
        public static partial void AccountSummaryCancel(ILogger logger, int requestId);

        [LoggerMessage(EventId = 212, Level = LogLevel.Information, Message = "[AccountSummary] Attempted to cancel unknown or already cancelled reqId={requestId}")]
        public static partial void AccountSummaryCancelError(ILogger logger, int requestId);


        [LoggerMessage(EventId = 220, Level = LogLevel.Information, Message = "[AccountUpdate] Subscribing for accId={accountId}")]
        public static partial void AccountUpdateSub(ILogger logger, string accountId);

        [LoggerMessage(EventId = 221, Level = LogLevel.Information, Message = "[AccountUpdate] Cancelling subscription for accId={accountId}")]
        public static partial void AccountUpdateCancel(ILogger logger, string accountId);
        [LoggerMessage(EventId = 222, Level = LogLevel.Information, Message = "[AccountUpdate] AccountId is empty, pulling from list of AccountIds")]
        public static partial void AccountUpdateSubEmptyId(ILogger logger);
        #endregion

        [LoggerMessage(EventId = 300, Level = LogLevel.Warning, Message = "Unhandled handler error: {Handler} {Error}")]
        public static partial void HandlerError(ILogger logger, string Handler, string Error);
    }
}
