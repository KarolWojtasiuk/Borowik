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
    private readonly IBookDataParser _bookDataParser;

    public ImportBookRequestHandler(
        IBookshelfRepository bookshelfRepository,
        IBookRepository bookRepository,
        IDateTimeProvider dateTimeProvider,
        IGuidProvider guidProvider,
        IBookDataParser bookDataParser)
    {
        _bookshelfRepository = bookshelfRepository;
        _bookRepository = bookRepository;
        _dateTimeProvider = dateTimeProvider;
        _guidProvider = guidProvider;
        _bookDataParser = bookDataParser;
    }

    public async Task<ImportBookResponse> Handle(ImportBookRequest request, CancellationToken cancellationToken)
    {
        if (!await _bookshelfRepository.ExistsAsync(request.BookshelfId, cancellationToken))
            throw new BookExceptions.BookshelfNotFoundException(request.BookshelfId);

        var metadata = await _bookDataParser.ExtractMetadataAsync(request.Type, request.Data, cancellationToken);

        var id = _guidProvider.Generate();
        var book = new Book(id, request.BookshelfId, metadata, _dateTimeProvider.GetUtcNew(), null);

        await _bookRepository.CreateAsync(book, request.Data, cancellationToken);

        return new ImportBookResponse(book);
    }
}