namespace Borowik.Commands;

public interface ICommander
{
    public Task SendCommandAsync(ICommand command, CancellationToken cancellationToken);

    public Task<TResult> SendCommandAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken);
}