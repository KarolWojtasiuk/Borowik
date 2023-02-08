namespace Borowik.Books.Contracts;

public record GetBookshelfRequest(Guid BookshelfId) : IBorowikRequest<GetBookshelfResponse>;