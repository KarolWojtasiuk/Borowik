namespace Borowik.Books.Entities;

public record BookMetadata
(
    string Title,
    string Author,
    BookImage? Cover,
    BookType Type
);