using Confluent.Kafka;
using MDA.App.Log;
using MDA.Model;
using System.Text.Json;

namespace MDA.App.Service.AccountUpdate
{
    // For reference and debugging purposes
    public sealed class IBAccountUpdateGenericService : IAccountUpdateHandler
    {
        public IBAccountUpdateGenericService() { }

        public Task HandleAsync(IBAccountUpdate update, CancellationToken cancellationToken)
        {
            string key;
            string value = JsonSerializer.Serialize(update, update.GetType());

            switch (update)
            {
                case IBUpdateAccountValue v:
                    key = "IBUpdateAccountValue";
                    break;
                case IBUpdatePortfolio p:
                    key = "IBUpdatePortfolio";
                    break;
                case IBUpdateAccountTime t:
                    key = "IBUpdateAccountTime";
                    break;
                case IBAccountDownloadEnd e:
                    key = "IBAccountDownloadEnd";
                    break;
                default:
                    key = "Unknown";
                    break;
            }
            Console.WriteLine($"[IBAccountUpdate] Key = {key}, Value = {value}");

            return Task.CompletedTask;
        }
    }
}
