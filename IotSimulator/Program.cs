using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using IotSimulator.Models;

namespace IotSimulator
{
    public class Program
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _serverUrl = "https://localhost:7225/api/telemetry";

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== DEPLOYMENT OF AN IoT SENSOR NETWORK ===");

            var sensors = new List<SensorConfig>
            {
                new SensorConfig { DeviceId = "esp32-kitchen", IntervalMilliseconds = 2000, MinTemp = 22.0, MaxTemp = 28.0 },
                new SensorConfig { DeviceId = "esp32-bedroom", IntervalMilliseconds = 3000, MinTemp = 19.0, MaxTemp = 22.0 },
                new SensorConfig { DeviceId = "esp32-server-room", IntervalMilliseconds = 5000, MinTemp = 16.0, MaxTemp = 19.0 }
            };

            var sensorTasks = new List<Task>();

            foreach (var config in sensors)
            {
                Task task = RunSensorAsync(config);
                sensorTasks.Add(task);
            }

            Console.WriteLine($"All sensors ({sensorTasks.Count} pcs.) have been successfully initialized and launched.\n");

            await Task.WhenAll(sensorTasks);
        }


        private static async Task RunSensorAsync(SensorConfig config)
        {
            Random random = new Random(config.DeviceId.GetHashCode());

            while (true)
            {
                try
                {
                    double currentTemperature = Math.Round(config.MinTemp + (random.NextDouble() * (config.MaxTemp - config.MinTemp)), 1);

                    var telemetry = new TelemetryData
                    {
                        DeviceId = config.DeviceId,
                        Temperature = currentTemperature,
                        Timestamp = DateTime.UtcNow
                    };

                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [{config.DeviceId}] Recorded: {telemetry.Temperature}°C");


                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_serverUrl, telemetry);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($" -> [{config.DeviceId}] Successfully sent.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($" -> [{config.DeviceId}] Server error: {response.StatusCode}");
                        Console.ResetColor();
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($" -> [{config.DeviceId}] Network error: {ex.Message}");
                    Console.ResetColor();
                }

                await Task.Delay(config.IntervalMilliseconds);
            }
        }
    }
}
