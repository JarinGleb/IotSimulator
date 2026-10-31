namespace IotSimulatorBackend.Presentation.Models
{
    public class TelemetryData
    {
        public string DeviceId { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
