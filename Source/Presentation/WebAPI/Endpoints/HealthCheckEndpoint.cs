using Application.Abstractions.UseCases;
using Application.UseCases;

namespace WebAPI.Endpoints;

public static class HealthCheckEndpoint
{
    public static void RegisterHealthCheckEndpoint(this WebApplication app)
    {
        var endpoints = app.MapGroup("/healthcheck");
        endpoints.MapGet("/", HealthCheckEndpointHandler).WithName("HealthCheck").WithOpenApi();
    }

    public static async Task<IResult> HealthCheckEndpointHandler(IHealthCheckUseCase healthCheckUseCase, string checkInMessage)
    {
        try
        {
            var input = new HealthCheckUseCaseInput(checkInMessage);
            var output = await healthCheckUseCase.Execute(input);
            return Results.Ok(output);
        }
        catch (Exception exception)
        {
            // Possible logging here.
            return Results.Problem(exception.Message, statusCode: 500);
        }
    }
}