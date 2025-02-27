using IBApi;
using System;
using System.Collections.Generic;

namespace MDA.Implementation
{
    public partial class IBClient : EWrapper
    {
        private static void DiscardImplementation(params object[] inputs)
        {
            _ = inputs;
        }

        public void error(string str)
        {
            DiscardImplementation(str);
            throw new NotImplementedException();
        }

        public void currentTime(long time)
        {
            DiscardImplementation(time);
            throw new NotImplementedException();
        }

        public void tickPrice(int tickerId, int field, double price, TickAttrib attrib)
        {
            DiscardImplementation(tickerId, field, price, attrib);
            throw new NotImplementedException();
        }

        public void tickSize(int tickerId, int field, decimal size)
        {
            DiscardImplementation(tickerId, field, size);
            throw new NotImplementedException();
        }

        public void tickString(int tickerId, int tickType, string value)
        {
            DiscardImplementation(tickerId, tickType, value);
            throw new NotImplementedException();
        }

        public void tickGeneric(int tickerId, int tickType, double value)
        {
            DiscardImplementation(tickerId, tickType, value);
            throw new NotImplementedException();
        }

        public void tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints,
            double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate)
        {
            DiscardImplementation(tickerId, tickType, basisPoints, formattedBasisPoints, impliedFuture, holdDays,
                futureLastTradeDate, dividendImpact, dividendsToLastTradeDate);
            throw new NotImplementedException();
        }

        public void deltaNeutralValidation(int reqId, DeltaNeutralContract deltaNeutralContract)
        {
            DiscardImplementation(reqId, deltaNeutralContract);
            throw new NotImplementedException();
        }

        public void tickOptionComputation(int tickerId, int tickType, int tickAttrib, double impliedVolatility, double delta,
            double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
        {
            DiscardImplementation(tickerId, tickType, tickAttrib, impliedVolatility, delta, optPrice, pvDividend, gamma, vega, theta, undPrice);
            throw new NotImplementedException();
        }

        public void tickSnapshotEnd(int tickerId)
        {
            DiscardImplementation(tickerId);
            throw new NotImplementedException();
        }

        public void connectionClosed()
        {
            throw new NotImplementedException();
        }

        public void accountSummary(int reqId, string account, string tag, string value, string currency)
        {
            DiscardImplementation(reqId, account, tag, value, currency);
            throw new NotImplementedException();
        }

        public void accountSummaryEnd(int reqId)
        {
            DiscardImplementation(reqId);
            throw new NotImplementedException();
        }

        public void bondContractDetails(int reqId, ContractDetails contractDetails)
        {
            DiscardImplementation(reqId, contractDetails);
            throw new NotImplementedException();
        }

        public void updateAccountValue(string key, string value, string currency, string accountName)
        {
            DiscardImplementation(key, value, currency, accountName);
            throw new NotImplementedException();
        }

        public void updatePortfolio(Contract contract, decimal position, double marketPrice, double marketValue,
            double averageCost, double unrealizedPNL, double realizedPNL, string accountName)
        {
            DiscardImplementation(contract, position, marketPrice, marketValue, averageCost, unrealizedPNL, realizedPNL, accountName);
            throw new NotImplementedException();
        }

        public void updateAccountTime(string timestamp)
        {
            DiscardImplementation(timestamp);
            throw new NotImplementedException();
        }

        public void accountDownloadEnd(string account)
        {
            DiscardImplementation(account);
            throw new NotImplementedException();
        }

        public void orderStatus(int orderId, string status, decimal filled, decimal remaining, double avgFillPrice,
            int permId, int parentId, double lastFillPrice, int clientId, string whyHeld, double mktCapPrice)
        {
            DiscardImplementation(orderId, status, filled, remaining, avgFillPrice, permId, parentId, lastFillPrice, clientId, whyHeld, mktCapPrice);
            throw new NotImplementedException();
        }

        public void openOrder(int orderId, Contract contract, Order order, OrderState orderState)
        {
            DiscardImplementation(orderId, contract, order, orderState);
            throw new NotImplementedException();
        }

        public void openOrderEnd()
        {
            throw new NotImplementedException();
        }

        public void contractDetails(int reqId, ContractDetails contractDetails)
        {
            DiscardImplementation(reqId, contractDetails);
            throw new NotImplementedException();
        }

        public void contractDetailsEnd(int reqId)
        {
            DiscardImplementation(reqId);
            throw new NotImplementedException();
        }

        public void execDetails(int reqId, Contract contract, Execution execution)
        {
            DiscardImplementation(reqId, contract, execution);
            throw new NotImplementedException();
        }

        public void execDetailsEnd(int reqId)
        {
            DiscardImplementation(reqId);
            throw new NotImplementedException();
        }

        public void commissionReport(CommissionReport commissionReport)
        {
            DiscardImplementation(commissionReport);
            throw new NotImplementedException();
        }

        public void fundamentalData(int reqId, string data)
        {
            DiscardImplementation(reqId, data);
            throw new NotImplementedException();
        }

        public void historicalData(int reqId, Bar bar)
        {
            DiscardImplementation(reqId, bar);
            throw new NotImplementedException();
        }

        public void historicalDataUpdate(int reqId, Bar bar)
        {
            DiscardImplementation(reqId, bar);
            throw new NotImplementedException();
        }

        public void historicalDataEnd(int reqId, string startDate, string endDate)
        {
            DiscardImplementation(reqId, startDate, endDate);
            throw new NotImplementedException();
        }

        public void marketDataType(int reqId, int marketDataType)
        {
            DiscardImplementation(reqId, marketDataType);
            throw new NotImplementedException();
        }

        public void updateMktDepth(int tickerId, int position, int operation, int side, double price, decimal size)
        {
            DiscardImplementation(tickerId, position, operation, side, price, size);
            throw new NotImplementedException();
        }

        public void updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, decimal size, bool isSmartDepth)
        {
            DiscardImplementation(tickerId, position, marketMaker, operation, side, price, size, isSmartDepth);
            throw new NotImplementedException();
        }

