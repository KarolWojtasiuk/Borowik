using Borowik.Books;
using Borowik.Books.Entities;
using LiteDB;

namespace Borowik.Database.LiteDb;

internal class LiteDbBookshelfRepository : IBookshelfRepository
{
    private readonly ILiteDbProvider _liteDbProvider;

    public LiteDbBookshelfRepository(ILiteDbProvider liteDbProvider)
    {
        _liteDbProvider = liteDbProvider ?? throw new ArgumentNullException(nameof(liteDbProvider));
    }

    public async Task<Bookshelf> GetBookshelfAsync(Guid bookshelfId, CancellationToken cancellationToken)
    {
        var db = await _liteDbProvider.GetLiteDatabase(cancellationToken);

        var books = db.GetCollection<Book>().Find(b => b.BookshelfId == bookshelfId);
        var bookshelf = db.GetCollection<Bookshelf>().FindById(bookshelfId);

        return bookshelf with { Books = books.ToArray() };
    }

    public async Task CreateBookshelfAsync(Bookshelf bookshelf, CancellationToken cancellationToken)
    {
        var db = await _liteDbProvider.GetLiteDatabase(cancellationToken);
        db.GetCollection<Bookshelf>().Insert(bookshelf);
        db.GetCollection<Book>().InsertBulk(bookshelf.Books);
    }

    public async Task CreateBookAsync(Book book, BookContent content, CancellationToken cancellationToken)
    {
        var db = await _liteDbProvider.GetLiteDatabase(cancellationToken);
        db.GetCollection<Book>().Insert(book);
        db.GetCollection<BookContent>().Insert(content);
    }

    public async Task UpdateBookLastOpenedAtAsync(Guid bookId, DateTime lastOpenedAt, CancellationToken cancellationToken)
    {
        var db = await _liteDbProvider.GetLiteDatabase(cancellationToken);
        var book = db.GetCollection<Book>("Book").FindOne(b => b.Id == bookId);
        db.GetCollection<Book>().Update(book with { LastOpenedAt = lastOpenedAt });
    }

    public async Task<BookContent> GetBookContentAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var db = await _liteDbProvider.GetLiteDatabase(cancellationToken);
        return db.GetCollection<BookContent>().FindById(bookId);
    }
}