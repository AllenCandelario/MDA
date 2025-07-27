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
        public List<string> AccountIds { get; set; }

        void EWrapper.managedAccounts(string accountsList)
        {
            AccountIds = [.. accountsList.Split(',')];
            Console.WriteLine($"List of Account Ids: {accountsList}");
        }

        /*
         * EClient.reqAccountSummary --> EWrapper.accountSummary --> EWrapper.accountSummaryEnd
         * EClient.cancelAccountSummary to unsubscribe
         * Refreshes every 3 minutes for values that have changed
        */
        #region Account Summary

        // Action to subscribe / cancel
        public void SubscribeToAccountSummary(bool subscribe = true, int requestId = 1, string accountGroup = "All", string commaSeparatedTags = "")
        {
            if (subscribe)
            {
                if (string.IsNullOrEmpty(commaSeparatedTags))
                {
                    commaSeparatedTags = AccountSummaryTags.GetAllTags();
                }
                Console.WriteLine($"Subscribing to account summary for Account Group: {accountGroup}");
                _clientSocket.reqAccountSummary(requestId, accountGroup, commaSeparatedTags);
            }
            else
            {
                Console.WriteLine($"Cancelling account summary subscription for request ID: {requestId}");
                _clientSocket.cancelAccountSummary(requestId);
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
            Console.WriteLine($"updateAccountValue: key = {key}, value = {value}, currency = {currency}, accountName = {accountName}");
        }

        public void updatePortfolio(Contract contract, decimal position, double marketPrice, double marketValue,
            double averageCost, double unrealizedPNL, double realizedPNL, string accountName)
        {
            Console.WriteLine($"updatePortfolio: contract = {JsonSerializer.Serialize(contract)}, position= {position}, marketPrice= {marketPrice}, marketValue= {marketValue}, averageCost= {averageCost}, unrealisedPNL = {unrealizedPNL}, realizedPNL = {realizedPNL}, accountName = {accountName}");
        }

        void EWrapper.updateAccountTime(string timestamp)
        {
            Console.WriteLine($"updateAccountTime: {timestamp}");
        }

        void EWrapper.accountDownloadEnd(string account)
        {
            Console.WriteLine($"accountDownloadEnd: {account}");
        }

        #endregion
    }
}
