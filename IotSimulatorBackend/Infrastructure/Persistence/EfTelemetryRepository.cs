using IotSimulatorBackend.Application.Interfaces;
using IotSimulatorBackend.Domain;
using Microsoft.EntityFrameworkCore;

namespace IotSimulatorBackend.Infrastructure.Persistence
{
    public class EfTelemetryRepository : ITelemetryRepository
    {
        private readonly AppDbContext _context;

        public EfTelemetryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(TelemetryRecord record)
        {
            await _context.TelemetryRecords.AddAsync(record);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TelemetryRecord>> GetLatestAsync(int count)
        {
            return await _context.TelemetryRecords
                .OrderByDescending(r => r.Timestamp)
                .Take(count)
                .ToListAsync();
        }
    }
}
