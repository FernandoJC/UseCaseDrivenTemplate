namespace Application.Abstractions;

public interface IUseCase<TInput, TOutput>
    where TInput : class
{
    Task<TOutput> Execute(TInput input);
}