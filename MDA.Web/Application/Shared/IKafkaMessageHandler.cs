using MDA.Web.Domain.Accounts;

namespace MDA.Web.Application.Shared
{
    public interface IKafkaMessageHandler
    {
        Task HandleKafkaMessageAsync(string key, string value, CancellationToken ct);
    }
}
