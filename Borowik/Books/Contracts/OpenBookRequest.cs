namespace Borowik.Books.Contracts;

public record OpenBookRequest(Guid BookId) : IBorowikRequest<OpenBookResponse>;