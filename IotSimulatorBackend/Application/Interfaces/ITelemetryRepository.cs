using IotSimulatorBackend.Domain;

namespace IotSimulatorBackend.Application.Interfaces
{
    public interface ITelemetryRepository
    {
        Task SaveAsync(TelemetryRecord record);
        Task<IEnumerable<TelemetryRecord>> GetLatestAsync(int count);
    }
}
