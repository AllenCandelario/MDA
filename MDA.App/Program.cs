using Microsoft.Extensions.Configuration;
using MDA.Implementation;
using Newtonsoft.Json;

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
            ibClient.NotificationReceived += (_, ibNotification) => Console.WriteLine($"Notification: {JsonConvert.SerializeObject(ibNotification)}");
            ibClient.InitiateConnection();

            Console.ReadLine();
        }
    }
}
