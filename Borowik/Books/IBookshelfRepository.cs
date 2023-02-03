using Borowik.Books.Entities;

namespace Borowik.Books;

internal interface IBookshelfRepository
{
    Task<Bookshelf> GetBookshelfAsync(Guid bookshelfId, CancellationToken cancellationToken);
    Task CreateBookshelfAsync(Bookshelf bookshelf, CancellationToken cancellationToken);

    Task CreateBookAsync(Book book, BookContent content, CancellationToken cancellationToken);
    Task UpdateBookLastOpenedAtAsync(Guid bookId, DateTime lastOpenedAt, CancellationToken cancellationToken);

    Task<BookContent> GetBookContentAsync(Guid bookId, CancellationToken cancellationToken);
}