using System.Drawing;

namespace Borowik.Books.Entities;

public record BookshelfWithBooks
(
    Guid Id,
    string Name,
    string Description,
    Color Color,
    Book[] Books,
    BooksSortMode SortMode,
    DateTime CreatedAt
) : Bookshelf(Id, Name, Description, Color, SortMode, CreatedAt)
{
    public BookshelfWithBooks(Bookshelf bookshelf, Book[] books) : this(
        bookshelf.Id,
        bookshelf.Name,
        bookshelf.Description,
        bookshelf.Color,
        books,
        bookshelf.SortMode,
        bookshelf.CreatedAt)
    {
    }
}