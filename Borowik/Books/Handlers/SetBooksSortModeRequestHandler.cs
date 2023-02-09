using Borowik.Books.Contracts;
using MediatR;

namespace Borowik.Books.Handlers;

internal class SetBooksSortModeRequestHandler : IRequestHandler<SetBooksSortModeRequest, SetBooksSortModeResponse>
{
    private readonly IBookshelfRepository _bookshelfRepository;

    public SetBooksSortModeRequestHandler(IBookshelfRepository bookshelfRepository)
    {
        _bookshelfRepository = bookshelfRepository;
    }

    public async Task<SetBooksSortModeResponse> Handle(SetBooksSortModeRequest request, CancellationToken cancellationToken)
    {
        var bookshelf = await _bookshelfRepository.GetAsync(request.BookshelfId, cancellationToken);
        bookshelf = bookshelf with { SortMode = request.SortMode };
        await _bookshelfRepository.UpdateAsync(bookshelf, cancellationToken);

        return new SetBooksSortModeResponse();
    }
}