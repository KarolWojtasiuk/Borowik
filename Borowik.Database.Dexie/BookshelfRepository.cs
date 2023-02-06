using Borowik.Books;
using Borowik.Books.Entities;
using Borowik.Database.Dexie.Entities;
using DexieNET;

namespace Borowik.Database.Dexie;

internal class BookshelfRepository : IBookshelfRepository
{
    private readonly IDbProvider _dbProvider;

    public BookshelfRepository(IDbProvider dbProvider)
    {
        _dbProvider = dbProvider;
    }

    public async Task<Bookshelf?> GetBookshelfAsync(Guid bookshelfId, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        var entity = await db.BookshelfEntities().Get(bookshelfId);
        return entity?.Map();
    }

    public async Task CreateBookshelfAsync(Bookshelf bookshelf, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();

        await db.Transaction(async transaction =>
        {
            await transaction.BookshelfEntities().Add(BookshelfEntity.Map(bookshelf));
            await transaction.BookEntities().BulkAdd(bookshelf.Books.Select(BookEntity.Map));
        });
    }

    public async Task CreateBookAsync(Book book, BookContent content, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();

        await db.Transaction(async transaction =>
        {
            await transaction.BookEntities().Add(BookEntity.Map(book));
            await transaction.BookContentEntities().Add(BookContentEntity.Map(content));
        });
    }

    public async Task UpdateBookLastOpenedAtAsync(Guid bookId, DateTime lastOpenedAt,
        CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        await db.BookEntities().Update(bookId, b => b.LastOpenedAt, lastOpenedAt);
    }

    public async Task<BookContent?> GetBookContentAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        var entity = await db.BookContentEntities().Get(bookId);
        return entity?.Map();
    }
}