using MediatR;

namespace Borowik.Commands;

internal class MediatorCommander : ICommander
{
    private readonly IMediator _mediator;

    public MediatorCommander(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    public async Task SendCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
    {
        await _mediator.Send(command, cancellationToken);
    }

    public async Task<TResult> SendCommandAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand<TResult>
    {
        return await _mediator.Send(command, cancellationToken);
    }
}