        public void updateNewsBulletin(int msgId, int msgType, string message, string origin)
        {
            DiscardImplementation(msgId, msgType, message, origin);
            throw new NotImplementedException();
        }

        public void position(string account, Contract contract, decimal position, double avgCost)
        {
            DiscardImplementation(account, contract, position, avgCost);
            throw new NotImplementedException();
        }

        public void positionEnd()
        {
            throw new NotImplementedException();
        }

        public void realtimeBar(int reqId, long time, double open, double high, double low, double close, decimal volume, decimal wap, int count)
        {
            DiscardImplementation(reqId, time, open, high, low, close, volume, wap, count);
            throw new NotImplementedException();
        }

        public void scannerParameters(string xml)
        {
            DiscardImplementation(xml);
            throw new NotImplementedException();
        }

        public void scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
        {
            DiscardImplementation(reqId, rank, contractDetails, distance, benchmark, projection, legsStr);
            throw new NotImplementedException();
        }

        public void scannerDataEnd(int reqId)
        {
            DiscardImplementation(reqId);
            throw new NotImplementedException();
        }

        public void receiveFA(int faDataType, string xml)
        {
            DiscardImplementation(faDataType, xml);
            throw new NotImplementedException();
        }

        public void verifyMessageAPI(string apiData)
        {
            DiscardImplementation(apiData);
            throw new NotImplementedException();
        }

        public void verifyCompleted(bool isSuccessful, string errorText)
        {
            DiscardImplementation(isSuccessful, errorText);
            throw new NotImplementedException();
        }

        public void verifyAndAuthMessageAPI(string apiData, string xyzChallenge)
        {
            DiscardImplementation(apiData, xyzChallenge);
            throw new NotImplementedException();
        }

