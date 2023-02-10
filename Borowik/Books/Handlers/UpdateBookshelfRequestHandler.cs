using Borowik.Books.Contracts;
using MediatR;

namespace Borowik.Books.Handlers;

internal class UpdateBookshelfRequestHandler : IRequestHandler<UpdateBookshelfRequest, UpdateBookshelfResponse>
{
    private readonly IBookshelfRepository _bookshelfRepository;

    public UpdateBookshelfRequestHandler(IBookshelfRepository bookshelfRepository)
    {
        _bookshelfRepository = bookshelfRepository;
    }

    public async Task<UpdateBookshelfResponse> Handle(UpdateBookshelfRequest request, CancellationToken cancellationToken)
    {
        var bookshelf = await _bookshelfRepository.GetAsync(request.BookshelfId, cancellationToken);

        bookshelf = bookshelf with
        {
            Name = request.Name ?? bookshelf.Name,
            Description = request.Description ?? bookshelf.Description,
            Color = request.Color ?? bookshelf.Color,
            SortMode = request.SortMode ?? bookshelf.SortMode
        };

        await _bookshelfRepository.UpdateAsync(bookshelf, cancellationToken);

        return new UpdateBookshelfResponse();
    }
}