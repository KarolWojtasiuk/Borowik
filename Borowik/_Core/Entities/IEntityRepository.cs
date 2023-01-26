namespace Borowik.Entities;

internal interface IEntityRepository<TEntity, TId>
    where TEntity : IEntity<TId>
    where TId : notnull
{
}