using Application;
using Application.Abstractions.UseCases;
using Application.UseCases;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/healthcheck", async (IHealthCheckUseCase healthCheckUseCase, string message) =>
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

app.Run();