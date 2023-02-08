namespace Borowik;

public interface IBorowikManager
{
    public Task<TResponse> SendRequestAsync<TResponse>(IBorowikRequest<TResponse> request, CancellationToken cancellationToken = default);
}