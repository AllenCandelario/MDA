using MDA.Implementation;
using MDA.Model;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDA.App.Workers
{
    public sealed class IBClientWorker : BackgroundService
    {
        private readonly IBClient _ib;

        public IBClientWorker(IBClient ib)
        {
            _ib = ib;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ib.NotificationReceived += OnNotification;
            _ib.AccountUpdateReceived += OnAccountUpdate;


            _ib.InitiateConnection();
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _ib.NotificationReceived -= OnNotification;
            _ib.AccountUpdateReceived -= OnAccountUpdate;

            _ib.Dispose();
            return base.StopAsync(cancellationToken);
        }


        public void OnNotification(IBNotification notification)
        {
            Console.WriteLine($"Notification: {JsonSerializer.Serialize(notification)}");
        }

        public void OnAccountUpdate(IBAccountUpdate accountUpdate)
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
