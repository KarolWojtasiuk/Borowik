using System.Drawing;
using Borowik.Books.Entities;
using Borowik.Books.Services;
using Borowik.Services;

namespace Borowik.Books;

internal class BookshelfManager : IBookshelfManager
{
    private readonly IBookshelfRepository _bookshelfRepository;
    private readonly IGuidProvider _guidProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IRawBookParser _rawBookParser;

    public BookshelfManager(
        IBookshelfRepository bookshelfRepository,
        IGuidProvider guidProvider,
        IDateTimeProvider dateTimeProvider,
        IRawBookParser rawBookParser)
    {
        _bookshelfRepository = bookshelfRepository;
        _guidProvider = guidProvider;
        _dateTimeProvider = dateTimeProvider;
        _rawBookParser = rawBookParser;
    }

    public async Task<Bookshelf> CreateBookshelfAsync(
        string name,
        string? description,
        Color color,
        Book[] books,
        CancellationToken cancellationToken)
    {
        var bookshelf = new Bookshelf(
            _guidProvider.Generate(),
            name,
            description,
            color,
            books,
            _dateTimeProvider.GetUtcNew());

        await _bookshelfRepository.CreateBookshelfAsync(bookshelf, cancellationToken);

        return bookshelf;
    }

    public async Task<Book> ImportBookAsync(
        Guid bookshelfId,
        RawBookType type,
        byte[] data,
        CancellationToken cancellationToken)
    {
        var (node, metadata) = await _rawBookParser.ParseAsync(type, data, cancellationToken);

        var id = _guidProvider.Generate();
        var book = new Book(id, metadata, _dateTimeProvider.GetUtcNew(), null);
        var content = new BookContent(id, node);

        await _bookshelfRepository.CreateBookAsync(book, content, cancellationToken);

        return book;
    }

    public async Task<(BookContent, DateTime)> OpenBookAsync(
        Guid bookId,
        CancellationToken cancellationToken)
    {
        var lastOpenedAt = _dateTimeProvider.GetUtcNew();

        await _bookshelfRepository.UpdateBookLastOpenedAtAsync(bookId, lastOpenedAt, cancellationToken);
        var content = await _bookshelfRepository.GetBookContentAsync(bookId, cancellationToken);

        return (content, lastOpenedAt);
    }
}