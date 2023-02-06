using Borowik.Books.Entities;

namespace Borowik.Books;

internal interface IBookshelfRepository
{
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<Bookshelf> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<Bookshelf[]> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(Bookshelf bookshelf, CancellationToken cancellationToken);
}