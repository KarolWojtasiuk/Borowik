namespace Borowik.Entities;

internal interface IEntityRepository<TEntity, TId>
    where TEntity : IEntity<TId>
    where TId : notnull
{
    public Task<TEntity?> GetAsync(TId id, CancellationToken cancellationToken);
    public Task CreateAsync(TEntity entity, CancellationToken cancellationToken);
}