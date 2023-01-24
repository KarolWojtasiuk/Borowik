using MediatR;

namespace Borowik.Commands;

internal abstract class CommandHandler<TCommand> : IRequestHandler<TCommand, Unit>
    where TCommand : ICommand
{
    public async Task<Unit> Handle(TCommand command, CancellationToken cancellationToken)
    {
        await HandleAsync(command, cancellationToken);
        return Unit.Value;
    }

    protected abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}

internal abstract class CommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
    {
        return await HandleAsync(command, cancellationToken);
    }

    protected abstract Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
}
