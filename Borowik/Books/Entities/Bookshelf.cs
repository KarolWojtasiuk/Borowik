using Borowik.Entities;

namespace Borowik.Books.Entities;

public record Bookshelf(Guid Id, string Name, string? Description, Book[] Books, DateTime CreatedAt) : IEntity<Guid>;