using Borowik.Books.Entities;

namespace Borowik.Books.Contracts;

public record UpdateBookRequest
(
    Guid BookId,
    string? Title = null,
    string? Author = null,
    BookImage? Cover = null
) : IBorowikRequest<UpdateBookResponse>;