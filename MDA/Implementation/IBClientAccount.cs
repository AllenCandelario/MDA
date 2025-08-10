using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using IBApi;
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
            Console.WriteLine($"List of Account Ids: {accountsList}");
            CheckAccountUpdateSubscriptionReady();
        }

        /*
         * EClient.reqAccountSummary --> EWrapper.accountSummary --> EWrapper.accountSummaryEnd
         * EClient.cancelAccountSummary to unsubscribe
         * Refreshes every 3 minutes for values that have changed
        */
        #region Account Summary

        // Action to subscribe
        public int SubscribeToAccountSummary(string accountGroup = "All", string commaSeparatedTags = "")
        {
            int requestId = Interlocked.Increment(ref _nextAccountSummaryRequestId);

            if (string.IsNullOrEmpty(commaSeparatedTags))
            {
                commaSeparatedTags = AccountSummaryTags.GetAllTags();
            }
            Console.WriteLine($"[AccountSummary] Subscribing for Account Group: {accountGroup} with requestId: {requestId}");
            _clientSocket.reqAccountSummary(requestId, accountGroup, commaSeparatedTags);
            _activeAccountSummaryRequestIds.Add(requestId);
            return requestId;
        }

        // Action to cancel
        public void CancelAccountSummary(int requestId)
        {
            if (_activeAccountSummaryRequestIds.Remove(requestId))
            {
                Console.WriteLine($"[AccountSummary] Cancelling requestId={requestId}");
                _clientSocket.cancelAccountSummary(requestId);
            }
            else
            {
                Console.WriteLine($"[AccountSummary] Attempted to cancel unknown or already cancelled requestId={requestId}");
            }
        }

        void EWrapper.accountSummary(int reqId, string account, string tag, string value, string currency)
        {
            Console.WriteLine($"accountSummary: reqId = {reqId}, account = {account}, tag = {tag}, value = {value}, currency = {currency}");
        }

        /*o
         * Only triggers on first reqAccountSummary
         * Subsequent account summary updates (every 3 minutes) will show the updated value without triggering this
        */
        void EWrapper.accountSummaryEnd(int reqId)
        {
            Console.WriteLine($"$accountSummaryEnd: {reqId}");
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
                Console.WriteLine("AccountId is empty, pull from list of AccountIds");
                accountId = AccountIds.FirstOrDefault();
            }
            string action = subscribe ? "Subscribing to" : "Cancelling subscription for";
            Console.WriteLine($"{action} account updates for account id: {accountId}");
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
