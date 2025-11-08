using IBApi;
using MDA.Config;
using MDA.Model;

namespace MDA.Implementation
{
    public partial class IBClient : EWrapper
    {
        private int _nextAccountSummaryRequestId = 0;
        private readonly HashSet<int> _activeAccountSummaryRequestIds = new();
        
        private List<string> AccountIds { get; set; }
        public bool _accountUpdateSubscriptionReady = false;

        public event Action<IBAccountUpdate> AccountUpdateReceived;

        void EWrapper.managedAccounts(string accountsList)
        {
            AccountIds = new List<string>(accountsList.Split(','));
            IBLogging.ManagedAccounts(_logger, accountsList);
            CheckAccountUpdateSubscriptionReady();
        }

        #region Account Summary
        /*
         * EClient.reqAccountSummary --> EWrapper.accountSummary --> EWrapper.accountSummaryEnd
         * EClient.cancelAccountSummary to unsubscribe
         * Refreshes every 3 minutes for values that have changed
        */

        // Action to subscribe
        public int SubscribeToAccountSummary(string accountGroup = "All", string commaSeparatedTags = "")
        {
            int requestId = Interlocked.Increment(ref _nextAccountSummaryRequestId);

            if (string.IsNullOrEmpty(commaSeparatedTags))
            {
                commaSeparatedTags = AccountSummaryTags.GetAllTags();
            }
            IBLogging.AccountSummarySub(_logger, accountGroup, requestId);
            _clientSocket.reqAccountSummary(requestId, accountGroup, commaSeparatedTags);
            _activeAccountSummaryRequestIds.Add(requestId);
            return requestId;
        }

        // Action to cancel
        public void CancelAccountSummary(int requestId)
        {
            if (_activeAccountSummaryRequestIds.Remove(requestId))
            {
                IBLogging.AccountSummaryCancel(_logger, requestId);
                _clientSocket.cancelAccountSummary(requestId);
            }
            else
            {
                IBLogging.AccountSummaryCancelError(_logger, requestId);
            }
        }

        void EWrapper.accountSummary(int reqId, string account, string tag, string value, string currency)
        {
            // Do nothing for now
        }

        /*o
         * Only triggers on first reqAccountSummary
         * Subsequent account summary updates (every 3 minutes) will show the updated value without triggering this
        */
        void EWrapper.accountSummaryEnd(int reqId)
        {
            // Do nothing for now
        }
        #endregion

        /*
         * EClient.reqAccountUpdates --> EWrapper.accountSummary --> EWrapper.accountSummaryEnd | EClient.cancelAccountSummary to unsubscribe
         * Refreshes every 3 minutes
        */
        #region Account Updates

        // Action to subscribe / cancel
        public void SubscribeToAccountUpdates(bool subscribe, string accountId = "")
        {
            if (string.IsNullOrEmpty(accountId))
            {
                IBLogging.AccountUpdateSubEmptyId(_logger);
                accountId = AccountIds.FirstOrDefault();
            }
            if (subscribe)
            {
                IBLogging.AccountUpdateSub(_logger, accountId);
            }
            else
            {
                IBLogging.AccountUpdateCancel(_logger, accountId);
            }
            _clientSocket.reqAccountUpdates(subscribe, accountId);
        }

        void EWrapper.updateAccountValue(string key, string value, string currency, string accountName)
        {
            var dto = new IBUpdateAccountValue(key, value, currency, accountName);
            AccountUpdateReceived?.Invoke(dto);
        }

        public void updatePortfolio(Contract contract, decimal position, double marketPrice, double marketValue,
            double averageCost, double unrealizedPNL, double realizedPNL, string accountName)
        {
            var dto = new IBUpdatePortfolio(contract, position, marketPrice, marketValue, averageCost, unrealizedPNL, realizedPNL, accountName);
            AccountUpdateReceived?.Invoke(dto);
        }

        void EWrapper.updateAccountTime(string timestamp)
        {
            var dto = new IBUpdateAccountTime(timestamp);
            AccountUpdateReceived?.Invoke(dto);
        }

        void EWrapper.accountDownloadEnd(string account)
        {
            var dto = new IBAccountDownloadEnd(account);
            AccountUpdateReceived?.Invoke(dto);
        }
        void CheckAccountUpdateSubscriptionReady()
        {
            if (!_accountUpdateSubscriptionReady && AccountIds?.Any() == true)
            {
                _accountUpdateSubscriptionReady = true;
            }
        }

        #endregion
    }
}
