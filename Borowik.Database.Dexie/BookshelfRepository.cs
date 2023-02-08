using Borowik.Books;
using Borowik.Books.Entities;
using Borowik.Database.Dexie.Entities;
using DexieNET;
using Scrutor;

namespace Borowik.Database.Dexie;

[ServiceDescriptor(typeof(IBookshelfRepository))]
internal class BookshelfRepository : IBookshelfRepository
{
    private readonly IDbProvider _dbProvider;

    public BookshelfRepository(IDbProvider dbProvider)
    {
        _dbProvider = dbProvider;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        return await db.BookshelfEntities().Get(id) is not null;
    }

    public async Task<Bookshelf> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        var entity = await db.BookshelfEntities().Get(id)
                     ?? throw new BorowikException($"Bookshelf with Id '{id}' does not exist in database");

        return entity.Map();
    }

    public async Task<Bookshelf[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        var bookshelves = await db.BookshelfEntities().ToArray();
        return bookshelves.Select(b => b.Map()).ToArray();
    }

    public async Task CreateAsync(Bookshelf bookshelf, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        await db.BookshelfEntities().Add(BookshelfEntity.Map(bookshelf));
    }
}