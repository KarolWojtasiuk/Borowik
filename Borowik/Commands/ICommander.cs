namespace Borowik.Commands;

public interface ICommander
{
    public Task SendCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : ICommand;
    
    public Task<TResult> SendCommandAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken)
        where TCommand : ICommand<TResult>;
}