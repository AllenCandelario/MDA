using MDA.Web.API.Hubs;
using MDA.Web.Application.Accounts;
using MDA.Web.Application.Accounts.Service;
using MDA.Web.Application.Categories;
using MDA.Web.Application.Holdings;
using MDA.Web.Application.Instruments;
using MDA.Web.Application.Instruments.Service;
using MDA.Web.Application.Notifications.Service;
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
            builder.Services.AddScoped<IBNotificationService>()
                            .AddScoped<AccountService>()
                            .AddScoped<InstrumentService>();
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
