using IBApi;

namespace MDA.Implementation
{
    public partial class IBClient : EWrapper
    {
        private static void DiscardImplementation(params object[] inputs)
        {
            _ = inputs;
        }

        public void currentTime(long time)
        {
            DiscardImplementation(time);
            throw new NotImplementedException("currentTime");
        }

        public void deltaNeutralValidation(int reqId, DeltaNeutralContract deltaNeutralContract)
        {
            DiscardImplementation(reqId, deltaNeutralContract);
            throw new NotImplementedException("deltaNeutralValidation");
        }

        public void bondContractDetails(int reqId, ContractDetails contractDetails)
        {
            DiscardImplementation(reqId, contractDetails);
            throw new NotImplementedException("bondContractDetails");
        }

        public void orderStatus(int orderId, string status, decimal filled, decimal remaining, double avgFillPrice,
            int permId, int parentId, double lastFillPrice, int clientId, string whyHeld, double mktCapPrice)
        {
            DiscardImplementation(orderId, status, filled, remaining, avgFillPrice, permId, parentId, lastFillPrice, clientId, whyHeld, mktCapPrice);
            throw new NotImplementedException("orderStatus");
        }

        public void openOrder(int orderId, Contract contract, Order order, OrderState orderState)
        {
            DiscardImplementation(orderId, contract, order, orderState);
            throw new NotImplementedException("openOrder");
        }

        public void openOrderEnd()
        {
            throw new NotImplementedException("openOrderEnd");
        }

        public void contractDetails(int reqId, ContractDetails contractDetails)
        {
            DiscardImplementation(reqId, contractDetails);
            throw new NotImplementedException("contractDetails");
        }

        public void contractDetailsEnd(int reqId)
        {
            DiscardImplementation(reqId);
            throw new NotImplementedException("contractDetailsEnd");
        }

        public void execDetails(int reqId, Contract contract, Execution execution)
        {
            DiscardImplementation(reqId, contract, execution);
            throw new NotImplementedException("execDetails");
        }

        public void execDetailsEnd(int reqId)
        {
            DiscardImplementation(reqId);
            throw new NotImplementedException("execDetailsEnd");
        }

        public void commissionReport(CommissionReport commissionReport)
        {
            DiscardImplementation(commissionReport);
            throw new NotImplementedException("commissionReport");
        }

        public void fundamentalData(int reqId, string data)
        {
            DiscardImplementation(reqId, data);
            throw new NotImplementedException("funadamentalData");
        }

        public void historicalData(int reqId, Bar bar)
        {
            DiscardImplementation(reqId, bar);
            throw new NotImplementedException("historicalData");
        }

        public void historicalDataUpdate(int reqId, Bar bar)
        {
            DiscardImplementation(reqId, bar);
            throw new NotImplementedException("historicalDataUpdate");
        }

        public void historicalDataEnd(int reqId, string startDate, string endDate)
        {
            DiscardImplementation(reqId, startDate, endDate);
            throw new NotImplementedException("historicalDataEnd");
        }

        public void updateMktDepth(int tickerId, int position, int operation, int side, double price, decimal size)
        {
            DiscardImplementation(tickerId, position, operation, side, price, size);
            throw new NotImplementedException("updateMktDepth");
        }

        public void updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, decimal size, bool isSmartDepth)
        {
            DiscardImplementation(tickerId, position, marketMaker, operation, side, price, size, isSmartDepth);
            throw new NotImplementedException("updateMktDepthL2");
        }

        public void updateNewsBulletin(int msgId, int msgType, string message, string origin)
        {
            DiscardImplementation(msgId, msgType, message, origin);
            throw new NotImplementedException("updateNewsBulletin");
        }

        public void position(string account, Contract contract, decimal position, double avgCost)
        {
            DiscardImplementation(account, contract, position, avgCost);
            throw new NotImplementedException("position");
        }

        public void positionEnd()
        {
            throw new NotImplementedException("positionEnd");
        }

        public void realtimeBar(int reqId, long time, double open, double high, double low, double close, decimal volume, decimal wap, int count)
        {
            DiscardImplementation(reqId, time, open, high, low, close, volume, wap, count);
            throw new NotImplementedException("realtimeBar");
        }

        public void scannerParameters(string xml)
        {
            DiscardImplementation(xml);
            throw new NotImplementedException("scannerParameters");
        }

        public void scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
        {
            DiscardImplementation(reqId, rank, contractDetails, distance, benchmark, projection, legsStr);
            throw new NotImplementedException("scannerData");
        }

        public void scannerDataEnd(int reqId)
        {
            DiscardImplementation(reqId);
            throw new NotImplementedException("scannerDataEnd");
        }

