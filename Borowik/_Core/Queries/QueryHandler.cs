using MediatR;

namespace Borowik.Queries;

internal abstract class QueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    public async Task<TResult> Handle(TQuery query, CancellationToken cancellationToken)
    {
        return await HandleAsync(query, cancellationToken);
    }

    protected abstract Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
}