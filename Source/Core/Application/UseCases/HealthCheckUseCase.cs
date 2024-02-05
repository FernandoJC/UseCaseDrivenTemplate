using Application.Abstractions.UseCases;
using Microsoft.Extensions.Logging;

namespace Application.UseCases;

public class HealthCheckUseCase(ILogger<HealthCheckUseCase> logger) : IHealthCheckUseCase
{
    private const string UseCaseName = nameof(HealthCheckUseCase);
    private readonly ILogger<HealthCheckUseCase> logger = logger;

    public Task<string> Execute(IHealthCheckUseCase.Input input)
    {
        logger.LogInformation("Executing {usecase} with input {input}", UseCaseName, input.ToJson());
        var healthyMessage = $"Health check received message '{input.HealthMessage}' successfully!";

        logger.LogInformation("Finished {usecase} execution", UseCaseName);
        return Task.FromResult(healthyMessage);
    }
}

public class HealthCheckUseCaseInput(string healthMessage) : IHealthCheckUseCase.Input
{
    public string HealthMessage { get; } = healthMessage;
}