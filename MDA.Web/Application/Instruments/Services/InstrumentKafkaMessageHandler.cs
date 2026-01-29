using MDA.Web.API.Hubs;
using MDA.Web.Application.Accounts.Contracts;
using MDA.Web.Application.Instruments.Contracts;
using MDA.Web.Application.Instruments.Interfaces;
using MDA.Web.Application.Shared;
using MDA.Web.Domain.Accounts;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace MDA.Web.Application.Instruments.Services
{
    public sealed class InstrumentKafkaMessageHandler : IKafkaMessageHandler
    {
        private readonly ILogger<InstrumentKafkaMessageHandler> _logger;
        private readonly IHubContext<MDAHub> _hub;
        private readonly IInstrumentRepository _instrumentRepository;
        public InstrumentKafkaMessageHandler(ILogger<InstrumentKafkaMessageHandler> logger, IHubContext<MDAHub> hub, IInstrumentRepository instrumentRepository)
        {
            _logger = logger;
            _hub = hub;
            _instrumentRepository = instrumentRepository;
        }

        //public async Task HandleKafkaMessageAsync(string message, CancellationToken cancellationToken)
        //{
        //    //_logger.LogInformation(message);
        //    //await _hub.Clients.All.SendAsync("marketData", new
        //    //{
        //    //    key = "marketData",
        //    //    value = message
        //    //}, cancellationToken);

        //    var marketDataMessage = JsonSerializer.Deserialize<MarketDataKafkaMessage>(message);
        //    if (marketDataMessage == null)
        //    {
        //        // TODO: Proper logging
        //        _logger.LogWarning("Null market data message");
        //    }
        //    else
        //    {
        //        _logger.LogInformation($"Incoming MarketData message: {message}");
        //    }
        //}

        public async Task HandleKafkaMessageAsync(string key, string value, CancellationToken cancellationToken)
        {
            switch (key)
            {
                case "IBTickString":
                    var ibTickStringValue = JsonSerializer.Deserialize<IBTickString>(value);
                    if (ibTickStringValue == null)
                    {
                        // TODO: Proper logging 
                        _logger.LogWarning("Market data message --> Null IBTickString");
                    }
                    else
                    {
                        // To expand when we start handling more tick types, now we only care about PE ratio stuffs
                        if (ibTickStringValue.tickType == 47)
                        {
                            await HandleIBUpdateInstrumentPe(ibTickStringValue.tickerId, ibTickStringValue.value, cancellationToken);
                        }
                        else
                        {
                            // TODO: Proper logging 
                            _logger.LogInformation("Market data message --> Unhandled tickType for IBTickString");
                        }
                    }
                    break;
                case "IBTickPrice":
                    var ibTickPriceValue = JsonSerializer.Deserialize<IBTickPrice>(value);
                    if (ibTickPriceValue == null)
                    {
                        _logger.LogWarning("Market data message --> Null IBTickPrice");
                    }
                    else
                    {
                        // To expand when we start handling more field types, now we only care about last price and 52 week high
                        if (ibTickPriceValue.field == 20) // 52 week high
                        {
                            await HandleIBUpdateInstrument52WeekHigh(ibTickPriceValue.tickerId, ibTickPriceValue.price, cancellationToken);
                        }
                        else if (ibTickPriceValue.field == 68)
                        {
                            await HandleIBUpdateInstrumentLastPrice(ibTickPriceValue.tickerId, ibTickPriceValue.price, cancellationToken);
                        }
                        else if (ibTickPriceValue.field == 75)
                        {
                            await HandleIBUpdateInstrumentClosePrice(ibTickPriceValue.tickerId, ibTickPriceValue.price, cancellationToken);
                        }
                        else
                        {
                            // TODO: Proper logging 
                            _logger.LogInformation("Market data message --> Unhandled field for IBTickPrice");
                        }
                    }
                    break;
                default:
                    // TODO: Proper logging
                    _logger.LogDebug("Unhandled Market data key {Key}", key);
                    break;
            }
        }

        private async Task HandleIBUpdateInstrument52WeekHigh(int instrumentContractId, double price, CancellationToken ct)
        {
            var instrument = await _instrumentRepository.GetByIbkrConId(instrumentContractId, ct);
            if (instrument == null)
            {
                _logger.LogWarning("No Instrument found for contract Id {instrumentContractId}", instrumentContractId);
                return;
            }

            instrument.Update52WeekHigh((decimal)price);

            await _instrumentRepository.SaveChangesAsync(ct);
        }

        private async Task HandleIBUpdateInstrumentLastPrice(int instrumentContractId, double price, CancellationToken ct)
        {
            var instrument = await _instrumentRepository.GetByIbkrConId(instrumentContractId, ct);
            if (instrument == null)
            {
                _logger.LogWarning("No Instrument found for contract Id {instrumentContractId}", instrumentContractId);
                return;
            }

            instrument.UpdateLastPrice((decimal)price);

            await _instrumentRepository.SaveChangesAsync(ct);
        }

        private async Task HandleIBUpdateInstrumentClosePrice(int instrumentContractId, double price, CancellationToken ct)
        {
            var instrument = await _instrumentRepository.GetByIbkrConId(instrumentContractId, ct);
            if (instrument == null)
            {
                _logger.LogWarning("No Instrument found for contract Id {instrumentContractId}", instrumentContractId);
                return;
            }

            if (!instrument.LastPrice.HasValue || instrument.LastPrice.Value == 0m)
            {
                _logger.LogInformation("Outside of regular trading hours, instrument's last price recorded as 0, updating with closing price");
                instrument.UpdateLastPrice(lastPrice: (decimal)price);
                await _instrumentRepository.SaveChangesAsync(ct);
            }
        }

        private async Task HandleIBUpdateInstrumentPe(int instrumentContractId, string raw, CancellationToken ct)
        {
            var instrument = await _instrumentRepository.GetByIbkrConId(instrumentContractId, ct);
            if (instrument == null)
            {
                _logger.LogWarning("No Instrument found for contract Id {instrumentContractId}", instrumentContractId);
                return;
            }

            var dict = ParseKeyValue(raw);
            decimal? pe = null;
            decimal? forwardEpsDecimal = null;
            decimal? priceDecimal = null;
            decimal? forwardPe = null;

            if (!dict.TryGetValue("PEEXCLXOR", out var peRaw))
            {
                _logger.LogInformation("Market data message --> Unable to retrieve pe value");
            }
            else
            {
                pe = ParseReutersNullableDecimal(peRaw);
            }

            if (!dict.TryGetValue("AFEEPSNTM", out var forwardEpsRaw) || !dict.TryGetValue("NPRICE", out var priceRaw))
            {
                _logger.LogInformation("Market data message --> Unable to retrieve details for forward pe calculation");
            }
            else
            {

                forwardEpsDecimal = ParseReutersNullableDecimal(forwardEpsRaw);
                priceDecimal = ParseReutersNullableDecimal(priceRaw);
                if (forwardEpsDecimal != null && priceDecimal != null && forwardEpsDecimal > 0m && priceDecimal > 0m)
                {
                    forwardPe = decimal.Round(priceDecimal.Value / forwardEpsDecimal.Value, 8, MidpointRounding.AwayFromZero);
                }
                
            }

            instrument.UpdatePeRatios(pe, forwardPe);
            await _instrumentRepository.SaveChangesAsync(ct);
        }

        /* Method to split raw string for ticker 47
         * Sample : TTMNPMGN=43.63164;NLOW=134.25;TTMPRCFPS=16.7723;....
         * */
        private Dictionary<string, string> ParseKeyValue(string raw)
        {
            Dictionary<string, string> dict = new(StringComparer.OrdinalIgnoreCase); // ignore case

            // Split via semicolon
            var parts = raw.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            
            // Identify = sign and split into key value pairs
            foreach (var part in parts )
            {
                var equalSignIndex = part.IndexOf('=');

                // ignore if the index is not right
                if (equalSignIndex <= 0 || equalSignIndex == part.Length - 1)
                {
                    continue;
                }
                string key = part.Substring(0, equalSignIndex);
                string value = part.Substring(equalSignIndex + 1);
                dict[key] = value;
            }

            return dict;
        }

        // IBKR utilizes data from retuers for fundamental data like p/e ratios. Missing data will result in -99999.99 values, so we need to handle it
        private static decimal? ParseReutersNullableDecimal(string s)
        {
            s = s.Trim();

            if (string.IsNullOrWhiteSpace(s))
            {
                return null;
            }

            if (s == "-99999.99" || s == "-99999.9" || s == "N/A")
            {
                return null;
            }

            if (decimal.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var val))
            {
                return val;
            }

            return null;
        }
    }
}
