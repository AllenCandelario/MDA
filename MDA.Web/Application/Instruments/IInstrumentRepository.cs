using MDA.Web.Domain.Instruments;

namespace MDA.Web.Application.Instruments
{
    public interface IInstrumentRepository
    {
        Task<Instrument?> GetByIbkrConId(int ibkrConId, CancellationToken ct);
        Task AddAsync(Instrument instrument, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
