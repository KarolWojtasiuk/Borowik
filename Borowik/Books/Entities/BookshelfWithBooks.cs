using System.Drawing;

namespace Borowik.Books.Entities;

public record BookshelfWithBooks
(
    Guid Id,
    string Name,
    string? Description,
    Color Color,
    Book[] Books,
    DateTime CreatedAt
) : Bookshelf(Id, Name, Description, Color, CreatedAt)
{
    public BookshelfWithBooks(Bookshelf bookshelf, Book[] books) : this(
        bookshelf.Id,
        bookshelf.Name,
        bookshelf.Description,
        bookshelf.Color,
        books,
        bookshelf.CreatedAt)
    {
    }
}