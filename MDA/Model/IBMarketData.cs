using IBApi;

namespace MDA.Model
{
    // Polymorphic handle for all market data
    public abstract record IBMarketData;

    public sealed record IBTickPrice(int tickerId, int field, double price, TickAttrib attribs) : IBMarketData;

    public sealed record IBTickSize(int tickerId, int field, decimal size) : IBMarketData;

    public sealed record IBTickReqParams(int tickerId, double minTick, string bboExchange, int snapshotPermissions) : IBMarketData;

    public sealed record IBTickString(int tickerId, int tickType, string value) : IBMarketData;

    public sealed record IBTickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate) : IBMarketData;

    public sealed record IBTickGeneric(int tickerId, int tickType, double value) : IBMarketData;

    public sealed record IBTickOptionComputation(int tickerId, int tickType, int tickAttrib, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice) : IBMarketData;

    public sealed record IBTickSnapshotEnd(int tickerId) : IBMarketData;
}
