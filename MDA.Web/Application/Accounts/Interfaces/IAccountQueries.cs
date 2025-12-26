using MDA.Web.Application.Accounts.Contracts;
using MDA.Web.Domain.Accounts;
using System.Security.Claims;

namespace MDA.Web.Application.Accounts.Interfaces
{
    public interface IAccountQueries
    {
        Task<AccountResponse?> GetAccountAsync(Guid accountId, CancellationToken ct);
        Task<Account> GetActiveAccountForUserAsync(ClaimsPrincipal user, CancellationToken ct);
        Task<AccountSummaryResponse?> GetAccountSummaryAsync(Guid accountId, CancellationToken ct);
    }
}
