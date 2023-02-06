using System.Drawing;
using Borowik.Books.Entities;
using Borowik.Books.Services;
using Borowik.Services;

namespace Borowik.Books;

internal class BookshelfManager : IBookshelfManager
{
    private readonly IBookshelfRepository _bookshelfRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IGuidProvider _guidProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IRawBookParser _rawBookParser;

    public BookshelfManager(
        IBookshelfRepository bookshelfRepository,
        IBookRepository bookRepository,
        IGuidProvider guidProvider,
        IDateTimeProvider dateTimeProvider,
        IRawBookParser rawBookParser)
    {
        _bookshelfRepository = bookshelfRepository;
        _bookRepository = bookRepository;
        _guidProvider = guidProvider;
        _dateTimeProvider = dateTimeProvider;
        _rawBookParser = rawBookParser;
    }

    public async Task<BookshelfWithBooks> GetBookshelfAsync(Guid bookshelfId, CancellationToken cancellationToken)
    {
        if (!await _bookshelfRepository.ExistsAsync(bookshelfId, cancellationToken))
            throw new BorowikDomainException($"Bookshelf with Id '{bookshelfId}' does not exist");
        
        var bookshelf = await _bookshelfRepository.GetAsync(bookshelfId, cancellationToken);

        var books = await _bookRepository.GetAllAsync(bookshelfId, cancellationToken);
        return new BookshelfWithBooks(bookshelf, books);
    }

    public async Task<Bookshelf[]> GetBookshelvesAsync(Guid bookshelfId, CancellationToken cancellationToken)
    {
        return await _bookshelfRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Bookshelf> CreateBookshelfAsync(
        string name,
        string? description,
        Color color,
        CancellationToken cancellationToken)
    {
        var bookshelf = new Bookshelf(
            _guidProvider.Generate(),
            name,
            description,
            color,
            _dateTimeProvider.GetUtcNew());

        await _bookshelfRepository.CreateAsync(bookshelf, cancellationToken);

        return bookshelf;
    }

    public async Task<Book> ImportBookAsync(
        Guid bookshelfId,
        RawBookType type,
        byte[] data,
        CancellationToken cancellationToken)
    {
        if (!await _bookshelfRepository.ExistsAsync(bookshelfId, cancellationToken))
            throw new BorowikDomainException($"Bookshelf with Id '{bookshelfId}' does not exist");

        var (pages, metadata) = await _rawBookParser.ParseAsync(type, data, cancellationToken);

        var id = _guidProvider.Generate();
        var book = new Book(id, bookshelfId, metadata, _dateTimeProvider.GetUtcNew(), null);
        var content = new BookContent(id, pages);

        await _bookRepository.CreateAsync(book, content, cancellationToken);

        return book;
    }

    public async Task<(BookContent, DateTime)> OpenBookAsync(
        Guid bookId,
        CancellationToken cancellationToken)
    {
        if (!await _bookRepository.ExistsAsync(bookId, cancellationToken))
            throw new BorowikDomainException($"Book with Id '{bookId}' does not exist");
        
        var book = await _bookRepository.GetAsync(bookId, cancellationToken);
        book = book with { LastOpenedAt = _dateTimeProvider.GetUtcNew() };

        await _bookRepository.UpdateAsync(book, cancellationToken);
        var content = await _bookRepository.GetContentAsync(bookId, cancellationToken);
        

        return (content, book.LastOpenedAt.Value);
    }
}