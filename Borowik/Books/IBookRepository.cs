using Borowik.Books.Entities;

namespace Borowik.Books;

internal interface IBookRepository
{
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<Book> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<Book[]> GetAllAsync(Guid bookshelfId, CancellationToken cancellationToken);
    Task<BookContent> GetContentAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(Book book, BookContent content, CancellationToken cancellationToken);
    Task UpdateAsync(Book book, CancellationToken cancellationToken);
}