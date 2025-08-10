using Microsoft.Extensions.Configuration;
using MDA.Implementation;
using System.Text.Json;
using MDA.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MDA.App.Workers;
using MDA.App.Service;

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
                        .AddSingleton<IBNotificationService>() // Handles notification events
                        .AddSingleton<IBAccountService>() // Handles account events

                        .AddHostedService<IBConnectionWorker>() // Connects
                        .AddHostedService<IBNotificationListener>() // Listener for notification events --> Passes to IBNotificationService
                        .AddHostedService<IBAccountWorker>() // Subscribes to account events
                        .AddHostedService<IBAccountListener>(); // Listener for account events --> Passes to IBAccountService

                })
                .Build();

            await host.RunAsync();
        }
    }
}
