using Borowik.Books.Entities;

namespace Borowik.Books.Contracts;

public record ImportBookRequest(Guid BookshelfId, RawBookType Type, Stream Stream) : IBorowikRequest<ImportBookResponse>;