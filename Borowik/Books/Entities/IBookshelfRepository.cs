using Borowik.Entities;

namespace Borowik.Books.Entities;

internal interface IBookshelfRepository : IEntityRepository<Bookshelf, Guid>
{
    public Task CreateBookshelfAsync(Bookshelf bookshelf, CancellationToken cancellationToken);
    public Task<Bookshelf[]> GetAllBookshelvesAsync(CancellationToken cancellationToken);
    
    public Task CreateBookAsync(Guid bookshelfId, Book book, BookContent content, CancellationToken cancellationToken);
    public Task<BookContent> OpenBookAsync(Guid bookId, DateTime lastOpenedAt, CancellationToken cancellationToken);
}