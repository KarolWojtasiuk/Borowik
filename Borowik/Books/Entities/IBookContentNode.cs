namespace Borowik.Books.Entities;

public interface IBookContentNode
{
    public static abstract string Type { get; }
    public Guid Id { get; }
};