        public void receiveFA(int faDataType, string xml)
        {
            DiscardImplementation(faDataType, xml);
            throw new NotImplementedException("receiveFA");
        }

        public void verifyMessageAPI(string apiData)
        {
            DiscardImplementation(apiData);
            throw new NotImplementedException("verifyMessageAPI");
        }

        public void verifyCompleted(bool isSuccessful, string errorText)
        {
            DiscardImplementation(isSuccessful, errorText);
            throw new NotImplementedException("verifyCompleted");
        }

        public void verifyAndAuthMessageAPI(string apiData, string xyzChallenge)
        {
            DiscardImplementation(apiData, xyzChallenge);
            throw new NotImplementedException("verifyAndAuthMessageAPI");
        }

        public void verifyAndAuthCompleted(bool isSuccessful, string errorText)
        {
            DiscardImplementation(isSuccessful, errorText);
            throw new NotImplementedException("verifyAndAuthCompleted");
        }

        public void displayGroupList(int reqId, string groups)
        {
            DiscardImplementation(reqId, groups);
            throw new NotImplementedException("displayGroupList");
        }

        public void displayGroupUpdated(int reqId, string contractInfo)
        {
            DiscardImplementation(reqId, contractInfo);
            throw new NotImplementedException("displayGroupUpdated");
        }

        public void positionMulti(int reqId, string account, string modelCode, Contract contract, decimal pos, double avgCost)
        {
            DiscardImplementation(reqId, account, modelCode, contract, pos, avgCost);
            throw new NotImplementedException("positionMulti");
        }

        public void positionMultiEnd(int reqId)
        {
            DiscardImplementation(reqId);
            throw new NotImplementedException("positionMultiEnd");
        }

        public void accountUpdateMulti(int reqId, string account, string modelCode, string key, string value, string currency)
        {
            DiscardImplementation(reqId, account, modelCode, key, value, currency);
            throw new NotImplementedException("accountUpdateMulti");
        }

        public void accountUpdateMultiEnd(int reqId)
        {
            DiscardImplementation(reqId);
            throw new NotImplementedException("accountUPdateMultiEnd");
        }

        public void securityDefinitionOptionParameter(int reqId, string exchange, int underlyingConId, string tradingClass, string multiplier, HashSet<string> expirations, HashSet<double> strikes)
        {
            DiscardImplementation(reqId, exchange, underlyingConId, tradingClass, multiplier, expirations, strikes);
            throw new NotImplementedException("securityDefinitionOptionParameter");
        }

        public void securityDefinitionOptionParameterEnd(int reqId)
        {
            DiscardImplementation(reqId);
            throw new NotImplementedException("secrutiyDefinitionOptionParameterEnd");
        }

        public void softDollarTiers(int reqId, SoftDollarTier[] tiers)
        {
            DiscardImplementation(reqId, tiers);
            throw new NotImplementedException("softDollarTiers");
        }

        public void familyCodes(FamilyCode[] familyCodes)
        {
            DiscardImplementation(familyCodes);
            throw new NotImplementedException("familyCodes");
        }

        public void symbolSamples(int reqId, ContractDescription[] contractDescriptions)
        {
            DiscardImplementation(reqId, contractDescriptions);
            throw new NotImplementedException("symbolSamples");
        }

        public void mktDepthExchanges(DepthMktDataDescription[] depthMktDataDescriptions)
        {
            DiscardImplementation(depthMktDataDescriptions);
            throw new NotImplementedException("mktDepthExchanges");
        }

        public void tickNews(int tickerId, long timeStamp, string providerCode, string articleId, string headline, string extraData)
        {
            DiscardImplementation(tickerId, timeStamp, providerCode, articleId, headline, extraData);
            throw new NotImplementedException("tickNews");
        }

        public void smartComponents(int reqId, Dictionary<int, KeyValuePair<string, char>> theMap)
        {
            DiscardImplementation(reqId, theMap);
            throw new NotImplementedException("smartComponents");
        }

        public void newsProviders(NewsProvider[] newsProviders)
        {
            DiscardImplementation(newsProviders);
            throw new NotImplementedException("newsProviders");
        }

        public void newsArticle(int requestId, int articleType, string articleText)
        {
            DiscardImplementation(requestId, articleType, articleText);
            throw new NotImplementedException("newsArticle");
        }

        public void historicalNews(int requestId, string time, string providerCode, string articleId, string headline)
        {
            DiscardImplementation(requestId, time, providerCode, articleId, headline);
            throw new NotImplementedException("historicalNews");
        }

