using System.Text.Json;

namespace Application.Abstractions.UseCases;

public interface IHealthCheckUseCase : IUseCase<IHealthCheckUseCase.Input, IHealthCheckUseCase.Output>
{
    public interface Input
    {
        string CheckInMessage { get; }

        string ToJson() => JsonSerializer.Serialize(this);
    }

    public interface Output
    {
        string HealthyOutputMessage { get; }
    }
}