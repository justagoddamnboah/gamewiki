namespace gamewiki.net.api;

public static class HealthCheckEndpoint {
    public static void MapHealthCheck(this WebApplication app) {
        app.MapGet("/health", () => Results.Ok(new {
                status = "Healthy",
                timestamp = DateTime.UtcNow,
                uptime = TimeSpan.FromSeconds(Environment.TickCount64 / 1000).ToString(@"dd\.hh\:mm\:ss"),
                services = new {
                    games = "OK",
                    developers = "OK"
                }
            }))
            .WithSummary("Проверка работоспособности сервиса")
            .WithDescription("Health check endpoint для мониторинга состояния API")
            .Produces(StatusCodes.Status200OK);
    }
}