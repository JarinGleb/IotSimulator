using IotSimulatorBackend.Application.Interfaces;
using IotSimulatorBackend.Application.Services;
using IotSimulatorBackend.Domain;
using IotSimulatorBackend.Infrastructure.Persistence;
using IotSimulatorBackend.Presentation;
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

            var app = builder.Build();

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
