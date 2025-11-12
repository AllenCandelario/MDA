using MDA.Web.Messaging.Kafka;
using MDA.Web.Messaging.Service;

namespace MDA.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Default
            builder.Services.AddControllers();
            builder.Services.AddOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            #endregion

            #region Configuration
            builder.Services.Configure<KafkaConsumerConfigOptions>(builder.Configuration.GetSection("KafkaConsumerConfig"));
            #endregion

            #region Background services
            builder.Services.AddHostedService<KafkaConsumer>();
            #endregion

            #region Handlers
            // Kafka Message Handlers
            builder.Services.AddSingleton<IBNotificationKafkaHandler>()
                            .AddSingleton<IBAccountUpdateKafkaHandler>();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
