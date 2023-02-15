using Borowik.Books.Contracts;
using Borowik.Books.Entities;
using Borowik.Books.Services;
using Borowik.Services;
using MediatR;

namespace Borowik.Books.Handlers;

internal class OpenBookRequestHandler : IRequestHandler<OpenBookRequest, OpenBookResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IBookDataParser _bookDataParser;

    public OpenBookRequestHandler(
        IBookRepository bookRepository,
        IDateTimeProvider dateTimeProvider,
        IBookDataParser bookDataParser)
    {
        _bookRepository = bookRepository;
        _dateTimeProvider = dateTimeProvider;
        _bookDataParser = bookDataParser;
    }

    public async Task<OpenBookResponse> Handle(OpenBookRequest request, CancellationToken cancellationToken)
    {
        if (!await _bookRepository.ExistsAsync(request.BookId, cancellationToken))
            throw new BorowikException($"Book with Id '{request.BookId}' does not exist");

        var book = await _bookRepository.GetAsync(request.BookId, cancellationToken);
        book = book with { LastOpenedAt = _dateTimeProvider.GetUtcNew() };

        await _bookRepository.UpdateAsync(book, cancellationToken);
        var data = await _bookRepository.GetDataAsync(request.BookId, cancellationToken);
        var pages = await _bookDataParser.ParseAsync(book.Metadata.Type, data, cancellationToken);
        var content = new BookContent(book.Id, pages);

        return new OpenBookResponse(book, content);
    }
}