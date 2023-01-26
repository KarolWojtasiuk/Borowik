namespace Borowik.Queries;

public interface IQuerier
{
    public Task<TResult> SendQueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken);
}