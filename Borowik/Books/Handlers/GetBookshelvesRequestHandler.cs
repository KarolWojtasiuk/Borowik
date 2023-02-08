using Borowik.Books.Contracts;
using MediatR;

namespace Borowik.Books.Handlers;

internal class GetBookshelvesRequestHandler : IRequestHandler<GetBookshelvesRequest, GetBookshelvesResponse>
{
    private readonly IBookshelfRepository _bookshelfRepository;

    public GetBookshelvesRequestHandler(IBookshelfRepository bookshelfRepository)
    {
        _bookshelfRepository = bookshelfRepository;
    }

    public async Task<GetBookshelvesResponse> Handle(GetBookshelvesRequest request, CancellationToken cancellationToken)
    {
        return new GetBookshelvesResponse(await _bookshelfRepository.GetAllAsync(cancellationToken));
    }
}