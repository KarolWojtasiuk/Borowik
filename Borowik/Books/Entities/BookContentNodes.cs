namespace Borowik.Books.Entities;

public static class BookContentNodes
{
    public record PlainTextNode(string Value) : IBookContentNode;
}