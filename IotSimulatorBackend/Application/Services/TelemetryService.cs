using IotSimulatorBackend.Application.Interfaces;
using IotSimulatorBackend.Domain;

namespace IotSimulatorBackend.Application.Services
{
    public class TelemetryService
    {
        private readonly ITelemetryRepository _repository;

        public TelemetryService(ITelemetryRepository repository)
        {
            _repository = repository;
        }

        public async Task ProcessTelemetryAsync(string deviceId, double temperature, DateTime timestamp)
        {
            if (temperature > 50.0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ALERT] Temperature rise of {deviceId}: {temperature}°C!");
                Console.ResetColor();
            }

            var record = new TelemetryRecord(Guid.NewGuid(), deviceId, temperature, timestamp);
            await _repository.SaveAsync(record);
        }

        public async Task<IEnumerable<TelemetryRecord>> GetRecentHistoryAsync(int count)
        {
            return await _repository.GetLatestAsync(count);
        }
    }
}
