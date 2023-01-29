using Borowik.Books.Entities;
using Borowik.Commands;

namespace Borowik.Books.Commands;

public record ImportBookCommand(Guid BookshelfId, RawBookType Type, byte[] Content) : ICommand<Book>;