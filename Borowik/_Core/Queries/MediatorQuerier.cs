using MediatR;

namespace Borowik.Queries;

internal class MediatorQuerier : IQuerier
{
    private readonly IMediator _mediator;

    public MediatorQuerier(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<TResult> SendQueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
        where TQuery : IQuery<TResult>
    {
        return await _mediator.Send(query, cancellationToken);
    }
}