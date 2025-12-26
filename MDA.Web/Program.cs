using MDA.Web.API.Hubs;
using MDA.Web.Application.Accounts.Interfaces;
using MDA.Web.Application.Accounts.Services;
using MDA.Web.Application.Categories.Interfaces;
using MDA.Web.Application.Categories.Services;
using MDA.Web.Application.Holdings.Interfaces;
using MDA.Web.Application.Holdings.Services;
using MDA.Web.Application.Instruments.Interfaces;
using MDA.Web.Application.Instruments.Services;
using MDA.Web.Application.Notifications.Services;
using MDA.Web.Application.Shared;
using MDA.Web.Infrastructure.Messaging.Kafka;
using MDA.Web.Infrastructure.Persistence;
using MDA.Web.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

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

            #region Database

            builder.Services.AddDbContext<MdaDbContext>(opt =>
            {
                var connectionString = builder.Configuration.GetConnectionString("MdaDb");
                opt.UseNpgsql(connectionString);
            });

            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IHoldingRepository, HoldingRepository>();
            builder.Services.AddScoped<IInstrumentRepository, InstrumentRepository>();

            #endregion 

            #region Background services
            builder.Services.AddHostedService<KafkaConsumer>();
            #endregion

            #region Services

            // Kafka message handlers
            builder.Services.AddKeyedScoped<IKafkaMessageHandler, IBNotificationKafkaMessageHandler>("dev.mda.ib.notification.v1")
                            .AddKeyedScoped<IKafkaMessageHandler, AccountKafkaMessageHandler>("dev.mda.ib.account.update.v1")
                            .AddKeyedScoped<IKafkaMessageHandler, InstrumentKafkaMessageHandler>("dev.mda.ib.market.data.v1");

            // REST API handlers
            builder.Services.AddScoped<IAccountQueries, AccountQueries>()
                            .AddScoped<ICategoryQueries, CategoryQueries>()
                            .AddScoped<IHoldingQueries, HoldingQueries>()
                            .AddScoped<IInstrumentQueries, InstrumentQueries>();
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
