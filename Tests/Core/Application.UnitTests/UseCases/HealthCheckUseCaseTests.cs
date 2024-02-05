using Application.UseCases;
using Microsoft.Extensions.Logging;
using Shouldly;

namespace Application.UnitTests.UseCases;

public class HealthCheckUseCaseTests
{
    private readonly ILogger<HealthCheckUseCase> logger;
    private readonly HealthCheckUseCase healthCheckUseCase;

    public HealthCheckUseCaseTests()
    {
        logger = NSubstitute.Substitute.For<ILogger<HealthCheckUseCase>>();
        healthCheckUseCase = new HealthCheckUseCase(logger);
    }

    [Fact]
    public async Task ShouldBuildHealthyMessage()
    {
        var input = new HealthCheckUseCaseInput("Testing");
        var output = await healthCheckUseCase.Execute(input);

        output.HealthyOutputMessage.ShouldBe("Health check processed message 'Testing' successfully!");
    }
}