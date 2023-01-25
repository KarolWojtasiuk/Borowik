using System.Drawing;
using Borowik.Entities;

namespace Borowik.Books.Entities;

public record Bookshelf(Guid Id, string Name, string? Description, Book[] Books, Color Color, DateTime CreatedAt) : IEntity<Guid>;