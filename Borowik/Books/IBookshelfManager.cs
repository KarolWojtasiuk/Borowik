using System.Drawing;
using Borowik.Books.Entities;

namespace Borowik.Books;

public interface IBookshelfManager
{
    public Task<Bookshelf> CreateBookshelfAsync(
        string name,
        string? description,
        Color color,
        Book[] books,
        CancellationToken cancellationToken);

    public Task<Book> ImportBookAsync(
        Guid bookshelfId,
        RawBookType type,
        byte[] data,
        CancellationToken cancellationToken);

    public Task<(BookContent, DateTime)> OpenBookAsync(
        Guid bookId,
        CancellationToken cancellationToken);
}