using MDA.Web.Domain.Accounts;
using MDA.Web.Domain.Holdings;

namespace MDA.Web.Application.Holdings
{
    public interface IHoldingRepository
    {
        Task<Holding?> GetByAccountAndInstrumentAsync(Guid accountId, Guid instrumentId, CancellationToken ct);
        Task AddAsync(Holding holding, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
