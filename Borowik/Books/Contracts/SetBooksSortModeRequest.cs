using Borowik.Books.Entities;

namespace Borowik.Books.Contracts;

public record SetBooksSortModeRequest(Guid BookshelfId, BooksSortMode SortMode) : IBorowikRequest<SetBooksSortModeResponse>;