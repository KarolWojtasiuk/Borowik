using Borowik.Books.Contracts;
using Borowik.Books.Entities;
using MediatR;

namespace Borowik.Books.Handlers;

internal class GetBookshelfRequestHandler : IRequestHandler<GetBookshelfRequest, GetBookshelfResponse>
{
    private readonly IBookshelfRepository _bookshelfRepository;
    private readonly IBookRepository _bookRepository;

    public GetBookshelfRequestHandler(
        IBookshelfRepository bookshelfRepository,
        IBookRepository bookRepository)
    {
        _bookshelfRepository = bookshelfRepository;
        _bookRepository = bookRepository;
    }

    public async Task<GetBookshelfResponse> Handle(GetBookshelfRequest request, CancellationToken cancellationToken)
    {
        if (!await _bookshelfRepository.ExistsAsync(request.BookshelfId, cancellationToken))
            throw new BorowikException($"Bookshelf with Id '{request.BookshelfId}' does not exist");

        var bookshelf = await _bookshelfRepository.GetAsync(request.BookshelfId, cancellationToken);

        var books = await _bookRepository.GetAllAsync(request.BookshelfId, cancellationToken);
        return new GetBookshelfResponse(new BookshelfWithBooks(bookshelf, books));
    }
}