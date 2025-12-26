using MDA.Web.API.DTOs;
using MDA.Web.Application.Accounts.Contracts;
using MDA.Web.Application.Accounts.Interfaces;
using MDA.Web.Domain.Accounts;
using System.Data;
using System.Security.Claims;

namespace MDA.Web.Application.Accounts.Services
{
    public sealed class AccountQueries : IAccountQueries
    {

        private readonly IAccountRepository _accountRepository;

        public AccountQueries(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountResponse?> GetAccountAsync(Guid accountId, CancellationToken ct)
        {
            var account = await _accountRepository.GetByIdAsync(accountId, ct);
            if (account != null)
            {
                return AccountResponse.From(account);
            }
            else
            {
                return null;
            }
        }

        public async Task<AccountSummaryResponse?> GetAccountSummaryAsync(Guid accountId, CancellationToken ct)
        {
            var account = await _accountRepository.GetByIdAsync(accountId, ct);
            if (account != null)
            {
                return AccountSummaryResponse.From(account);
            }
            else
            {
                return null;
            }
        }

        public async Task<Account> GetActiveAccountForUserAsync(ClaimsPrincipal user, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