        public void verifyAndAuthCompleted(bool isSuccessful, string errorText)
        {
            DiscardImplementation(isSuccessful, errorText);
            throw new NotImplementedException();
        }

        public void displayGroupList(int reqId, string groups)
        {
            DiscardImplementation(reqId, groups);
            throw new NotImplementedException();
        }

        public void displayGroupUpdated(int reqId, string contractInfo)
        {
            DiscardImplementation(reqId, contractInfo);
            throw new NotImplementedException();
        }

        public void connectAck()
        {
            throw new NotImplementedException();
        }

        public void positionMulti(int reqId, string account, string modelCode, Contract contract, decimal pos, double avgCost)
        {
            DiscardImplementation(reqId, account, modelCode, contract, pos, avgCost);
            throw new NotImplementedException();
        }

        public void positionMultiEnd(int reqId)
        {
            DiscardImplementation(reqId);
            throw new NotImplementedException();
        }

        public void accountUpdateMulti(int reqId, string account, string modelCode, string key, string value, string currency)
        {
            DiscardImplementation(reqId, account, modelCode, key, value, currency);
            throw new NotImplementedException();
        }

        public void accountUpdateMultiEnd(int reqId)
        {
            DiscardImplementation(reqId);
            throw new NotImplementedException();
        }

        public void securityDefinitionOptionParameter(int reqId, string exchange, int underlyingConId, string tradingClass, string multiplier, HashSet<string> expirations, HashSet<double> strikes)
        {
            DiscardImplementation(reqId, exchange, underlyingConId, tradingClass, multiplier, expirations, strikes);
            throw new NotImplementedException();
        }

        public void securityDefinitionOptionParameterEnd(int reqId)
        {
            DiscardImplementation(reqId);
            throw new NotImplementedException();
        }

        public void softDollarTiers(int reqId, SoftDollarTier[] tiers)
        {
            DiscardImplementation(reqId, tiers);
            throw new NotImplementedException();
        }

        public void familyCodes(FamilyCode[] familyCodes)
        {
            DiscardImplementation(familyCodes);
            throw new NotImplementedException();
        }

        public void symbolSamples(int reqId, ContractDescription[] contractDescriptions)
        {
            DiscardImplementation(reqId, contractDescriptions);
            throw new NotImplementedException();
        }

        public void mktDepthExchanges(DepthMktDataDescription[] depthMktDataDescriptions)
        {
            DiscardImplementation(depthMktDataDescriptions);
            throw new NotImplementedException();
        }

        public void tickNews(int tickerId, long timeStamp, string providerCode, string articleId, string headline, string extraData)
        {
            DiscardImplementation(tickerId, timeStamp, providerCode, articleId, headline, extraData);
            throw new NotImplementedException();
        }

        public void smartComponents(int reqId, Dictionary<int, KeyValuePair<string, char>> theMap)
        {
            DiscardImplementation(reqId, theMap);
            throw new NotImplementedException();
        }

        public void tickReqParams(int tickerId, double minTick, string bboExchange, int snapshotPermissions)
        {
            DiscardImplementation(tickerId, minTick, bboExchange, snapshotPermissions);
            throw new NotImplementedException();
        }

        public void newsProviders(NewsProvider[] newsProviders)
        {
            DiscardImplementation(newsProviders);
            throw new NotImplementedException();
        }

        public void newsArticle(int requestId, int articleType, string articleText)
        {
            DiscardImplementation(requestId, articleType, articleText);
            throw new NotImplementedException();
        }

        public void historicalNews(int requestId, string time, string providerCode, string articleId, string headline)
        {
            DiscardImplementation(requestId, time, providerCode, articleId, headline);
            throw new NotImplementedException();
        }

        public void historicalNewsEnd(int requestId, bool hasMore)
        {
            DiscardImplementation(requestId, hasMore);
            throw new NotImplementedException();
        }

        public void headTimestamp(int reqId, string headTimestamp)
        {
            DiscardImplementation(reqId, headTimestamp);
            throw new NotImplementedException();
        }

