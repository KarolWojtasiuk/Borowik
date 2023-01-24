namespace Borowik.Entities;

public interface IEntity<TId>
    where TId : notnull
{
    public TId Id { get; }
}