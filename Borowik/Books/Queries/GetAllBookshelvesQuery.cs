using Borowik.Books.Entities;
using Borowik.Queries;

namespace Borowik.Books.Queries;

public record GetAllBookshelvesQuery : IQuery<Bookshelf[]>;