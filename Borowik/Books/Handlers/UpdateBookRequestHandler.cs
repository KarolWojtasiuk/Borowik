using Borowik.Books.Contracts;
using MediatR;

namespace Borowik.Books.Handlers;

internal class UpdateBookRequestHandler : IRequestHandler<UpdateBookRequest, UpdateBookResponse>
{
    private readonly IBookRepository _bookRepository;

    public UpdateBookRequestHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<UpdateBookResponse> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetAsync(request.BookId, cancellationToken)
                        ?? throw new BookExceptions.BookshelfNotFoundException(request.BookId);

        book = book with
        {
            Metadata = book.Metadata with
            {
                Title = request.Title ?? book.Metadata.Title,
                Author = request.Author ?? book.Metadata.Author,
                Cover = request.Cover ?? book.Metadata.Cover,
            }
        };

        await _bookRepository.UpdateAsync(book, cancellationToken);

        return new UpdateBookResponse();
    }
}