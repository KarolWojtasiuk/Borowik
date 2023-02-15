namespace Borowik.Books.Entities;

public static class BookContentNodes
{
    public record ParagraphNode(Guid Id, string Value) : IBookContentNode
    {
        public static string Type => "Paragraph";
    }

    public record ImageNode(Guid Id, BookImage Value) : IBookContentNode
    {
        public static string Type => "Image";
    }
}