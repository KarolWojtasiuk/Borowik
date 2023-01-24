using Borowik.Entities;

namespace Borowik.Books.Entities;

internal interface IBookshelfRepository : IEntityRepository<Bookshelf, Guid>
{
}