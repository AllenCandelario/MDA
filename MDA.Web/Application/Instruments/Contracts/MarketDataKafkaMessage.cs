namespace MDA.Web.Application.Instruments.Contracts
{
    public sealed record IBTickPrice(int tickerId, int field, double price, IBTickAttrib attribs);

    public sealed record IBTickSize(int tickerId, int field, decimal size);

    public sealed record IBTickReqParams(int tickerId, double minTick, string bboExchange, int snapshotPermissions);

    public sealed record IBTickString(int tickerId, int tickType, string value);

    public sealed record IBTickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate);

    public sealed record IBTickGeneric(int tickerId, int tickType, double value);

    public sealed record IBTickOptionComputation(int tickerId, int tickType, int tickAttrib, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice);

    public sealed record IBTickSnapshotEnd(int tickerId);


    // IBKR.API objects
    public sealed record IBTickAttrib (bool CanAutoExecute, bool PastLimit, bool PreOpen, bool Unreported, bool BidPastLow, bool AskPastHigh);
}
