namespace Borowik.Books.Entities;

public interface IBookContentNode
{
    public static virtual string Type => "!!!EMPTY!!!";
    public Guid Id { get; }
};