        public void histogramData(int reqId, HistogramEntry[] data)
        {
            DiscardImplementation(reqId, data);
            throw new NotImplementedException();
        }

        public void rerouteMktDataReq(int reqId, int conId, string exchange)
        {
            DiscardImplementation(reqId, conId, exchange);
            throw new NotImplementedException();
        }

        public void rerouteMktDepthReq(int reqId, int conId, string exchange)
        {
            DiscardImplementation(reqId, conId, exchange);
            throw new NotImplementedException();
        }

        public void marketRule(int marketRuleId, PriceIncrement[] priceIncrements)
        {
            DiscardImplementation(marketRuleId, priceIncrements);
            throw new NotImplementedException();
        }

        public void pnl(int reqId, double dailyPnL, double unrealizedPnL, double realizedPnL)
        {
            DiscardImplementation(reqId, dailyPnL, unrealizedPnL, realizedPnL);
            throw new NotImplementedException();
        }

        public void pnlSingle(int reqId, decimal pos, double dailyPnL, double unrealizedPnL, double realizedPnL, double value)
        {
            DiscardImplementation(reqId, pos, dailyPnL, unrealizedPnL, realizedPnL, value);
            throw new NotImplementedException();
        }

        public void historicalTicks(int reqId, HistoricalTick[] ticks, bool done)
        {
            DiscardImplementation(reqId, ticks, done);
            throw new NotImplementedException();
        }

        public void historicalTicksBidAsk(int reqId, HistoricalTickBidAsk[] ticks, bool done)
        {
            DiscardImplementation(reqId, ticks, done);
            throw new NotImplementedException();
        }

        public void historicalTicksLast(int reqId, HistoricalTickLast[] ticks, bool done)
        {
            DiscardImplementation(reqId, ticks, done);
            throw new NotImplementedException();
        }

        public void tickByTickAllLast(int reqId, int tickType, long time, double price, decimal size, TickAttribLast tickAttribLast, string exchange, string specialConditions)
        {
            DiscardImplementation(reqId, tickType, time, price, size, tickAttribLast, exchange, specialConditions);
            throw new NotImplementedException();
        }

        public void tickByTickBidAsk(int reqId, long time, double bidPrice, double askPrice, decimal bidSize, decimal askSize, TickAttribBidAsk tickAttribBidAsk)
        {
            DiscardImplementation(reqId, time, bidPrice, askPrice, bidSize, askSize, tickAttribBidAsk);
            throw new NotImplementedException();
        }

        public void tickByTickMidPoint(int reqId, long time, double midPoint)
        {
            DiscardImplementation(reqId, time, midPoint);
            throw new NotImplementedException();
        }

        public void orderBound(long orderId, int apiClientId, int apiOrderId)
        {
            DiscardImplementation(orderId, apiClientId, apiOrderId);
            throw new NotImplementedException();
        }

        public void completedOrder(Contract contract, Order order, OrderState orderState)
        {
            DiscardImplementation(contract, order, orderState);
            throw new NotImplementedException();
        }

        public void completedOrdersEnd()
        {
            throw new NotImplementedException();
        }

        public void userInfo(int reqId, string userInfo)
        {
            DiscardImplementation(reqId, userInfo);
            throw new NotImplementedException();
        }

        public void replaceFAEnd(int reqId, string text)
        {
            DiscardImplementation(reqId, text);
            throw new NotImplementedException();
        }

        public void wshMetaData(int reqId, string data)
        {
            DiscardImplementation(reqId, data);
            throw new NotImplementedException();
        }

        public void wshEventData(int reqId, string data)
        {
            DiscardImplementation(reqId, data);
            throw new NotImplementedException();
        }

        public void historicalSchedule(int reqId, string startDateTime, string endDateTime, string timeZone, HistoricalSession[] sessions)
        {
            DiscardImplementation(reqId, startDateTime, endDateTime, timeZone, sessions);
            throw new NotImplementedException();
        }
    }
}
