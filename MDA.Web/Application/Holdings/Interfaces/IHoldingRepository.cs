using MDA.Web.Domain.Accounts;
using MDA.Web.Domain.Holdings;

namespace MDA.Web.Application.Holdings.Interfaces
{
    public interface IHoldingRepository
    {
        Task<IReadOnlyList<Holding>?> GetFullDetailsByAccountAsync(Guid accountId, CancellationToken ct);
        Task<Holding?> GetByAccountAndInstrumentAsync(Guid accountId, Guid instrumentId, CancellationToken ct);
        Task AddAsync(Holding holding, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
