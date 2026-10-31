namespace IotSimulatorBackend.Domain
{
    public class TelemetryRecord
    {
        public Guid Id { get; set; }
        public string DeviceId { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public TelemetryRecord(Guid id, string deviceId, double temperature, DateTime timestamp)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(deviceId);
            Id = id;
            DeviceId = deviceId;
            Temperature = temperature;
            Timestamp = timestamp;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
