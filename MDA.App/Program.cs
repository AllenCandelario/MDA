using MDA.Implementation;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MDA.App.Service.Notification;
using MDA.App.Workers.Subscribers;
using MDA.App.Workers.Listeners;
using MDA.App.Service.AccountUpdate;
using MDA.Config;
using MDA.App.Infrastructure.Kafka;
using Microsoft.Extensions.Configuration;
using MDA.App.Service.MarketData;

namespace MDA.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    #region Configuration
                    services
                        .Configure<IBKRConfigOptions>(context.Configuration.GetSection("IBKRConfig"))
                        .Configure<KafkaConfigOptions>(context.Configuration.GetSection("KafkaConfig"));
                    #endregion

                    #region Kafka
                    services.AddSingleton<KafkaProducer>();
                    #endregion

                    #region IBKR connection + subscribers + listeners & handlers
                    services
                        .AddSingleton<IBClient>() // Main IBKR class 

                    #region Listeners, throws events to Handlers
                        .AddHostedService<IBNotificationListener>() // Listener for notification events --> Passes to Notification services
                        .AddHostedService<IBAccountListener>() // Listener for account events --> Passes to AccountUpdate services
                        .AddHostedService<IBMarketDataListener>() // Listener for market data events --> Passes to MarketData services
                    #endregion

                    #region Handlers
                        // IB Notifications
                        //.AddSingleton<INotificationHandler, IBNotificationGenericService>() // For debugging purposes
                        .AddSingleton<INotificationHandler, IBNotificationKafkaService>()
                        //.AddSingleton<INotificationHandler, IBNotificationTestLongService>()

                        // IB Account Updates
                        //.AddSingleton<IAccountUpdateHandler, IBAccountUpdateGenericService>() // For debugging purposes
                        .AddSingleton<IAccountUpdateHandler, IBAccountUpdateSubscribePortfolioMarketData>()
                        .AddSingleton<IAccountUpdateHandler, IBAccountUpdateKafkaService>()

                        // IB Market Data
                        //.AddSingleton<IMarketDataHandler, IBMarketDataGenericService>() // For debugging purposes
                        .AddSingleton<IMarketDataHandler, IBMarketDataKafkaService>()
                    #endregion

                    #region Subscribers (auto-subscribes as background jobs)
                        .AddHostedService<IBConnectionWorker>() // Connects
                        .AddHostedService<IBMarketDataWorker>() // Sets market data type 
                        .AddHostedService<IBAccountWorker>() // Subscribes to account events
                        ;
                    #endregion
                    #endregion
                })
                .Build();

            await host.RunAsync();
        }
    }
}
