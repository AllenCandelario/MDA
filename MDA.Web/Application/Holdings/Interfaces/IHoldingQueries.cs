using MDA.Web.Application.Accounts.Contracts;
using MDA.Web.Application.Holdings.Contracts;

namespace MDA.Web.Application.Holdings.Interfaces
{
    public interface IHoldingQueries
    {
        Task<HoldingsResponse?> GetAllHoldingDetailsUnderAccount(Guid accountId, CancellationToken ct);
    }
}
