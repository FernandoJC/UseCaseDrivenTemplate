using System.Text.Json;

namespace Application.Abstractions.UseCases;

public interface IHealthCheckUseCase : IUseCase<IHealthCheckUseCase.Input, string>
{
    public interface Input
    {
        string HealthMessage { get; }

        string ToJson() => JsonSerializer.Serialize(this);
    }
}