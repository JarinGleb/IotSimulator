using IotSimulatorBackend.Application.Services;
using IotSimulatorBackend.Presentation.Models;

namespace IotSimulatorBackend.Presentation
{
    public static class TelemetryEndpoints
    {
        public static void MapTelemetryEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/telemetry");

            group.MapPost("/", async (TelemetryData telemetry, TelemetryService telemetryService) =>
            {
                await telemetryService.ProcessTelemetryAsync(telemetry.DeviceId, telemetry.Temperature, telemetry.Timestamp);
                return Results.Ok(new { message = "Data received and processed" });
            });

            group.MapGet("/latest", async (TelemetryService telemetryService) =>
            {
                var history = await telemetryService.GetRecentHistoryAsync(10);
                return Results.Ok(history);
            });
        }
    }
}
