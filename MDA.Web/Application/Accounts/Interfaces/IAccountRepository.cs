using MDA.Web.Domain.Accounts;

namespace MDA.Web.Application.Accounts.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> GetByIbkrAccountIdAsync(string ibkrAccountId, CancellationToken ct);
        Task<Account?> GetByIbkrAccountIdIncludeHoldingsAsync(string ibkrAccountId, CancellationToken ct);
        Task<Account?> GetByIdAsync(Guid accountId, CancellationToken ct);
        Task AddAsync(Account account, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
