using Borowik.Books.Entities;
using Borowik.Queries;

namespace Borowik.Books.Queries;

public record GetBookshelfQuery(Guid Id) : IQuery<Bookshelf>;