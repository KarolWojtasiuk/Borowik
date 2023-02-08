using FluentValidation;
using MediatR;

namespace Borowik;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBorowikRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var results = _validators.Select(v => v.ValidateAsync(request, cancellationToken));
        var errors = (await Task.WhenAll(results))
            .Where(r => !r.IsValid)
            .SelectMany(r => r.Errors)
            .OrderBy(e => e.PropertyName)
            .ToArray();

        if (errors.Any())
            throw new ValidationException(errors);

        return await next();
    }
}