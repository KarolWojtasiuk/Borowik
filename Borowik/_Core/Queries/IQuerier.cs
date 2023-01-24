namespace Borowik.Queries;

public interface IQuerier
{
    public Task<TResult> SendQueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
        where TQuery : IQuery<TResult>;
}