using Microsoft.Extensions.Configuration;
using MDA.Implementation;
using System.Text.Json;

namespace MDA.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Retrieving configuration 
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // Development
                .AddEnvironmentVariables() // Production
                .Build();

            var ibClient = new IBClient(config);
            
            ibClient.NotificationReceived += (ibNotification) => Console.WriteLine($"Notification: {JsonSerializer.Serialize(ibNotification)}");
            ibClient.InitiateConnection();

            while (true)
            {
                var cmd = Console.ReadLine()?.Trim().ToLowerInvariant();
                switch (cmd)
                {
                    case "exit":
                        return;
                    case "cancel account summary":
                        ibClient.SubscribeToAccountSummary(false);
                        break;
                    case "subscribe account summary":
                        ibClient.SubscribeToAccountSummary(true);
                        break;
                    case "subscribe account updates":
                        ibClient.SubscribeToAccountUpdates(true);
                        break;
                    case "cancel account updates":
                        ibClient.SubscribeToAccountUpdates(false);
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }
    }
}
