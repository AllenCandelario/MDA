using MDA.Web.Application.Holdings;
using MDA.Web.Application.Holdings.Interfaces;
using MDA.Web.Domain.Holdings;
using Microsoft.EntityFrameworkCore;

namespace MDA.Web.Infrastructure.Persistence.Repositories
{
    public sealed class HoldingRepository : IHoldingRepository
    {
        private readonly MdaDbContext _db;

        public HoldingRepository(MdaDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<IReadOnlyList<Holding>?> GetFullDetailsByAccountAsync(Guid accountId, CancellationToken ct)
        {
            return await _db.Holdings
                .Where(h => h.AccountId == accountId)
                .Include(h => h.Instrument)
                .Include(h => h.Category)
                .ToListAsync(ct);
        }

        public async Task<Holding?> GetByAccountAndInstrumentAsync(Guid accountId, Guid instrumentId, CancellationToken ct)
        {
            return await _db.Holdings.SingleOrDefaultAsync(h => h.AccountId == accountId && h.InstrumentId == instrumentId, ct);
        }

        public async Task AddAsync(Holding holding, CancellationToken ct)
        {
            await _db.Holdings.AddAsync(holding, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
