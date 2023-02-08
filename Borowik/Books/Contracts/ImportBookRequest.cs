using Borowik.Books.Entities;

namespace Borowik.Books.Contracts;

public record ImportBookRequest(Guid BookshelfId, RawBookType Type, byte[] Data) : IBorowikRequest<ImportBookResponse>;