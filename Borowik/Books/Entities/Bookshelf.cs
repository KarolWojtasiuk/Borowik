using System.Drawing;

namespace Borowik.Books.Entities;

public record Bookshelf
(
    Guid Id,
    string Name,
    string? Description,
    Color Color,
    DateTime CreatedAt
);