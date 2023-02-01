using MediatR;

namespace Borowik.Commands;

internal class MediatorCommander : ICommander
{
    private readonly IMediator _mediator;

    public MediatorCommander(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    public async Task SendCommandAsync(ICommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }

    public async Task<TResult> SendCommandAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken)
    {
        return await _mediator.Send(command, cancellationToken);
    }
}