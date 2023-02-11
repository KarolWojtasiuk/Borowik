namespace Borowik.Books.Entities;

public static class BookContentNodes
{
    public record PlainTextNode(Guid Id, string Value) : IBookContentNode
    {
        public static string Type => "PlainText";
    }

    public record ImageNode(Guid Id, BookImage Value) : IBookContentNode
    {
        public static string Type => "Image";
    }
}