using Borowik.Books.Contracts;
using Borowik.Books.Entities;
using Borowik.Books.Services;
using Borowik.Services;
using MediatR;

namespace Borowik.Books.Handlers;

internal class ImportBookRequestHandler : IRequestHandler<ImportBookRequest, ImportBookResponse>
{
    private readonly IBookshelfRepository _bookshelfRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGuidProvider _guidProvider;
    private readonly IRawBookParser _rawBookParser;

    public ImportBookRequestHandler(
        IBookshelfRepository bookshelfRepository,
        IBookRepository bookRepository,
        IDateTimeProvider dateTimeProvider,
        IGuidProvider guidProvider,
        IRawBookParser rawBookParser)
    {
        _bookshelfRepository = bookshelfRepository;
        _bookRepository = bookRepository;
        _dateTimeProvider = dateTimeProvider;
        _guidProvider = guidProvider;
        _rawBookParser = rawBookParser;
    }

    public async Task<ImportBookResponse> Handle(ImportBookRequest request, CancellationToken cancellationToken)
    {
        if (!await _bookshelfRepository.ExistsAsync(request.BookshelfId, cancellationToken))
            throw new BorowikException($"Bookshelf with Id '{request.BookshelfId}' does not exist");

        var (pages, metadata) = await _rawBookParser.ParseAsync(request.Type, request.Stream, cancellationToken);

        var id = _guidProvider.Generate();
        var book = new Book(id, request.BookshelfId, metadata, _dateTimeProvider.GetUtcNew(), null);
        var content = new BookContent(id, pages);

        await _bookRepository.CreateAsync(book, content, cancellationToken);

        return new ImportBookResponse(book);
    }
}