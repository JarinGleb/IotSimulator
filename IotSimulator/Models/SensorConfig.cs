using System;
using System.Collections.Generic;
using System.Text;

namespace IotSimulator.Models
{
    public class SensorConfig
    {
        public string DeviceId { get; set; } = string.Empty;
        public int IntervalMilliseconds { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
    }
}
