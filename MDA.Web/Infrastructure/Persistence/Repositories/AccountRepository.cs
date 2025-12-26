using MDA.Web.Application.Accounts.Interfaces;
using MDA.Web.Domain.Accounts;
using MDA.Web.Domain.Holdings;
using Microsoft.EntityFrameworkCore;

namespace MDA.Web.Infrastructure.Persistence.Repositories
{
    public sealed class AccountRepository : IAccountRepository
    {
        private readonly MdaDbContext _db;

        public AccountRepository(MdaDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<Account?> GetByIbkrAccountIdAsync(string ibkrAccountId, CancellationToken ct)
        {
            return await _db.Accounts.SingleOrDefaultAsync(a => a.IbkrAccountId == ibkrAccountId);
        }

        public async Task<Account?> GetByIdAsync(Guid accountId, CancellationToken ct)
        {
            return await _db.Accounts.SingleOrDefaultAsync(a => a.Id == accountId, ct);
        }

        // Consider removing if this is not used 
        private async Task<Account?> GetFullDetailsByIbkrAccountIdAsync(string ibkrAccountId, CancellationToken ct)
        {
            return await _db.Accounts
                .Include(a => a.Holdings)
                .ThenInclude(h => h.Instrument)
                .SingleOrDefaultAsync(a => a.IbkrAccountId == ibkrAccountId, ct);
        }

        public async Task AddAsync(Account account, CancellationToken ct)
        {
            await _db.Accounts.AddAsync(account, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
