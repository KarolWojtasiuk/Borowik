namespace Borowik.Books.Entities;

public record BookContent(Guid BookId, IBookContentNode RootNode);