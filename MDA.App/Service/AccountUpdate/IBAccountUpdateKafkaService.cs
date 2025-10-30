using MDA.Model;

namespace MDA.App.Service.AccountUpdate
{
    public sealed class IBAccountUpdateKafkaService : IAccountUpdateHandler
    {
        public IBAccountUpdateKafkaService() { }

        public async Task HandleAsync(IBAccountUpdate accountUpdate, CancellationToken cancellationToken)
        {
            switch (accountUpdate)
            {
                case IBUpdateAccountValue v:
                    Console.WriteLine(
                        $"[Value] {v.AccountName}: {v.Key} = {v.Value} {v.Currency}"
                    );
                    break;

                case IBUpdatePortfolio p:
                    Console.WriteLine(
                        $"[Position] {p.AccountName}: {p.Contract.Symbol} {p.Position} @ {p.MarketPrice} → P&L {p.UnrealizedPNL}/{p.RealizedPNL}"
                    );
                    break;

                case IBUpdateAccountTime t:
                    Console.WriteLine($"[Time] {t.Timestamp}");
                    break;

                case IBAccountDownloadEnd e:
                    Console.WriteLine($"[End] Download complete for account {e.Account}");
                    break;

                default:
                    Console.WriteLine("[Unknown account update]");
                    break;
            }
        }
    }
}
