using MDA.Web.API.Hubs;
using MDA.Web.Application.Messaging.Kafka;
using MDA.Web.Infrastructure.Messaging.Kafka;

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
            
            // Kafka
            builder.Services.Configure<KafkaConsumerConfigOptions>(builder.Configuration.GetSection("KafkaConsumerConfig"));

            // CORS
            var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();

            // WS / SignalR
            var hubPath = builder.Configuration["Realtime:HubPath"] ?? "/ws/mda";

            #endregion

            #region CORS
            builder.Services.AddCors(o =>
            {
                o.AddPolicy("client",
                    p => p.WithOrigins(corsOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials());
            });
            #endregion

            #region WS / SignalR
            builder.Services.AddSignalR();

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

            //app.UseHttpsRedirection();

            app.UseCors("client");

            app.UseAuthorization();


            app.MapControllers();

            app.MapHub<MDAHub>(hubPath);

            app.Run();
        }
    }
}
