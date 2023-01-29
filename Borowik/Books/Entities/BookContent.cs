using Borowik.Entities;

namespace Borowik.Books.Entities;

public record BookContent(Guid Id, IBookContentNode RootNode) : IEntity<Guid>;