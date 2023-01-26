namespace Borowik.Entities;

internal interface IEntity<TId>
    where TId : notnull
{
    public TId Id { get; }
}