using Application.Abstractions.UseCases;
using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services.AddUseCases();

    private static IServiceCollection AddUseCases(this IServiceCollection services) =>
        services.AddScoped<IHealthCheckUseCase, HealthCheckUseCase>();
}