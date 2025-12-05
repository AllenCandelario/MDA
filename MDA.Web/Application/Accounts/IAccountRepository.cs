using MDA.Web.Domain.Accounts;

namespace MDA.Web.Application.Accounts
{
    public interface IAccountRepository
    {
        //GET 
        Task<Account?> GetByIbkrAccountIdAsync(string ibkrAccountId, CancellationToken ct);
        Task AddAsync(Account account, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
