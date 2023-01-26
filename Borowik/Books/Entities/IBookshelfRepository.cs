using Borowik.Entities;

namespace Borowik.Books.Entities;

internal interface IBookshelfRepository : IEntityRepository<Bookshelf, Guid>
{
    public Task CreateAsync(Bookshelf bookshelf, CancellationToken cancellationToken);
    public Task<Bookshelf[]> GetAllAsync(CancellationToken cancellationToken);
}