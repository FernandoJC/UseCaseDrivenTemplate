using Application.Abstractions.UseCases;
using Microsoft.Extensions.Logging;

namespace Application.UseCases;

public class HealthCheckUseCase(ILogger<HealthCheckUseCase> logger) : IHealthCheckUseCase
{
    private const string UseCaseName = nameof(HealthCheckUseCase);
    private readonly ILogger<HealthCheckUseCase> logger = logger;

    public Task<IHealthCheckUseCase.Output> Execute(IHealthCheckUseCase.Input input)
    {
        logger.LogInformation("Executing {usecase} with input {input}", UseCaseName, input.ToJson());
        var healthyMessage = $"Health check processed message '{input.CheckInMessage}' successfully!";

        logger.LogInformation("Finished {usecase} execution", UseCaseName);
        return Task.FromResult(new HealthCheckUseCaseOutput(healthyMessage) as IHealthCheckUseCase.Output);
    }
}

public class HealthCheckUseCaseInput(string checkInMessage) : IHealthCheckUseCase.Input
{
    public string CheckInMessage { get; } = checkInMessage;
}

public class HealthCheckUseCaseOutput(string healthyOutputMessage) : IHealthCheckUseCase.Output
{
    public string HealthyOutputMessage { get; } = healthyOutputMessage;
}