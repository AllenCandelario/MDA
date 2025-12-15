using IBApi;
using MDA.Config;
using MDA.Model;
using System.Collections.Concurrent;

namespace MDA.Implementation
{
    public partial class IBClient : EWrapper
    {
        // multiple requests to subscribe to market data may happen, use a thread safe collection. Value (byte) is just dummy data
        private readonly ConcurrentDictionary<int, byte> _activeMarketDataRequestIds = new();

        public event Action<IBMarketData> MarketDataReceived;

        public void SubscribeToDelayedMarketDataType()
        {
            _clientSocket.reqMarketDataType(3);
        }

        public int SubscribeToMarketData(Contract contract, string commaSeparatedGenericTickList = "", bool snapshot = false, bool regulatorySnapshot = false, List<TagValue>? mktDataOptions = null)
        {   
            IBLogging.MarketDataSub(_logger, contract.Symbol, contract.ConId);
            if (!_activeMarketDataRequestIds.TryAdd(contract.ConId, 0))
            {
                IBLogging.MarketDataAlreadySub(_logger, contract.Symbol, contract.ConId);
            }
            else
            {
                _clientSocket.reqMktData(contract.ConId, contract, commaSeparatedGenericTickList, snapshot, regulatorySnapshot, mktDataOptions);
            }
            return contract.ConId;
        }

        public void CancelMarketData(int requestId)
        {
            if (_activeMarketDataRequestIds.TryRemove(requestId, out _))
            {
                IBLogging.MarketDataCancel(_logger, requestId);
                _clientSocket.cancelMktData(requestId);
            }
            else
            {
                IBLogging.MarketDataCancelError(_logger, requestId);
            }
        }

        public void CancelAllMarketData()
        {
            var requestIds = _activeMarketDataRequestIds.Keys.ToList();

            foreach (var reqId in requestIds)
            {
                CancelMarketData(reqId);
            }

            if (!_activeMarketDataRequestIds.IsEmpty)
            {
                var remainingIds = _activeMarketDataRequestIds.Keys.ToList();
                string remaining = string.Join(", ", remainingIds);
                IBLogging.MarketDataCancelAllRemaining(_logger, remaining);

                _activeMarketDataRequestIds.Clear();
            }
        }

        void EWrapper.marketDataType(int reqId, int marketDataType)
        {
            Console.WriteLine($"Market data type info: reqId: {reqId}, marketDataType: {marketDataType}");
        }

        void EWrapper.tickPrice(int tickerId, int field, double price, TickAttrib attribs)
        {
            var dto = new IBTickPrice(tickerId, field, price, attribs);
            MarketDataReceived?.Invoke(dto);
        }

        void EWrapper.tickSize(int tickerId, int field, decimal size)
        {
            var dto = new IBTickSize(tickerId, field, size);
            MarketDataReceived?.Invoke(dto);
        }

        void EWrapper.tickReqParams(int tickerId, double minTick, string bboExchange, int snapshotPermissions)
        {
            var dto = new IBTickReqParams(tickerId, minTick, bboExchange, snapshotPermissions);
            MarketDataReceived?.Invoke(dto);
        }

        void EWrapper.tickString(int tickerId, int tickType, string value)
        {
            var dto = new IBTickString(tickerId, tickType, value);
            MarketDataReceived?.Invoke(dto);
        }

        void EWrapper.tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints,
            double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate)
        {
            var dto = new IBTickEFP(tickerId, tickType, basisPoints, formattedBasisPoints, impliedFuture, holdDays, futureLastTradeDate, dividendImpact, dividendsToLastTradeDate);
            MarketDataReceived?.Invoke(dto);
        }

        void EWrapper.tickGeneric(int tickerId, int tickType, double value)
        {
            var dto = new IBTickGeneric(tickerId, tickType, value);
            MarketDataReceived?.Invoke(dto);
        }

        void EWrapper.tickOptionComputation(int tickerId, int tickType, int tickAttrib, double impliedVolatility, double delta,
            double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
        {
            var dto = new IBTickOptionComputation(tickerId, tickType, tickAttrib, impliedVolatility, delta, optPrice, pvDividend, gamma, vega, theta, undPrice);
            MarketDataReceived?.Invoke(dto);
        }

        public void tickSnapshotEnd(int tickerId)
        {
            var dto = new IBTickSnapshotEnd(tickerId);
            MarketDataReceived?.Invoke(dto);
        }

    }
}
