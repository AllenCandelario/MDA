using MDA.Implementation;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MDA.App.Service.Notification;
using MDA.App.Workers.Subscribers;
using MDA.App.Workers.Listeners;
using MDA.App.Service.AccountUpdate;

namespace MDA.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services
                        .AddSingleton<IBClient>() // Main IBKR class 

                    #region Listeners, throws events to Handlers
                        .AddHostedService<IBNotificationListener>() // Listener for notification events --> Passes to IBNotificationService
                        .AddHostedService<IBAccountListener>() // Listener for account events --> Passes to IBAccountService
                    #endregion

                    #region Handlers
                        // IB Notifications
                        .AddSingleton<INotificationHandler, IBNotificationTestLongService>()
                        .AddSingleton<INotificationHandler, IBNotificationKafkaService>()
                        .AddSingleton<INotificationHandler, IBNotificationGenericService>()

                        // IB Account Updates
                        .AddSingleton<IAccountUpdateHandler, IBAccountUpdateGenericService>()
                        .AddSingleton<IAccountUpdateHandler, IBAccountUpdateKafkaService>()
                    #endregion

                    #region Subscribers (auto-subscribes as background jobs)
                        .AddHostedService<IBConnectionWorker>() // Connects
                        .AddHostedService<IBAccountWorker>() // Subscribes to account events
                        ;
                    #endregion
                })
                .Build();

            await host.RunAsync();
        }
    }
}
