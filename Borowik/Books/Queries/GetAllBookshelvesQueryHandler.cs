using Borowik.Books.Entities;
using Borowik.Queries;

namespace Borowik.Books.Queries;

internal class GetAllBookshelvesQueryHandler : QueryHandler<GetAllBookshelvesQuery, Bookshelf[]>
{
    private readonly IBookshelfRepository _bookshelfRepository;

    public GetAllBookshelvesQueryHandler(IBookshelfRepository bookshelfRepository)
    {
        _bookshelfRepository = bookshelfRepository ?? throw new ArgumentNullException(nameof(bookshelfRepository));
    }

    protected override async Task<Bookshelf[]> HandleAsync(GetAllBookshelvesQuery query, CancellationToken cancellationToken)
    {
        return await _bookshelfRepository.GetAllBookshelvesAsync(cancellationToken);
    }
}