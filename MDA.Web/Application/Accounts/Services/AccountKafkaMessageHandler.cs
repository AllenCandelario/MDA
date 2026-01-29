using MDA.Web.Application.Accounts.Contracts;
using MDA.Web.Application.Accounts.Interfaces;
using MDA.Web.Application.Holdings.Interfaces;
using MDA.Web.Application.Instruments.Interfaces;
using MDA.Web.Application.Shared;
using MDA.Web.Domain.Accounts;
using MDA.Web.Domain.Holdings;
using MDA.Web.Domain.Instruments;
using System.Text.Json;

namespace MDA.Web.Application.Accounts.Services
{
    public sealed class AccountKafkaMessageHandler : IKafkaMessageHandler
    {
        private readonly ILogger<AccountKafkaMessageHandler> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IHoldingRepository _holdingRepository;
        private readonly IInstrumentRepository _instrumentRepository;
        public AccountKafkaMessageHandler(ILogger<AccountKafkaMessageHandler> logger, IAccountRepository accountRepository, IHoldingRepository holdingRepository, IInstrumentRepository instrumentRepository)
        {
            // TODO: Proper logging
            _logger = logger;
            _accountRepository = accountRepository;
            _holdingRepository = holdingRepository;
            _instrumentRepository = instrumentRepository;
        }

        public async Task HandleKafkaMessageAsync(string key, string value, CancellationToken cancellationToken)
        {
            
            switch (key)
            {
                case "IBUpdateAccountValue":
                    var ibUpdateAccountValue = JsonSerializer.Deserialize<IBUpdateAccountValue>(value);
                    if (ibUpdateAccountValue == null)
                    {
                        // TODO: Proper logging
                        _logger.LogWarning("Account update message --> Null IBUpdateAccountValue");
                    }
                    else
                    {
                        await HandleIBUpdateAccountValue(ibUpdateAccountValue, cancellationToken);
                    }
                    break;
                case "IBUpdatePortfolio":
                    {
                        var portfolioPayload = JsonSerializer.Deserialize<IBUpdatePortfolio>(value);
                        if (portfolioPayload == null)
                        {
                            _logger.LogWarning("Account update message --> Null IBUpdatePortfolio");
                            return;
                        }

                        await HandleIBUpdatePortfolio(portfolioPayload, cancellationToken);
                        break;
                    }
                default:
                    // TODO: Proper logging
                    _logger.LogDebug("Unhandled AccountUpdate key {Key}", key);
                    break;
            }
        }

        private async Task HandleIBUpdateAccountValue(IBUpdateAccountValue ibUpdateAccountValue, CancellationToken ct)
        {

            if (string.IsNullOrWhiteSpace(ibUpdateAccountValue.AccountName))
            {
                _logger.LogWarning("IBUpdateAccountValue has empty AccountName");
                return;
            }

            var account = await _accountRepository.GetByIbkrAccountIdAsync(ibUpdateAccountValue.AccountName, ct);
            if (account == null)
            {
                _logger.LogWarning("No Account found for IbkrAccountId {IbkrAccountId}", ibUpdateAccountValue.AccountName);
                return;
            }

            switch (ibUpdateAccountValue.Key)
            {
                case "NetLiquidation":
                    if (decimal.TryParse(ibUpdateAccountValue.Value, out var netLiq))
                    {
                        account.TotalPortfolioValue = netLiq;
                    }
                    else
                    {
                        _logger.LogWarning("Failed to parse NetLiquidation value {Value}", ibUpdateAccountValue.Value);
                    }
                    break;
                case "CashBalance":
                    if (decimal.TryParse(ibUpdateAccountValue.Value, out var cashBal))
                    {
                        account.SettledCash = cashBal;
                    }
                    else
                    {
                        _logger.LogWarning("Failed to parse CashBalance value {Value}", ibUpdateAccountValue.Value);
                    }
                    break;
                case "ExcessLiquidity":
                    if (decimal.TryParse(ibUpdateAccountValue.Value, out var excessLiquidity))
                    {
                        account.ExcessLiquidity = excessLiquidity;
                    }
                    else
                    {
                        _logger.LogWarning("Failed to parse ExcessLiquidity value {Value}", ibUpdateAccountValue.Value);
                    }
                    break;
                case "BuyingPower":
                    if (decimal.TryParse(ibUpdateAccountValue.Value, out var buyingPower))
                    {
                        account.BuyingPower = buyingPower;
                    }
                    else
                    {
                        _logger.LogWarning("Failed to parse BuyingPower value {Value}", ibUpdateAccountValue.Value);
                    }
                    break;
                case "UnrealizedPnL":
                    if (decimal.TryParse(ibUpdateAccountValue.Value, out var unrealizedPnL))
                    {
                        account.UnrealizedPnl = unrealizedPnL;
                    }
                    else
                    {
                        _logger.LogWarning("Failed to parse UnrealizedPnL value {Value}", ibUpdateAccountValue.Value);
                    }
                    break;
                default:
                    _logger.LogInformation("Key not tracked {Key}", ibUpdateAccountValue.Key);
                    break;
            }

            account.LastUpdatedUtc = DateTime.UtcNow;
            await _accountRepository.SaveChangesAsync(ct);
        }

        private async Task HandleIBUpdatePortfolio(IBUpdatePortfolio ibUpdatePortfolio, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(ibUpdatePortfolio.AccountName))
            {
                // TODO: Proper logging
                _logger.LogWarning("IBUpdatePortfolio has empty AccountName");
                return;
            }

            var account = await _accountRepository.GetByIbkrAccountIdIncludeHoldingsAsync(ibUpdatePortfolio.AccountName, ct);
            if (account == null)
            {
                // TODO: Proper logging
                _logger.LogWarning("No Account found for IbkrAccountId {IbkrAccountId} when handling portfolio update", ibUpdatePortfolio.AccountName);
                return;
            }

            var contract = ibUpdatePortfolio.Contract;

            // 1) Find or create Instrument
            var instrument = await _instrumentRepository.GetByIbkrConId(contract.ConId, ct);
            if (instrument == null)
            {
                var instrumentName = string.IsNullOrWhiteSpace(contract.Description) ? contract.Symbol : contract.Description;

                instrument = new Instrument(contract.Symbol, instrumentName,contract.ConId, contract.SecType);

                await _instrumentRepository.AddAsync(instrument, ct);
            }


            // 2) Find or create Holding for this account + instrument
            var holding = account.Holdings.FirstOrDefault(h => h.InstrumentId == instrument.Id);

            if (holding == null)
            {
                holding = new Holding(account.Id, instrument.Id, null, ibUpdatePortfolio.Position, (decimal)ibUpdatePortfolio.AverageCost);
                await _holdingRepository.AddAsync(holding, ct);
            }
            else
            {
                holding.UpdatePosition(ibUpdatePortfolio.Position, (decimal)ibUpdatePortfolio.AverageCost, (decimal)ibUpdatePortfolio.UnrealizedPNL, (decimal)ibUpdatePortfolio.RealizedPNL);
            }

            account.LastUpdatedUtc = DateTime.UtcNow;

            await _accountRepository.SaveChangesAsync(ct);
        }
    }
}
