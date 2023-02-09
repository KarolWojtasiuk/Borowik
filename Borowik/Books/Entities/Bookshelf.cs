using System.Drawing;

namespace Borowik.Books.Entities;

public record Bookshelf
(
    Guid Id,
    string Name,
    string Description,
    Color Color,
    BooksSortMode SortMode,
    DateTime CreatedAt
);