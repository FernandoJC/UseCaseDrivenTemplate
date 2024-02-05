using Application.Abstractions.UseCases;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shouldly;
using WebAPI.Endpoints;

namespace WebAPI.UnitTests.Endpoints;

public class HealthCheckEndpointTests
{
    private readonly IHealthCheckUseCase healthCheckUseCase;

    public HealthCheckEndpointTests()
    {
        healthCheckUseCase = Substitute.For<IHealthCheckUseCase>();
    }

    [Fact]
    public async Task HealthCheckEndpointHandler_ShouldReturnOkOnSuccess()
    {
        var useCaseOutput = Substitute.For<IHealthCheckUseCase.Output>();
        useCaseOutput.HealthyOutputMessage.Returns("HealthCheck output message");
        IHealthCheckUseCase.Input? useCaseInput = null;
        healthCheckUseCase
            .Execute(Arg.Do<IHealthCheckUseCase.Input>(x => useCaseInput = x))
            .Returns(useCaseOutput);

        var result = await HealthCheckEndpoint.HealthCheckEndpointHandler(healthCheckUseCase, "Test");
        var okResult = result.ShouldBeOfType<Ok<IHealthCheckUseCase.Output>>();

        okResult.Value.ShouldNotBeNull();
        okResult.Value.HealthyOutputMessage.ShouldBe("HealthCheck output message");
        useCaseInput.ShouldNotBeNull();
        useCaseInput.CheckInMessage.ShouldBe("Test");
    }

    [Fact]
    public async Task HealthCheckEndpointHandler_ShouldReturn500OnFailure()
    {
        healthCheckUseCase
            .Execute(Arg.Any<IHealthCheckUseCase.Input>())
            .ThrowsForAnyArgs(new Exception("Something really bad happened"));

        var result = await HealthCheckEndpoint.HealthCheckEndpointHandler(healthCheckUseCase, "Test");
        var problemResult = result.ShouldBeOfType<ProblemHttpResult>();

        problemResult.StatusCode.ShouldBe(500);
        problemResult.ProblemDetails.Detail.ShouldBe("Something really bad happened");
    }
}