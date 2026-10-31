using IotSimulatorBackend.Domain;
using Microsoft.EntityFrameworkCore;

namespace IotSimulatorBackend.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TelemetryRecord> TelemetryRecords => Set<TelemetryRecord>();
    }
}
