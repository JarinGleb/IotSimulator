using System;
using System.Collections.Generic;
using System.Text;

namespace IotSimulator.Models
{
    public class TelemetryData
    {
        public string DeviceId { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
