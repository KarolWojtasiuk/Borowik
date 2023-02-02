namespace Borowik.Books.Entities;

public static class BookContentNodes
{
    public record PlainTextNode(Guid Id, string Value) : BookContentNode(Id);
}