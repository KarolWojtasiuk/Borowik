using Borowik.Books.Entities;

namespace Borowik.Books.Contracts;

public record ImportBookRequest(Guid BookshelfId, BookType Type, byte[] Data) : IBorowikRequest<ImportBookResponse>;