using MDA.Web.Application.Accounts.Contracts;
using MDA.Web.Application.Accounts.Interfaces;
using MDA.Web.Application.Holdings.Contracts;
using MDA.Web.Application.Holdings.Interfaces;
using MDA.Web.Domain.Accounts;

namespace MDA.Web.Application.Holdings.Services
{
    public sealed class HoldingQueries : IHoldingQueries
    {
        private readonly IHoldingRepository _holdingRepository;
        private readonly IAccountRepository _accountRepository;

        public HoldingQueries(IHoldingRepository holdingRepository, IAccountRepository accountRepository) 
        { 
            _holdingRepository = holdingRepository;
            _accountRepository = accountRepository;
        }
        
        public async Task<HoldingsResponse?> GetAllHoldingDetailsUnderAccount(Guid accountId, CancellationToken ct)
        {

            var account = await _accountRepository.GetByIdAsync(accountId, ct);
            if (account == null)
            {
                return null;
            }

            var fullHoldingDetails = await _holdingRepository.GetFullDetailsByAccountAsync(accountId, ct);

            if (fullHoldingDetails != null)
            {
                return HoldingsResponse.From(fullHoldingDetails, account.TotalPortfolioValue);
            }
            else
            {
                return null;
            }

        }
    }
}
