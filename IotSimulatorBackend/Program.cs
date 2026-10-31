using IotSimulatorBackend.Application.Interfaces;
using IotSimulatorBackend.Application.Services;
using IotSimulatorBackend.Domain;
using IotSimulatorBackend.Infrastructure.Persistence;
using IotSimulatorBackend.Presentation;
using IotSimulatorBackend.Presentation.Hubs;
using IotSimulatorBackend.Presentation.Models;
using Microsoft.EntityFrameworkCore;

namespace IotSimulatorBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=TelemetryRecords.db"));

            builder.Services.AddScoped<ITelemetryRepository, EfTelemetryRepository>();

            builder.Services.AddScoped<TelemetryService>();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:7225", "null")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            builder.Services.AddSignalR();

            var app = builder.Build();

            app.UseCors();

            app.MapHub<TelemetryHub>("/hub/telemetry");

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            }

            

            app.MapGet("/", () => "The local IoT server is running");

            app.MapTelemetryEndpoints();

            app.Run();
        }
    }
}
