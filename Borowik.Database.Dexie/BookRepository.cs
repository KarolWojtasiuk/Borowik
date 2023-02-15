using Borowik.Books;
using Borowik.Books.Entities;
using Borowik.Database.Dexie.Entities;
using DexieNET;
using Scrutor;

namespace Borowik.Database.Dexie;

[ServiceDescriptor(typeof(IBookRepository))]
internal class BookRepository : IBookRepository
{
    private readonly IDbProvider _dbProvider;

    public BookRepository(IDbProvider dbProvider)
    {
        _dbProvider = dbProvider;
    }

    public async Task<Book?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        var entity = await db.BookEntities().Get(id);
        return entity?.Map();
    }

    public async Task<Book[]> GetAllAsync(Guid bookshelfId, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        var books = await db.BookEntities().Where(b => b.BookshelfId, bookshelfId).ToArray();
        return books.Select(b => b.Map()).ToArray();
    }

    public async Task<byte[]?> GetDataAsync(Guid id, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        var entity = await db.BookDataEntities().Get(id);
        return entity?.Data;
    }

    public async Task CreateAsync(Book book, byte[] data, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        await db.Transaction(async _ =>
        {
            await db.BookEntities().Add(BookEntity.Map(book));
            await db.BookDataEntities().Add(new BookDataEntity(book.Id, data));
        });
    }

    public async Task UpdateAsync(Book book, CancellationToken cancellationToken)
    {
        var db = await _dbProvider.GetAsync();
        await db.BookEntities().Put(BookEntity.Map(book));
    }
}