using Borowik.Books;
using Borowik.Books.Entities;
using Borowik.Database.Dexie.Entities;
using DexieNET;

namespace Borowik.Database.Dexie;

internal class BookRepository : IBookRepository
{
    private readonly IDbProvider _dbProvider;

    public BookRepository(IDbProvider dbProvider)
    {
        _dbProvider = dbProvider;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        return await db.BookEntities().Get(id) is not null;
    }

    public async Task<Book> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        var entity = await db.BookEntities().Get(id)
                     ?? throw new BorowikDomainException($"Book with Id '{id}' does not exist in database");

        return entity.Map();
    }

    public async Task<Book[]> GetAllAsync(Guid bookshelfId, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        var books = await db.BookEntities().Where(b => b.BookshelfId, bookshelfId).ToArray();
        return books.Select(b => b.Map()).ToArray();
    }

    public async Task<BookContent> GetContentAsync(Guid id, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        var entity = await db.BookContentEntities().Get(id)
                     ?? throw new BorowikDomainException($"Book with Id '{id}' does not exist in database");

        return entity.Map();
    }

    public async Task CreateAsync(Book book, BookContent content, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        await db.Transaction(async transaction =>
        {
            await transaction.BookEntities().Add(BookEntity.Map(book));
            await transaction.BookContentEntities().Add(BookContentEntity.Map(content));
        });
    }

    public async Task UpdateAsync(Book book, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        await db.BookEntities().Put(BookEntity.Map(book));
    }
}