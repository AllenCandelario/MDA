using MDA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;

namespace MDA.Model
{
    // Polymorphic handle for all account update data
    public abstract record IBAccountUpdate;

    // updateAccountValue 
    public sealed record IBUpdateAccountValue(string Key, string Value, string Currency, string AccountName) : IBAccountUpdate;

    // updatePortfolio
    public sealed record IBUpdatePortfolio(Contract Contract, decimal Position, double MarketPrice, double MarketValue,
            double AverageCost, double UnrealizedPNL, double RealizedPNL, string AccountName) : IBAccountUpdate;

    // updateAccountTime
    public sealed record IBUpdateAccountTime(string Timestamp) : IBAccountUpdate;

    // accountDownloadEnd
    public sealed record IBAccountDownloadEnd(string Account) : IBAccountUpdate;
}

/* The above implementations of the IBAccountUpdate abstract classes are the short-handed versions. The explicit declarations will look like the below */

//public sealed record AccountValueUpdate : AccountUpdate
//{
//    public string Key { get; init; }
//    public string Value { get; init; }
//    public string Currency { get; init; }
//    public string AccountName { get; init; }

//    public AccountValueUpdate(string key, string value, string currency, string accountName)
//    {
//        Key = key;
//        Value = value;
//        Currency = currency;
//        AccountName = accountName;
//    }
//}