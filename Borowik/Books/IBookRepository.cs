using Borowik.Books.Entities;

namespace Borowik.Books;

internal interface IBookRepository
{
    Task<Book?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<Book[]> GetAllAsync(Guid bookshelfId, CancellationToken cancellationToken);
    Task<byte[]?> GetDataAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(Book book, byte[] data, CancellationToken cancellationToken);
    Task UpdateAsync(Book book, CancellationToken cancellationToken);
}