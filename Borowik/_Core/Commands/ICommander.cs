namespace Borowik.Commands;

public interface ICommander
{
    public Task SendCommandAsync(ICommand command, CancellationToken cancellationToken = default);

    public Task<TResult> SendCommandAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
}