        public void historicalNewsEnd(int requestId, bool hasMore)
        {
            DiscardImplementation(requestId, hasMore);
            throw new NotImplementedException("historicalNewsEnd");
        }

        public void headTimestamp(int reqId, string headTimestamp)
        {
            DiscardImplementation(reqId, headTimestamp);
            throw new NotImplementedException("headTimestamp");
        }

        public void histogramData(int reqId, HistogramEntry[] data)
        {
            DiscardImplementation(reqId, data);
            throw new NotImplementedException("histogramData");
        }

        public void rerouteMktDataReq(int reqId, int conId, string exchange)
        {
            DiscardImplementation(reqId, conId, exchange);
            throw new NotImplementedException("rerouteMktDataReq");
        }

        public void rerouteMktDepthReq(int reqId, int conId, string exchange)
        {
            DiscardImplementation(reqId, conId, exchange);
            throw new NotImplementedException("rerouteMktDepthReq");
        }

        public void marketRule(int marketRuleId, PriceIncrement[] priceIncrements)
        {
            DiscardImplementation(marketRuleId, priceIncrements);
            throw new NotImplementedException("marketRule");
        }

        public void pnl(int reqId, double dailyPnL, double unrealizedPnL, double realizedPnL)
        {
            DiscardImplementation(reqId, dailyPnL, unrealizedPnL, realizedPnL);
            throw new NotImplementedException("pnl");
        }

        public void pnlSingle(int reqId, decimal pos, double dailyPnL, double unrealizedPnL, double realizedPnL, double value)
        {
            DiscardImplementation(reqId, pos, dailyPnL, unrealizedPnL, realizedPnL, value);
            throw new NotImplementedException("pnlSingle");
        }

        public void historicalTicks(int reqId, HistoricalTick[] ticks, bool done)
        {
            DiscardImplementation(reqId, ticks, done);
            throw new NotImplementedException("historicalTicks");
        }

        public void historicalTicksBidAsk(int reqId, HistoricalTickBidAsk[] ticks, bool done)
        {
            DiscardImplementation(reqId, ticks, done);
            throw new NotImplementedException("historicalTicksBidAsk");
        }

        public void historicalTicksLast(int reqId, HistoricalTickLast[] ticks, bool done)
        {
            DiscardImplementation(reqId, ticks, done);
            throw new NotImplementedException("historicalTicksLast");
        }

        public void tickByTickAllLast(int reqId, int tickType, long time, double price, decimal size, TickAttribLast tickAttribLast, string exchange, string specialConditions)
        {
            DiscardImplementation(reqId, tickType, time, price, size, tickAttribLast, exchange, specialConditions);
            throw new NotImplementedException("tickByTickAllLast");
        }

        public void tickByTickBidAsk(int reqId, long time, double bidPrice, double askPrice, decimal bidSize, decimal askSize, TickAttribBidAsk tickAttribBidAsk)
        {
            DiscardImplementation(reqId, time, bidPrice, askPrice, bidSize, askSize, tickAttribBidAsk);
            throw new NotImplementedException("tickByTickBidAsk");
        }

        public void tickByTickMidPoint(int reqId, long time, double midPoint)
        {
            DiscardImplementation(reqId, time, midPoint);
            throw new NotImplementedException("tickByTickMidPoint");
        }

        public void orderBound(long orderId, int apiClientId, int apiOrderId)
        {
            DiscardImplementation(orderId, apiClientId, apiOrderId);
            throw new NotImplementedException("orderBound");
        }

        public void completedOrder(Contract contract, Order order, OrderState orderState)
        {
            DiscardImplementation(contract, order, orderState);
            throw new NotImplementedException("completedOrder");
        }

        public void completedOrdersEnd()
        {
            throw new NotImplementedException("completedOrdersEnd");
        }

        public void userInfo(int reqId, string userInfo)
        {
            DiscardImplementation(reqId, userInfo);
            throw new NotImplementedException("userInfo");
        }

        public void replaceFAEnd(int reqId, string text)
        {
            DiscardImplementation(reqId, text);
            throw new NotImplementedException("replaceFAEnd");
        }

        public void wshMetaData(int reqId, string data)
        {
            DiscardImplementation(reqId, data);
            throw new NotImplementedException("wshMetaData");
        }

        public void wshEventData(int reqId, string data)
        {
            DiscardImplementation(reqId, data);
            throw new NotImplementedException("wshEventData");
        }

        public void historicalSchedule(int reqId, string startDateTime, string endDateTime, string timeZone, HistoricalSession[] sessions)
        {
            DiscardImplementation(reqId, startDateTime, endDateTime, timeZone, sessions);
            throw new NotImplementedException("historicalSchedule");
        }
    }
}
