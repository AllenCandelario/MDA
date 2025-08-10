using Microsoft.Extensions.Configuration;
using MDA.Implementation;
using System.Text.Json;
using MDA.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MDA.App.Workers;

namespace MDA.App
{
    internal class Program
    {
        static int _accountRequestId;
        static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IBClient>();
                    services.AddHostedService<IBClientWorker>();
                    services.AddHostedService<IBClientAccountWorker>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}
