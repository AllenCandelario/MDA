using Microsoft.Extensions.Configuration;
using MDA.Implementation;

namespace MDA.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // For development
                .AddEnvironmentVariables() // Production
                .Build();

            var ibClient = new IBClient(config);
            ibClient.InitiateConnection();

            Console.ReadLine();
        }
    }
}
