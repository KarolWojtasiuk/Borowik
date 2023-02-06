namespace Borowik.Database.Dexie.Entities;

internal interface IEntity<TEntity, TBaseEntity>
{
    public TBaseEntity Map();
    public static abstract TEntity Map(TBaseEntity baseEntity);
}