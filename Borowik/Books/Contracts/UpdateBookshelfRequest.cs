using System.Drawing;
using Borowik.Books.Entities;

namespace Borowik.Books.Contracts;

public record UpdateBookshelfRequest
(
    Guid BookshelfId,
    string? Name = null,
    string? Description = null,
    Color? Color = null,
    BooksSortMode? SortMode = null
) : IBorowikRequest<UpdateBookshelfResponse>;