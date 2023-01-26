using Borowik.Books.Entities;
using Borowik.Entities;
using Borowik.Queries;

namespace Borowik.Books.Queries;

internal class GetBookshelfQueryHandler : QueryHandler<GetBookshelfQuery, Bookshelf>
{
    private readonly IEntityRepository<Bookshelf, Guid> _bookshelfRepository;

    public GetBookshelfQueryHandler(
        IEntityRepository<Bookshelf, Guid> bookshelfRepository)
    {
        _bookshelfRepository = bookshelfRepository ?? throw new ArgumentNullException(nameof(bookshelfRepository));
    }

    protected override async Task<Bookshelf> HandleAsync(GetBookshelfQuery query, CancellationToken cancellationToken)
    {
        return await _bookshelfRepository.GetAsync(query.Id, cancellationToken)
               ?? throw new NotImplementedException();
    }
}