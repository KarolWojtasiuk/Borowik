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
        var bookshelf = await _bookshelfRepository.GetAsync(request.BookshelfId, cancellationToken)
            ?? throw new BookExceptions.BookshelfNotFoundException(request.BookshelfId);


        var books = await _bookRepository.GetAllAsync(request.BookshelfId, cancellationToken);

        return new GetBookshelfResponse(new BookshelfWithBooks(bookshelf, books));
    }
}