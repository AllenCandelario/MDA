using MDA.Web.Application.Instruments.Interfaces;
using MDA.Web.Domain.Instruments;
using Microsoft.EntityFrameworkCore;

namespace MDA.Web.Infrastructure.Persistence.Repositories
{
    public sealed class InstrumentRepository : IInstrumentRepository
    {
        private readonly MdaDbContext _db;
        public InstrumentRepository(MdaDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<Instrument?> GetByIbkrConId(int ibkrConId, CancellationToken ct)
        {
            return await _db.Instruments.SingleOrDefaultAsync(i => i.IbkrConId == ibkrConId, ct);
        }
        public async Task AddAsync(Instrument instrument, CancellationToken ct)
        {
            await _db.Instruments.AddAsync(instrument, ct);
        }
        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct); 
        }
    }
}
