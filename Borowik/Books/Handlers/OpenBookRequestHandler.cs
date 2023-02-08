using Borowik.Books.Contracts;
using Borowik.Services;
using MediatR;

namespace Borowik.Books.Handlers;

internal class OpenBookRequestHandler : IRequestHandler<OpenBookRequest, OpenBookResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public OpenBookRequestHandler(
        IBookRepository bookRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _bookRepository = bookRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<OpenBookResponse> Handle(OpenBookRequest request, CancellationToken cancellationToken)
    {
        if (!await _bookRepository.ExistsAsync(request.BookId, cancellationToken))
            throw new BorowikException($"Book with Id '{request.BookId}' does not exist");

        var book = await _bookRepository.GetAsync(request.BookId, cancellationToken);
        book = book with { LastOpenedAt = _dateTimeProvider.GetUtcNew() };

        await _bookRepository.UpdateAsync(book, cancellationToken);
        var content = await _bookRepository.GetContentAsync(request.BookId, cancellationToken);


        return new OpenBookResponse(book, content);
    }
}