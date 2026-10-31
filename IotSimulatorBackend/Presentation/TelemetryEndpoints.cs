using IotSimulatorBackend.Application.Services;
using IotSimulatorBackend.Presentation.Hubs;
using IotSimulatorBackend.Presentation.Models;
using Microsoft.AspNetCore.SignalR;

namespace IotSimulatorBackend.Presentation
{
    public static class TelemetryEndpoints
    {
        public static void MapTelemetryEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/telemetry");

            group.MapPost("/", async (TelemetryData telemetry, TelemetryService telemetryService, IHubContext<TelemetryHub> hubContext) =>
            {
                await telemetryService.ProcessTelemetryAsync(telemetry.DeviceId, telemetry.Temperature, telemetry.Timestamp);

                await hubContext.Clients.All.SendAsync("ReceiveTelemetry", new
                {
                    deviceId = telemetry.DeviceId,
                    temperature = telemetry.Temperature,
                    timestamp = telemetry.Timestamp.ToString("HH:mm:ss")
                });

                return Results.Ok(new { message = "Data received and translating" });

            });

            group.MapGet("/latest", async (TelemetryService telemetryService) =>
            {
                var history = await telemetryService.GetRecentHistoryAsync(10);
                return Results.Ok(history);
            });
        }
    }
}
