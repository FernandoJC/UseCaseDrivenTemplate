using Application.Abstractions.UseCases;
using Application.UseCases;

namespace WebAPI.Endpoints;

public static class HealthCheckEndpoint
{
    public static void RegisterHealthCheckEndpoint(this WebApplication app)
    {
        var endpoints = app.MapGroup($"/healthcheck");
        endpoints.MapGet("/", async (IHealthCheckUseCase healthCheckUseCase, string message) =>
        {
            var input = new HealthCheckUseCaseInput(message);
            var output = await healthCheckUseCase.Execute(input);
            var result = new
            {
                healthCheckMessage = output
            };
            return Results.Ok(result);
        })
        .WithName("HealthCheck")
        .WithOpenApi();
    }
}